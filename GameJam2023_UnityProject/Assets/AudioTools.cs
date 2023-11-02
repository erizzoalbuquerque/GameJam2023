using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTools
{
    public static void PlayAudioCue(AudioCue audioCue, AudioSource audioSource)
    {
        Vector2 volumeRange = audioCue.VolumeRange;
        Vector2 pitchRange = audioCue.PitchRange;

        float volume = Random.Range(volumeRange.x, volumeRange.y);
        float pitch = Random.Range(pitchRange.x, pitchRange.y);

        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioCue.AudioClip, volume);
    }

    public static void PlayRandAudioCue(List<AudioCue> audioCueList, AudioSource audioSource)
    {
        AudioCue audioCue = audioCueList[Random.Range(0, audioCueList.Count)];

        PlayAudioCue(audioCue, audioSource);
    }
}
