using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio")]
public class AudioCue : ScriptableObject
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] Vector2 _volumeRange = Vector2.one;
    [SerializeField] Vector2 _pitchRange = Vector2.one;

    public AudioClip AudioClip { get => _audioClip; }
    public Vector2 VolumeRange { get => _volumeRange; }
    public Vector2 PitchRange { get => _pitchRange; }
}
