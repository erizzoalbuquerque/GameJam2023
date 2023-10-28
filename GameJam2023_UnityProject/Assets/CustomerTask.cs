using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerTask : MonoBehaviour
{
    public bool IsDone { get => _isDone;}
    public bool Success { get => _success;}
    
    protected bool _isDone = false;
    protected bool _success = false;
    protected Customer _customer;

    public abstract void Initialize(Customer customer);
    public abstract void Execute();
    public abstract void Finish();
    public abstract void Interact();
}
