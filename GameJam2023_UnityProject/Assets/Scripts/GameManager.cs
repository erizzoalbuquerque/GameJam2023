using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    [Header("Game Parameters")]
    [SerializeField] int _customerKillPenalty = 10;
    [SerializeField] int _failureToServeCustomerPenalty = 20;
    [SerializeField] int _startMoney = 100;
    [SerializeField] int _gameDuration = 120;
    [Header("References")]
    [SerializeField] GameObject _scoreText;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _madeMoneySound;
    [SerializeField] AudioClip _lostMoneySound;

    State _state = State.Intro;
    float _currentGameTime;
    float _gameStartTime;
    int _playerScore = 0;
    Player _player;
    TextMeshProUGUI _scoreTextTMP;
    private bool _playingGameStateWasSetup;
    private bool _gameOverStateWasSetup;
    private bool _victoryStateWasSetup;


    enum State { Intro, PlayingGame, GameOver, Victory };

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
    public int DeathPenalty { get => _customerKillPenalty; }
    public int FailureToSatisfyClientPenalty { get => _failureToServeCustomerPenalty; }
    public float CurrentGameTime { get => _currentGameTime; }
    public int GameDuration { get => _gameDuration;}

    void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _scoreTextTMP = _scoreText.GetComponent<TextMeshProUGUI>();

        
    }

    void Start()
    {
        EnableGame(false);
        _currentGameTime = 0f;
        SetScore(_startMoney);
        _state = State.Intro;
    }

    void Update()
    {
        //Check Restart
        if (Input.GetKeyDown(KeyCode.R))
            Restart();

        switch (_state) 
        {
            case State.Intro:
                DoIntroState();
                break;
            case State.PlayingGame: 
                DoPlayingGameState();
                break;
            case State.GameOver: 
                DoGameOverState(); 
                break;
            case State.Victory: 
                DoVictoryState();
                break;
        }        
    }


    public void AddScore(int scoreToAdd)
    {
        SetScore(_playerScore + scoreToAdd);

        if (scoreToAdd > 0)
            _audioSource.PlayOneShot(_madeMoneySound);
        else if (scoreToAdd < 0)
            _audioSource.PlayOneShot(_lostMoneySound);
    }

    void SetScore(int score)
    {
        _playerScore = score;
        _scoreTextTMP.text = _playerScore.ToString();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    void DoIntroState()
    {
        //Starting Cutscene
        //And then;
        _state = State.PlayingGame;
    }

    void DoPlayingGameState()
    {
        //Setup State
        if (_playingGameStateWasSetup == false)
        {
            EnableGame(true);
            _gameStartTime = Time.time;
            _playingGameStateWasSetup = true;
        }

        //Check Game Over Condition
        if (_playerScore <= 0f)
        {
            _state = State.GameOver;
            return;
        }

        //Check Victory Condition
        if (_currentGameTime >= _gameDuration)
        {
            _state = State.Victory;
            return;
        }

        //Update Ttime
        _currentGameTime = Time.time - _gameStartTime;
    }

    void DoGameOverState()
    {
        //Setup State
        if (_gameOverStateWasSetup == false)
        {
            EnableGame(false);
            _gameOverStateWasSetup = true;
            _audioSource.Stop();
        }
    }

    void DoVictoryState()
    {
        //Setup State
        if (_victoryStateWasSetup == false)
        {
            EnableGame(false);
            _victoryStateWasSetup = true;
            _audioSource.Stop();
        }
    }

    void EnableGame(bool active)
    {
        //Enable/Disable playerMovement, playerInput, customerFactory,....
    }
}
