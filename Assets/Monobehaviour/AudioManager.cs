using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //public AudioSource audiosrc;

    private void Awake()
    {
        instance = this;
    }

    public void PlayFX(string nameAudioClip,float volumen)
    {
        var audio = Resources.Load<AudioClip>("FX/" + nameAudioClip);
        if (audio == null) return;

        AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position,volumen);
        
    }
    /*public void PlayMusic(string nameAudioClip)
    {
        var audio = Resources.Load<AudioClip>("Music/" + nameAudioClip);
        if (audio == null|| audiosrc.clip == audio) return;

        audiosrc.clip = audio;
        audiosrc.Play();
    }*/
}
