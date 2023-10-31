using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerVisual : MonoBehaviour
{
    [SerializeField] Customer _customer;
    [SerializeField] CustomerMovement _customerMovement;

    Vector3 _startScale;
    Vector3 _lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _startScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_customer.CurrentSate == Customer.State.WalkingToTable || _customer.CurrentSate == Customer.State.Leaving) 
        {
            if (_customerMovement.CurrentVelocity.x > 0f)
                FaceDirection(true);
            else if (_customerMovement.CurrentVelocity.x < 0f)
                FaceDirection(false);
            else
            {
                //Do Nothing;
            }
        }
        else if (_customer.CurrentSate == Customer.State.DoingTasks)
        {
            Vector2 tableDirection = _customer.GetTablePosition() - _customer.transform.position;

            if (tableDirection.x >= 0f)
                FaceDirection(true);
            else
                FaceDirection(false);

            //if (_customer.IsEating())
            //    //Play eating animation;
        }
        else if (_customer.CurrentSate == Customer.State.Dying)
        {
            //Play Death animation
            this.transform.Rotate(Vector3.forward, Time.deltaTime * 360f);
        }
    }

    void FaceDirection(bool right)
    {
        this.transform.localScale = new Vector3((right ? 1f : -1f) * _startScale.x, _startScale.y, _startScale.z);
    }
}
