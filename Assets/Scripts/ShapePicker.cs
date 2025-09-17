using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShapePicker : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int selectedIndex = -1;

    private Color selectedColor = Color.green;
    private Color normalColor = Color.white;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int idx = i;
            buttons[i].onClick.AddListener(() => OnButtonClicked(idx));
            SetButtonColor(i, normalColor);
        }
    }

    private void OnButtonClicked(int idx)
    {
        if (selectedIndex == idx)
        {
            SetButtonColor(idx, normalColor);
            selectedIndex = -1;
        }
        else
        {
            if (selectedIndex != -1)
                SetButtonColor(selectedIndex, normalColor);


            SetButtonColor(idx, selectedColor);
            selectedIndex = idx;
        }
    }

    private void SetButtonColor(int idx, Color color)
    {
        var colors = buttons[idx].colors;
        colors.normalColor = color;
        colors.selectedColor = color;
        buttons[idx].colors = colors;
    }

    public int GetSelectedButtonId()
    {
        return selectedIndex == -1 ? 0 : selectedIndex + 1;
    }
}
