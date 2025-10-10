using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public enum AudioType
    {
        Hit, Jump, Attak
    }
    private AudioSource _audio;
    [SerializeField] AudioClip audio_Attak;
    [SerializeField] AudioClip audio_Hit;
    [SerializeField] AudioClip audio_Jump;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioType audio, bool priority = false)
    {
        if(_audio.isPlaying == false || priority)
            switch (audio)
            {
                case AudioType.Hit:
                     _audio.PlayOneShot(audio_Hit,0.3f);
                    break;

                case AudioType.Jump:
                    _audio.PlayOneShot(audio_Jump,0.8f);
                    break;

                case AudioType.Attak:
                    _audio.PlayOneShot(audio_Attak);
                break;
        }
    }
}
