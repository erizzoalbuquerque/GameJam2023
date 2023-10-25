using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxAcceleration;
    [SerializeField] float _turningSpeed;
    [SerializeField] float _gizmosSize;
    
    Rigidbody2D _rb;
    Vector2 _currentVelocity;
    Vector2 _input;

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
        Vector2 normalizedInput = input.normalized;
        _input = normalizedInput;
    }

    void Move()
    {
        Vector2 velocity = _rb.velocity;
        Vector2 desiredVelocity = _maxSpeed * _input.normalized;

        velocity = Vector3.RotateTowards(velocity,desiredVelocity,_turningSpeed*Time.deltaTime,_maxAcceleration * Time.deltaTime);
		
        _rb.velocity = velocity;

        _currentVelocity = velocity;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(_input.normalized.x,_input.normalized.y,0f) * _gizmosSize);
        
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
