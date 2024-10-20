using UnityEngine;

public class LevelMusicManager : MonoBehaviour
{
    [SerializeField] AudioSource _musicAudio;
    private float _musicFloat;
    private static readonly string _musicPref = "MusicPref";

    private void Start()
    {
        LevelSoundsSettings();
    }

    private void LevelSoundsSettings()
    {
        Debug.Log("Должны были измениться настройки музыки");
        Debug.Log($"_musicFloat: {_musicFloat}");
        _musicFloat = PlayerPrefs.GetFloat(_musicPref);
        _musicAudio.volume = _musicFloat;
        Debug.Log($"_musicAudio.volume: {_musicAudio.volume}");
    }
}
