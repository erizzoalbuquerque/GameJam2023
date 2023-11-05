using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    [SerializeField] int deathPenalty = 10;
    [SerializeField] int failureToSatisfyClientPenalty = 20;
    [SerializeField] GameObject _scoreText;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _madeMoneySound;
    [SerializeField] AudioClip _lostMoneySound;

    int _playerScore = 0;
    Player _player;
    TextMeshProUGUI _scoreTextTMP;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if(_instance == null) 
                {
                    Debug.LogError("Couldn't find a instance of type GameManager on the scene!");
                }
            }

            return _instance;
        }
    }

    public Player Player { get => _player;}
    public int DeathPenalty { get => deathPenalty; }
    public int FailureToSatisfyClientPenalty { get => failureToSatisfyClientPenalty; }

    void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        _scoreTextTMP = _scoreText.GetComponent<TextMeshProUGUI>();
        SetScore(0);
    }

    public void AddScore(int scoreToAdd)
    {
        SetScore(_playerScore + scoreToAdd);

        if (scoreToAdd > 0)
            _audioSource.PlayOneShot(_madeMoneySound);
        else if (scoreToAdd < 0)
            _audioSource.PlayOneShot(_lostMoneySound);
    }

    //public void Die()
    //{
    //    AddScore(-deathPenalty);
    //    if (_lostMoneySound != null)
    //        _audioSource.PlayOneShot(_lostMoneySound);
    //}

    void SetScore(int score)
    {
        _playerScore = score;
        _scoreTextTMP.text = _playerScore.ToString();
    }

}
