using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] float _maxSpeed;

    Rigidbody2D _rb;
    Vector2 _currentVelocity;
    Vector2 _currentDirection;

    public Vector2 CurrentVelocity { get => _currentVelocity; }


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void SetDirection(Vector2 direction)
    {
        _currentDirection = direction.normalized;
    }

    void Move()
    {
        _rb.velocity = _currentDirection * _maxSpeed;
    }
}
