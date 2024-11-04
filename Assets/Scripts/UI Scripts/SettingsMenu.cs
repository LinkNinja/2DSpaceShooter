using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
     
    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        

        //Clear out all the options in our dropdown
        resolutionDropdown.ClearOptions();

        //Create a list of strings which is going to be our options
        List<string> options = new List<string>();


        int currentResultionIndex = 0;

        //Loop through each element in the resolutions array and for each of them create a formatted string.
        //Added to the options list.
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);


            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResultionIndex = i;
            }
        }
        //Add options list to resolution drop down.
        resolutionDropdown.AddOptions(options);

       resolutionDropdown.value = currentResultionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume (float volume)
    {
    
        audioMixer.SetFloat("volume", volume);
        Debug.Log(volume);
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    public void SetResolution( int resolotionIndex)
    {
        Resolution resolution = resolutions[resolotionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
