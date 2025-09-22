using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColourPickerControll : MonoBehaviour
{
    public float CurentHue;
    public float CurentSaturation;
    public float CurentValue;

    [SerializeField] private RawImage hueImage;
    [SerializeField] private RawImage saturationValueImage;
    [SerializeField] private RawImage output;

    [SerializeField] private Slider hueSlider;

    [SerializeField] private TMP_InputField hexInputfield;

    private Texture2D hueTexture;
    private Texture2D svTexture;
    private Texture2D outputTexture;

    [SerializeField] MeshRenderer targetRenderer;

    private void Start()
    {
        CreateHueImage();
        CreateSVImage();
        CreateOutputImage();

        UpdateOutputImage();
    }

    private void CreateHueImage()
    {
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++)
        {
            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 1f));
        }

        hueTexture.Apply();
        CurentHue = 0;

        hueImage.texture = hueTexture;
    }

    private void CreateSVImage()
    {
        svTexture = new Texture2D(16, 16);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SVTexture";

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(CurentHue, (float)x / svTexture.width, (float)y / svTexture.height));
            }
        }

        svTexture.Apply();
        CurentSaturation = 0;
        CurentValue = 0;

        saturationValueImage.texture = svTexture;
    }

    private void CreateOutputImage()
    {
        outputTexture = new Texture2D(1, 16);
        outputTexture.wrapMode = TextureWrapMode.Clamp;
        outputTexture.name = "OutputTexture";

        Color currentColor = Color.HSVToRGB(CurentHue, CurentSaturation, CurentValue);

        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColor);
        }

        outputTexture.Apply();

        output.texture = outputTexture;
    }

    private void UpdateOutputImage()
    {
        Color currentColor = Color.HSVToRGB(CurentHue, CurentSaturation, CurentValue);

        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColor);
        }

        outputTexture.Apply();
        hexInputfield.text = ColorUtility.ToHtmlStringRGB(currentColor);

        if (targetRenderer != null)
        {
            targetRenderer.material.SetColor("_BaseColor", currentColor);
        }
    }

    public void SetSV(float x, float y)
    {
        CurentSaturation = x;
        CurentValue = y;

        UpdateOutputImage();
    }

    public void updateSvImage()
    {
        CurentHue = hueSlider.value;
        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(CurentHue, (float)x / svTexture.width, (float)y / svTexture.height));
            }
        }

        svTexture.Apply();

        UpdateOutputImage();
    }


    public void OnTextInput()
    {
        if (hexInputfield.text.Length < 6) { return; }

        Color newcol;

        if (ColorUtility.TryParseHtmlString("#" + hexInputfield.text, out newcol))
        {
            Color.RGBToHSV(newcol, out CurentHue, out CurentSaturation, out CurentValue);

            hueSlider.value = CurentHue;

            hexInputfield.text = "";

            UpdateOutputImage();
        }
    }
    public Color GetCurrentColor()
    {
       return Color.HSVToRGB(CurentHue, CurentSaturation, CurentValue);
    }
}
