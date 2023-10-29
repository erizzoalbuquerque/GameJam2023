using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] float _maxSpeed;

    Vector2 _targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetTarget(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    void Move()
    {
        if (transform.position != (Vector3)_targetPosition)
        {
            Vector2.MoveTowards(this.transform.position, _targetPosition,_maxSpeed * Time.deltaTime);
        }
    }
}
