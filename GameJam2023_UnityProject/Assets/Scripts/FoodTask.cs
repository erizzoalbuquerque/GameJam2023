using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTask : CustomerTask
{
    [SerializeField] Food _food;

    [SerializeField] float _timeToFail = 10f;

    float _waitingStartTime;
    float _eatingStartTime;

    State _state;

    public State CurrentState { get => _state; }

    public enum State
    {
        WaitingFood,
        Eating
    }

    public override void Initialize(Customer customer)
    {
        _customer = customer;
        _state = State.WaitingFood;
        _waitingStartTime = Time.time;

        _customer.BalloonDialog.Say(_food.Img, _timeToFail);
        //Debug.Log("Initializing task: Waiting for " + _food.name);
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
                    //print("FoodTask Progress: " + ((Time.time - _waitingStartTime) / _timeToFail).ToString());
                }
                else
                {
                    FailTask();
                }
                break;
            
            case State.Eating:
                if (Time.time - _eatingStartTime < _food.AvgConsumeTime)
                {
                    //Debug.Log("Eating " + _food.FoodName);
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
        _customer.BalloonDialog.ShutUp();

        _isDone = true;
        _success = false;

        //Debug.Log("Failed task: " + _food.FoodName);
    }

    void CompleteTask()
    {
        _isDone = true;
        _success = true;

        //Debug.Log("Completed task: " + _food.FoodName);
    }

    void ReceiveFood()
    {
        Player player = GameManager.Instance.Player;

        if (player.HasFood(_food))
        {
            player.DeliverFood(_food);

            _eatingStartTime = Time.time;
            _state = State.Eating;

            _customer.BalloonDialog.ShutUp();
            _customer.PlayReceiveFoodSound();

            Pay();
        }
    }

    void Pay()
    {
        _customer.Pay(_food.Price);
    }

    public override void Finish()
    {
        //Do nothing;
    }

    public override void Interact()
    {
        if (_state == State.WaitingFood)
            ReceiveFood();
    }
}
