using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioManager : MonoBehaviour
{

    public AudioClip main_theme;
    public AudioSource music_source;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
        if(level == 4)
        {
            music_source.clip = main_theme;
            music_source.Play();
        }
    }

}
