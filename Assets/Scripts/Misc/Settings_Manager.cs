using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_Manager : MonoBehaviour
{
    public static Settings_Manager instance;
    public bool mute;
    AudioListener listener;
    private void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }
    private void Update()
    {
        if (listener == null)
        {
            listener = FindObjectOfType<AudioListener>();
            SetMute();
        }
    }
    public void ToggleMute()
    {
        mute = !mute;
        SetMute();
    }

    void SetMute()
    {
        if (mute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
