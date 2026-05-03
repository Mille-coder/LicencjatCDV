using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;
using FMOD.Studio;

public class OptionsScreen : MonoBehaviour
{
    public Toggle fullScreenTog;
    public Toggle vSyncTog;
    public GameObject OptionsMenu;
    public List<ResItem> resolutions = new List<ResItem>();
    public TMP_Text resolutionLable;

    [Header("Audio")]
    public Slider masterVolumeSlider;

    private int selectedRes;
    private Bus masterBus;

    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
            vSyncTog.isOn = false;
        else
            vSyncTog.isOn = true;

        masterBus = RuntimeManager.GetBus("bus:/");

        float currentVolume;
        masterBus.getVolume(out currentVolume);

        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = currentVolume;
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        UpdateResLable();
    }

    public void ResLeft()
    {
        selectedRes--;

        if (selectedRes < 0)
            selectedRes = resolutions.Count - 1;

        UpdateResLable();
    }

    public void ResRight()
    {
        selectedRes++;

        if (selectedRes > resolutions.Count - 1)
            selectedRes = 0;

        UpdateResLable();
    }

    public void UpdateResLable()
    {
        resolutionLable.text =
            resolutions[selectedRes].horizontal.ToString() +
            " X " +
            resolutions[selectedRes].vertical.ToString();
    }

    public void ApplyChanges()
    {
        if (vSyncTog.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        Screen.SetResolution(
            resolutions[selectedRes].horizontal,
            resolutions[selectedRes].vertical,
            fullScreenTog.isOn
        );
    }

    public void SetMasterVolume(float volume)
    {
        masterBus.setVolume(volume);
    }

    public void Close()
    {
        OptionsMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}