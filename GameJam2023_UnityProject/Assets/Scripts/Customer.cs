using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] List<CustomerTask> _possibleTasks;
    [SerializeField] int maxTasks = 3;
    [SerializeField] BalloonDialog _balloonDialog;
    [SerializeField] PathPlanner _pathPlanner;
    [SerializeField] CustomerMovement _customerMovement;
    [SerializeField] float _dyingStateDuration = 3f;
    [SerializeField] CustomerPayText _payText;

    //// Audio
    //[SerializeField] public AudioSource _audioSource;
    //[SerializeField] public AudioCue _receiveFoodAudioCue;
    //[SerializeField] public AudioCue _failCustomerAudioCue;

    public enum State
    {
        WalkingToTable,
        DoingTasks,
        Leaving,
        Dying,
    }

    List<CustomerTask> _tasks = new List<CustomerTask>();
    CustomerTask _currentTask;
    State _state;
    Table _table;
    Door _door;

    float _dyingStartTime;

    bool _initialized = false;

    public BalloonDialog BalloonDialog { get => _balloonDialog;}
    public State CurrentSate { get => _state; }

    // Start is called before the first frame update
    void Start()
    {
        _door = RoomManager.Instance.GetDoor();
        transform.position = _door.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_initialized == false)
        {
            return;
        }

        switch (_state)
        {
            case State.WalkingToTable:
                WalkToTable();
                break;

            case State.DoingTasks:
                DoTasks();
                break;

            case State.Leaving:
                WalkOut();
                break;

            case State.Dying:
                Dying();
                break;
        }
    }

    public void Initialize(Table table)
    {
        _state = State.WalkingToTable;
        _table = table;
        _table.Reserve(this);

        // Initialize Tasks
        int numTasks = Random.Range(1, maxTasks);

        for (int i = 0; i < numTasks; i++)
        {
            int index = Random.Range(0, _possibleTasks.Count);

            CustomerTask task = Instantiate(_possibleTasks[index], gameObject.transform);

            _tasks.Add(task);
        }

        _initialized = true;
    }

    void WalkToTable()
    {
        Vector2 seatPosition = _table.GetSeatPosition();

        if (((Vector2)this.transform.position - seatPosition).magnitude < 0.1f)
        {
            this.transform.position = seatPosition;
            _customerMovement.SetDirection(Vector2.zero);
            _state = State.DoingTasks;
            return;
        }
        
        Vector2 goalDirection = _pathPlanner.GetDirectionToGoal(seatPosition, this.transform.position);
        _customerMovement.SetDirection(goalDirection);
    }

    void DoTasks()
    {
        if (_tasks.Count == 0)
        {
            Leave();
            return;
        }

        if (_currentTask == null)
        {
            _currentTask = _tasks[0];
            _currentTask.Initialize(this);
        }

        if (_currentTask.IsDone == true)
        {
            _currentTask.Finish();

            if (_currentTask.Success == false)
            {
                //PlayFailSound();
                Leave();
                return;

            } else
            {
                _tasks.Remove(_currentTask);
                _currentTask = null;
            }
        }
        else
        {
            _currentTask.Execute();
        }
    }

    void Leave()
    {
        _state = State.Leaving;
        //Debug.Log("Leaving");
    }

    void WalkOut()
    {
        Vector3 doorPosition = _door.transform.position;

        Vector2 goalDirection = _pathPlanner.GetDirectionToGoal(doorPosition, this.transform.position);
        _customerMovement.SetDirection(goalDirection);

        if ((this.transform.position - doorPosition).magnitude < 0.5f)
        {
            _table.FreeTable();
            Destroy(gameObject);
        }
    }

    [ContextMenu("DeliverFood")]
    public void DeliverFood()
    {
        if (_currentTask == null)
        {
            return;
        }

        _currentTask.Interact();
    }

    public bool IsEating()
    {
        if ( _currentTask == null)
            return false;

        if (_currentTask is FoodTask)
        {
            FoodTask foodTask = (FoodTask)_currentTask;

            if (foodTask.CurrentState == FoodTask.State.Eating)
                return true;
            else
                return false;
        }
        else
        { 
            return false;
        }
    }

    public Vector3 GetTablePosition()
    {
        if (_table != null)
            return _table.transform.position;
        else
            return Vector3.zero;
    }

    void Die()
    {
        if (_currentTask != null)
        {
            Destroy(_currentTask);
            _currentTask = null;
        }

        _payText.Play( - GameManager.Instance.deathPenalty);
        GameManager.Instance.die();

        _balloonDialog.ShutUp();

        _customerMovement.SetDirection(Vector2.zero);//Stop any movement

        _dyingStartTime = Time.time;
        _state = State.Dying;
    }

    void Dying()
    {
        if (Time.time - _dyingStartTime > _dyingStateDuration)
        {
            _table.FreeTable();
            Destroy(this.gameObject);
        }
    }

    public void Pay(int amount)
    {
        _payText.Play(amount);
        GameManager.Instance.addScore(amount);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Die();
    }

    //public void PlayFailSound()
    //{
    //    AudioTools.PlayAudioCue(_failCustomerAudioCue, _audioSource);
    //}

    //public void PlayReceiveFoodSound()
    //{
    //    AudioTools.PlayAudioCue(_receiveFoodAudioCue, _audioSource);
    //}
}
