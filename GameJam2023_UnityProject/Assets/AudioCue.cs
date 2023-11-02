using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio")]
public class AudioCue : ScriptableObject
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] Vector2 _volumeRange;
    [SerializeField] Vector2 _pitchRange;

    public AudioClip AudioClip { get => _audioClip; }
    public Vector2 VolumeRange { get => _volumeRange; }
    public Vector2 PitchRange { get => _pitchRange; }
}
