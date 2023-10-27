using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CustomerTask")]
public abstract class CustomerTask : ScriptableObject
{
    public bool IsDone { get => _isDone;}
    public bool Success { get => _success;}
    
    protected bool _isDone = false;
    protected bool _success = false;

    public abstract void Start();
    public abstract void Update();
    public abstract void Finish();
    public abstract void Interact();
}
