using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider _musicSlider;
    [SerializeField] AudioSource _musicAudio;
   
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private int _firstPlayInt;
    private float musicFloat;

    private void Start()
    {
        _firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (_firstPlayInt == 0)
        {
            musicFloat = 0.25f;
            _musicSlider.value = musicFloat;
            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            _musicSlider.value = musicFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, _musicSlider.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
            SaveSoundSettings();
    }

    public void UpdateSound()
    {
        _musicAudio.volume = _musicSlider.value;         
    }

    public void StopMusic()
    {
        _musicAudio.volume = 0;
        _musicSlider.value = 0;
    }
}
