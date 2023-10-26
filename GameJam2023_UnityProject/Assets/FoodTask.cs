using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[Serializable]
public class FoodTask : CustomerTask
{
    [SerializeField] float _timeToFail = 20f;
    [SerializeField] float _timeToEat = 10f;
    [SerializeField] float _payment = 100f;
    [SerializeField] Food _food;
    
    State _state;
    float _waitingStartTime;
    float _eatingStartTime;

    enum State {WaitingFood, Eating}

    public override void Start()
    {
        float _startTime = Time.time;
        _state = State.WaitingFood;
    }

    public override void Update()
    {
        if (_isDone == true)
            return;

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

    void Pay()
    {
        //Increase Score
        Debug.Log("Received Money");
    }

    public override void Finish()
    {
        //Do nothing;
    }

    public override void Interact()
    {
        _state = State.Eating;
        Pay();        
    }
}


public class Food
{
    string name;
}