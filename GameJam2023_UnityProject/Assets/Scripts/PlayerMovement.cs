using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 10f;
    [SerializeField] float _turningSpeed = 5f;
    [SerializeField] float _maxAcceleration = 5f;
    [SerializeField] float _brakeAngleThreshold = 170.0f;
    [SerializeField] float _brakeAcceleration = 15f;

    [Header("Debug")]
    [SerializeField] bool _debug = false;
    [SerializeField] float _gizmosSize;
    
    Rigidbody2D _rb;
    Vector2 _currentVelocity;
    Vector2 _currentInput;
    bool _isBreaking = false;

    public Vector2 CurrentVelocity { get => _currentVelocity;}
    public Vector2 CurrentInput { get => _currentInput;}
    public bool IsBreaking { get => _isBreaking; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void SetInput(Vector2 input)
    {
        _currentInput = input;
    }

    void Move()
    {
        Vector2 currentVelocity = _rb.velocity;
        float currentSpeed = currentVelocity.magnitude;

        Vector2 newVelocity;
        if (Vector2.Angle(currentVelocity,_currentInput) >= _brakeAngleThreshold && currentSpeed > 0f)
        {
            //Player wants to brake
            newVelocity = currentVelocity.normalized * Mathf.MoveTowards(currentSpeed, 0f, _brakeAcceleration * Time.deltaTime);

            _isBreaking = true;
        }
        else
        {
            //Player wants to turn
            Vector2 desiredVelocity = _maxSpeed * _currentInput.normalized;
            newVelocity = Vector3.RotateTowards(currentVelocity, desiredVelocity, _turningSpeed * Time.deltaTime, _maxAcceleration * Time.deltaTime);
            
            _isBreaking = false;
        }        
		
        _rb.velocity = newVelocity;

        _currentVelocity = newVelocity;
    }


    void OnDrawGizmos()
    {
        if (_debug == false)
            return;
            
        //Green direction input
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(_currentInput.normalized.x,_currentInput.normalized.y,0f) * _gizmosSize);
        
        //Blue velocity
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(_currentVelocity.normalized.x, _currentVelocity.normalized.y, 0f) * _gizmosSize);
    }


    /*void Move()
    {
        Vector2 velocity = _rb.velocity;
        Vector2 desiredVelocity = _maxSpeed * _input.normalized;

        float maxSpeedChange = _maxAcceleration * Time.deltaTime;
		velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
		velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
		
        _rb.velocity = velocity;

        _currentVelocity = velocity;
    }*/
}
