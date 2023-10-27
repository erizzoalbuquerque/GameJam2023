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

    List<CustomerTask> _tasks;
    CustomerTask _currentTask;
    State _state;
    Table _table;

    bool _initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        _tasks = new List<CustomerTask>();

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

    public void Initialize(Table table)
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
        //this.transform.position = _table.GetSeatPosition();
        _state = State.DoingTasks;
    }

    void DoTasks()
    {
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
            FinishCurrentTask();

            if (_currentTask.Success == true)
            {
                //Success!
            }
            else
            {
                //Failed. Leave the restaurant.
                _state = State.Leaving;
            }
        }
        else
        {
            _currentTask.Update();
        }
    }

    public void Interact()
    {
        if (_currentTask == null)
            return;

        if (_currentTask.IsDone == true)
            return;

        _currentTask.Interact();
    }

    void StarNewTask()
    {
        _currentTask = _tasks[0];
        _currentTask.Start();
    }

    void FinishCurrentTask()
    {
        _currentTask.Finish();

        _currentTask = null;
        _tasks.Remove(_currentTask);        
    }
}
