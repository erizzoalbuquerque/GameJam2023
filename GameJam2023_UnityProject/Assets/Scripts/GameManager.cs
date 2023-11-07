using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    [Header("Game Parameters")]
    [SerializeField] int _customerKillPenalty = 10;
    [SerializeField] int _failureToServeCustomerPenalty = 20;
    [SerializeField] int _startMoney = 100;
    [SerializeField] int _gameDuration = 120;
    [Header("References")]
    [SerializeField] ScoreUI _scoreUI;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _musicAudioSource;
    [SerializeField] AudioClip _madeMoneySound;
    [SerializeField] AudioClip _lostMoneySound;
    [SerializeField] GameObject _gameOverCutsceneGameObject;
    [SerializeField] List<GamePhase> _gamePhases;

    State _state;
    PhaseState _phaseState;

    float _currentGameTime;
    float _gameStartTime;
    int _playerScore = 0;

    Player _player;
    GamePhase _phase;

    bool _introStateWasSetup;
    bool _playingGameStateWasSetup;
    bool _gameOverStateWasSetup;
    bool _victoryStateWasSetup;


    enum State { Intro, PlayingGame, GameOver, Victory };
    enum PhaseState { None, Stage1, Stage2, Stage3, Stage4, Stage5 };

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
    public float GameDuration { get => _gameDuration;}
    public float NormalizedGameTime { get => _currentGameTime / _gameDuration; }

    void Awake()
    {
        _player = FindAnyObjectByType<Player>();

        _introStateWasSetup = false;
        _playingGameStateWasSetup = false;
        _gameOverStateWasSetup = false;
        _victoryStateWasSetup = false;
    }

    void Start()
    {
        EnableGame(false);
        _currentGameTime = 0f;
        SetScore(_startMoney);
        _state = State.Intro;
        SetPhase(PhaseState.Stage1);
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
        if (_state != State.PlayingGame)
            return;

        SetScore(_playerScore + scoreToAdd);

        if (scoreToAdd > 0)
            _audioSource.PlayOneShot(_madeMoneySound);
        else if (scoreToAdd < 0)
            _audioSource.PlayOneShot(_lostMoneySound);
    }

    void SetScore(int score)
    {
        _playerScore = score;
        _scoreUI.SetScore(score);
    }

    void SetPhase(PhaseState phaseState)
    {
        if (_phaseState != phaseState)
        {
            //Deactivate current phase
            if (_phase != null)
            {
                _phase.gameObject.SetActive(false);
            }

            //Activate new phase
            _phaseState = phaseState;
            _phase = _gamePhases[(int) _phaseState - 1];
            _phase.gameObject.SetActive(true);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    void DoIntroState()
    {
        if (_introStateWasSetup == false)
        {
            StartCoroutine(IntroCoroutine());
            _introStateWasSetup = true;
        }        
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

        //Update Phase
        if (NormalizedGameTime < 0.2)
        {
            SetPhase(PhaseState.Stage1);

        } else if (NormalizedGameTime < 0.4)
        {
            SetPhase(PhaseState.Stage2);

        } else if (NormalizedGameTime < 0.6)
        {
            SetPhase(PhaseState.Stage3);

        } else if (NormalizedGameTime < 0.8)
        {
            SetPhase(PhaseState.Stage4);

        } else
        {
            SetPhase(PhaseState.Stage5);
        }

    }

    void DoGameOverState()
    {
        //Setup State
        if (_gameOverStateWasSetup == false)
        {
            EnableGame(false);
            _gameOverStateWasSetup = true;
            //_musicAudioSource.Stop();
            _gameOverCutsceneGameObject.SetActive(true);
        }
    }

    void DoVictoryState()
    {
        //Setup State
        if (_victoryStateWasSetup == false)
        {
            EnableGame(false);
            _victoryStateWasSetup = true;
            _musicAudioSource.Stop();
        }
    }

    void EnableGame(bool active)
    {
        //_player.GetComponent<PlayerInput>().enabled = active;
        FindAnyObjectByType<CostumerFactory>().enabled = active;

        //if (active == false)
        //{
        //    _player.GetComponent<PlayerMovement>().SetInput(Vector2.zero);
        //}
    }

    public void SetMusicPitch(float newPitch)
    {
        _musicAudioSource.pitch = newPitch;
    }

    public void SetNumberOfCustomersAtSameTime(int newNumber)
    {
        RoomManager.Instance.MaxNumberOfCustomersAtSameTime = newNumber;
    }

    public void AddNewTable()
    {
        RoomManager.Instance.EnableNewTable();
    }

    IEnumerator IntroCoroutine()
    {
        float cameraAnimationTime = 3f;
        float introCutsceneTime = 3f;

        float startSize = Camera.main.orthographicSize;
        Camera.main.orthographicSize = 5f;
        DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, startSize, cameraAnimationTime).SetEase(Ease.InCubic);
        
        yield return new WaitForSeconds(introCutsceneTime);

        _state = State.PlayingGame;
    }
}
