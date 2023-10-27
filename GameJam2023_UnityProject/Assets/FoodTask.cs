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

    float _timeToFail = 20f;
    float _timeToEat = 10f;
    float _payment = 100f;
    
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
        float _startTime = Time.time;
        _state = State.WaitingFood;

        Debug.Log("Starting task: " + _food.name);
    }

    public override void Update()
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
                if (Time.time - _eatingStartTime < _timeToEat)
                {
                    //Do Something;
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
    }

    void CompleteTask()
    {
        _isDone = true;
        _success = true;
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
