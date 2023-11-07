using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhase : MonoBehaviour
{
    [SerializeField] float _musicPitch = 1.0f;
    [SerializeField] float _delay = 1.0f;
    [SerializeField] int _numberOfCustomersAtSameTime = 1;
    [SerializeField] AudioSource _audioSource;

    // Start is called before the first frame update
    void OnEnable()
    {
        GameManager.Instance.SetMusicPitch(_musicPitch);
        GameManager.Instance.SetNumberOfCustomersAtSameTime(_numberOfCustomersAtSameTime);

        if (_audioSource != null)
            Invoke("PlaySound", _delay);
    }

    void PlaySound()
    {
        _audioSource.Play();
    }
}
