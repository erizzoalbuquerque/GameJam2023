using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class FoodTask : CustomerTask
{
    [SerializeField] Food _food;

    float _timeToFail = 10f;
    
    State _state;
    float _waitingStartTime;
    float _eatingStartTime;

    enum State
    {
        WaitingFood,
        Eating
    }

    public override void Start()
    {
        // Why the hell do we need this!
        _isDone = false;

        float _startTime = Time.time;
        _state = State.WaitingFood;

        Debug.Log("Starting task: Waiting for " + _food.name);
    }

    public override void Execute()
    {
        if (_isDone == true)
        {
            return;
        }

        switch (_state)
        {
            case State.WaitingFood:
                if (Time.time - _waitingStartTime < _timeToFail)
                {
                    //Do Something;
                }
                else
                {
                    FailTask();
                }
                break;
            
            case State.Eating:
                if (Time.time - _eatingStartTime < _food.AvgConsumeTime)
                {
                    Debug.Log("Eating " + _food.FoodName);
                }
                else
                {
                    CompleteTask();
                }
                break;
        }
    }

    void FailTask()
    {
        _isDone = true;
        _success = false;

        Debug.Log("Failed task: " + _food.FoodName);
    }

    void CompleteTask()
    {
        _isDone = true;
        _success = true;

        Debug.Log("Completed task: " + _food.FoodName);
    }

    void ReceiveFood()
    {
        _eatingStartTime = Time.time;
        _state = State.Eating;
        Pay();
    }

    void Pay()
    {
        // Increase Player Score (Global)
        // Display UI money (delay)
        Debug.Log("Received Money " + this._food.Price.ToString());
    }

    public override void Finish()
    {
        //Do nothing;
    }

    public override void Interact()
    {
        ReceiveFood();
    }
}
