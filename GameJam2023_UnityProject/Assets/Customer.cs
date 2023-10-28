using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] Table receiveTable; // Delete later
    [SerializeField] List<CustomerTask> _possibleTasks;
    [SerializeField] int maxTasks = 3;

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
                break;

            case State.Dying:
                break;
        }
    }

    void Initialize(Table table)
    {
        _state = State.WalkingToTable;
        _table = table; // TableManager.reserveTable();

        InitializeTasks();

        _initialized = true;
    }

    void InitializeTasks()
    {
        int numTasks = Random.Range(1, maxTasks);

        for (int i = 0; i < numTasks; i++)
        {
            int index = Random.Range(0, _possibleTasks.Count);
            _tasks.Add(_possibleTasks[index]);
        }
    }

    void WalkToTable()
    {
        this.transform.position = _table.GetSeatPosition();
        _state = State.DoingTasks;
    }

    void DoTasks()
    {
        Debug.Log("Do Tasks");
        Debug.Log(_tasks.Count);

        //Check if there's any remaining task
        if (_tasks.Count == 0)
        {
            //There's nothing more to do, time to leave.
            _state = State.Leaving;
            return;
        }

        if (_currentTask == null)
        {
            StarNewTask();
        }

        if (_currentTask.IsDone == true)
        {
            _currentTask.Finish();

            if (_currentTask.Success == false)
            {
                _state = State.Leaving;
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

    void StarNewTask()
    {
        _currentTask = _tasks[0];
        _currentTask.Start();
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
