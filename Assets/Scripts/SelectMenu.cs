using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectMenu : MonoBehaviour
{
    [Header("Top Level Panels")]
    [SerializeField] private GameObject valueModePanel;
    [SerializeField] private GameObject designModePanel;
    [SerializeField] private Button toValueModeButton;
    [SerializeField] private Button toDesignModeButton;

    [Header("Design Mode Subpanels")]
    [SerializeField] private GameObject pngPanel;
    [SerializeField] private GameObject shapeColourPanel;
    [SerializeField] private Toggle pngToggle;

    [Header("Shape & Colour Subpanels")]
    [SerializeField] private GameObject colourPickerPanel;
    [SerializeField] private GameObject shapePickerPanel;
    [SerializeField] private Button colourPickerButton;
    [SerializeField] private Button shapePickerButton;

    private void Start()
    {
        valueModePanel.SetActive(true);
        designModePanel.SetActive(false);

        pngToggle.onValueChanged.AddListener(OnPngToggleChanged);

        colourPickerButton.onClick.AddListener(() => ShowShapeColourSubpanel(true));
        shapePickerButton.onClick.AddListener(() => ShowShapeColourSubpanel(false));
        toValueModeButton.onClick.AddListener(GoToValueMode);
        toDesignModeButton.onClick.AddListener(GoToDesignMode);

        pngToggle.isOn = true;
        ShowDesignSubpanel(pngToggle.isOn);
    }

    public void GoToDesignMode()
    {
        valueModePanel.SetActive(false);
        designModePanel.SetActive(true);
        pngToggle.isOn = true;
        ShowDesignSubpanel(true);
    }

    public void GoToValueMode()
    {
        valueModePanel.SetActive(true);
        designModePanel.SetActive(false);
    }

    private void OnPngToggleChanged(bool isOn)
    {
        ShowDesignSubpanel(isOn);
    }

    private void ShowDesignSubpanel(bool showPng)
    {
        pngPanel.SetActive(showPng);
        shapeColourPanel.SetActive(!showPng);
    }

    private void ShowShapeColourSubpanel(bool showColour)
    {
        colourPickerPanel.SetActive(showColour);
        shapePickerPanel.SetActive(!showColour);
    }
    public int GetSelectedOption()
    {
        return pngToggle.isOn ? 0 : 1; // 0 for PNG, 1 for Shape + Colour
    }
}
