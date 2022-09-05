using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    [SerializeField] Slider volSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
            load();
        }
        else
        {
            load();
        }
        
    }

    public void changeVolume()
    {
        AudioListener.volume = volSlider.value;
        save();
    }

    private void load()
    {
        volSlider.value = PlayerPrefs.GetFloat("volume");
    }

    private void save()
    {
        PlayerPrefs.SetFloat("volume", volSlider.value);
    }

}
