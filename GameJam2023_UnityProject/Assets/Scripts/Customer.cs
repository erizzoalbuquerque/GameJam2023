using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEditor.Overlays;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] Table receiveTable; // Delete later
    [SerializeField] List<CustomerTask> _possibleTasks;
    [SerializeField] int maxTasks = 3;
    [SerializeField] BalloonDialog _balloonDialog;
    [SerializeField] PathPlanner _pathPlanner;
    [SerializeField] CustomerMovement _customerMovement;


    enum State
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

    bool _initialized = false;

    public BalloonDialog BalloonDialog { get => _balloonDialog;}

    // Start is called before the first frame update
    void Start()
    {
        Initialize(receiveTable);
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
                break;
        }
    }

    void Initialize(Table table)
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

        Debug.Log("Leaving");
    }

    void WalkOut()
    {
        //Vector2 doorPosition = RoomManager.GetDoorPosition()
        Vector3 doorPosition = 30f * Vector2.right;

        Vector2 goalDirection = _pathPlanner.GetDirectionToGoal(doorPosition, this.transform.position);
        _customerMovement.SetDirection(goalDirection);

        if ((this.transform.position - doorPosition).magnitude < 0.1f)
        {
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
}