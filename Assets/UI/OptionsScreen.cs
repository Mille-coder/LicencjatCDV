using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsScreen : MonoBehaviour
{
    public Toggle fullScreenTog;
    public Toggle vSyncTog;
    public GameObject OptionsMenu;
    public List<ResItem> resolutions = new List<ResItem>();
    public TMP_Text resolutionLable;

    private int selectedRes;
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vSyncTog.isOn = false;
        }
        else
        {
            vSyncTog.isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = resolutions.Count-1;
        }
        UpdateResLable();
    }

    public void ResRight()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count-1)
        {
            selectedRes = 0;
        }
        UpdateResLable();
    }

    public void UpdateResLable()
    {
        resolutionLable.text = resolutions[selectedRes].horizontal.ToString() + " X " + resolutions[selectedRes].vertical.ToString();
    }

    public void ApplyChanges()
    {
        //Screen.fullScreen = fullScreenTog.isOn;

        if(vSyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullScreenTog.isOn);
    }

    public void Close()
    {
        OptionsMenu.SetActive(false);
    }
}
[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}