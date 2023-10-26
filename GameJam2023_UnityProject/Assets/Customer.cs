using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Customer : MonoBehaviour
{
    enum State
    {
        WalkingToTable,
        DoingTasks,
        Leaving,
        Dying,
    } 

    public List<CustomerTask> _tasks;
    CustomerTask _currentTask;
    State _state;
    Table _table;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
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

    void Setup()
    {
        _state = State.WalkingToTable;
    }

    void WalkToTable()
    {
        this.transform.position =  _table.GetSeatPosition();
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
