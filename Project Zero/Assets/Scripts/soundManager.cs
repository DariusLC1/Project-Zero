using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class soundManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    const string MixerMusic = "MusicVolume";
    const string MixerSFX = "SFXVolume";

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(setMusic);
        sfxSlider.onValueChanged.AddListener(setSFX);
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            loadMusic();
        }
        else { loadMusic(); }



        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            loadSFX();
        }
        else { loadSFX(); }

    }

    void setMusic(float value)
    {
        mixer.SetFloat(MixerMusic, Mathf.Log10(value) * 20);
        saveMusic();
    }

    void setSFX(float value)
    {
        mixer.SetFloat(MixerSFX, Mathf.Log10(value) * 20);
        saveSFX();
    }

    void saveMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    void saveSFX()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    void loadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    void loadSFX()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}
