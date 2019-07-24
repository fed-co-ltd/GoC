using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneManager : MonoBehaviour {

    public Slider MusicSlider;
    public Text MusicLabel;
    public Slider SoundFXSlider;
    public Text SoundFXLabel;
    private Text VolumeLabel;
    private int VolumeType;
    public Slider MasterSlider;
    public Text MasterLabel;

    GameObject GameManager;

    void Start () {
        MusicSlider.normalizedValue = SettingsManager.MusicVolume;
        SoundFXSlider.normalizedValue = SettingsManager.SoundFXVolume;
        MasterSlider.normalizedValue = SettingsManager.MasterVolume;
    }
    public void ChangeGameMusicLabel (Text label) {
        VolumeLabel = label;
    }

    public void ChangeGameMusicLabelValue (Slider slider) {
        VolumeLabel.text = slider.value.ToString ();
    }

    public void UpdateSettings (int type) {
        switch (type) {
            case 1:
                SettingsManager.ChangeMusicVolume (MusicSlider.value);
                break;
            case 2:
                SettingsManager.ChangeSoundsVolume (SoundFXSlider.value);
                break;
            default:
                SettingsManager.ChangeMasterVolume (MasterSlider.value);
                break;
        }

    }

    public void UpdateSliderValue (Slider slider, Text label, int type) {
        //Check out 
        var value = 0f;
        switch (type) {
            case 1:
                value = SettingsManager.MusicVolume;
                break;
            case 2:
                value = SettingsManager.SoundFXVolume;
                break;
            default:
                value = SettingsManager.MasterVolume;
                break;
        }
        slider.normalizedValue = value;
        label.text = (value * 100).ToString ();
    }
}