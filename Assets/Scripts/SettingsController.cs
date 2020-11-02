using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Dropdown _resolutionsDropdown = default;
    [SerializeField] private Dropdown _shadowsDropdown = default;
    [SerializeField] private Dropdown _antialiasingDropdown = default;

    private Resolution[] _resolutions;

    private void Start()
    {
        InitializeResolutionSettings();   
        InitializeShadowsSettings();
        InitializeAntialiasingSettings();
    }

    private void InitializeResolutionSettings()
    {
        _resolutions = Screen.resolutions;
        _resolutionsDropdown.ClearOptions();
        for(int i = 0; i < _resolutions.Length; i++)
        {
            string option = $"{_resolutions[i].width}x{_resolutions[i].height}";
            _resolutionsDropdown.options.Add(new Dropdown.OptionData(option));
            if (Screen.width == _resolutions[i].width && Screen.height == _resolutions[i].height)
            {
                _resolutionsDropdown.value = i;
            }
        }
        _resolutionsDropdown.RefreshShownValue();
        _resolutionsDropdown.onValueChanged.AddListener(ChangeScreenResolution);
    }

    private void InitializeShadowsSettings()
    {
        _shadowsDropdown.ClearOptions();
        string[] shadowsLevels = new string[] {"Low", "Medium", "High", "Very High"};
        for (int i = 0; i < shadowsLevels.Length; i++)
        {
            _shadowsDropdown.options.Add(new Dropdown.OptionData(shadowsLevels[i]));
        }
        _shadowsDropdown.value = (int)QualitySettings.shadowResolution;
        _shadowsDropdown.RefreshShownValue();
        _shadowsDropdown.onValueChanged.AddListener(ChangeShadowsQuality);
    }
    
    private void InitializeAntialiasingSettings()
    {
        _antialiasingDropdown.ClearOptions();
        string[] aaLevels = new string[] {"OFF", "2x MSAA", "4x MSAA", "8x MSAA"};
        for (int i = 0; i < aaLevels.Length; i++)
        {
            _antialiasingDropdown.options.Add(new Dropdown.OptionData(aaLevels[i]));
        }
        int UpdateAAValueIndex(int aaQuality)
        {
            switch (aaQuality)
            {
                case 0: return 0;
                case 2: return 1;
                case 4: return 2;
                case 8: return 3;
                default: return -1;
            }
        }
        _antialiasingDropdown.value = UpdateAAValueIndex(QualitySettings.antiAliasing);
        _antialiasingDropdown.RefreshShownValue();
        _antialiasingDropdown.onValueChanged.AddListener(ChangeAntialiasingQuality);
    }

    private void ChangeShadowsQuality(int index)
    {
        switch (index)
        {
            case 0:
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                QualitySettings.shadowDistance = 40;
                QualitySettings.shadowCascades = 2;
                break;
            case 1:
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                QualitySettings.shadowDistance = 80;
                QualitySettings.shadowCascades = 2;
                break;
            case 2:
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.High;
                QualitySettings.shadowDistance = 120;
                QualitySettings.shadowCascades = 4;
                break;
            case 3:
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                QualitySettings.shadowDistance = 160;
                QualitySettings.shadowCascades = 4;
                break;
        }
    }

    private void ChangeAntialiasingQuality(int index)
    {
        switch (index)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;

                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                break;
        }
    }
    
    private void ChangeScreenResolution(int index)
    {
        Screen.SetResolution(_resolutions[index].width, _resolutions[index].height, FullScreenMode.ExclusiveFullScreen);
    }
}
