using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public GameObject scoreText;
    public int playerScore = 0;
    public int deathPenalty = 10;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _madeMoneySound;

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

    void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        _scoreTextTMP = scoreText.GetComponent<TextMeshProUGUI>();
        setScore(0);
    }

    public void addScore(int scoreToAdd)
    {
        setScore(playerScore + scoreToAdd);

        if (scoreToAdd > 0)
            _audioSource.PlayOneShot(_madeMoneySound);
    }

    public void die()
    {
        addScore(-deathPenalty);
    }

    void setScore(int score)
    {
        playerScore = score;
        _scoreTextTMP.text = playerScore.ToString();
    }

}
