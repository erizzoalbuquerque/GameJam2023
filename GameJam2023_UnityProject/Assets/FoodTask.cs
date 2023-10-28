using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FoodTask")]
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
        _isDone = false;

        float _startTime = Time.time;
        _state = State.WaitingFood;

        Debug.Log("Starting task: " + _food.name);
    }

    public override void Execute()
    {
        Debug.Log(_state);
        Debug.Log(_isDone);

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
                    //Do Something;
                    Debug.Log(Time.time - _eatingStartTime);
                    Debug.Log(_food.AvgConsumeTime);
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
        float _eatingStartTime = Time.time;
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
