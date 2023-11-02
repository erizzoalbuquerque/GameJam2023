using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CustomerAudio : MonoBehaviour
{
    [SerializeField] List<AudioCue> _screamingSounds;
    [SerializeField] AudioCue _cartoonFallingSound;
    [SerializeField] AudioCue _squishSound;
    [SerializeField] AudioCue _vooshSound;

    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScreamingSounds()
    {
        AudioTools.PlayRandAudioCue(_screamingSounds, _audioSource);
    }

    public void PlayCartoonFallingSound()
    {
        AudioTools.PlayAudioCue(_cartoonFallingSound, _audioSource);
    }

    public void PlaySquishSound()
    {
        AudioTools.PlayAudioCue(_squishSound, _audioSource);
    }

    public void PlayVooshSound()
    {
        AudioTools.PlayAudioCue(_vooshSound, _audioSource);
    }
}
