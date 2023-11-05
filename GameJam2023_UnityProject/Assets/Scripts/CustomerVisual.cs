using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerVisual : MonoBehaviour
{
    [SerializeField] Customer _customer;
    [SerializeField] CustomerMovement _customerMovement;
    [SerializeField] Animator _animator;

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
            _animator.SetBool("isAngry", _customer.IsAngry);

            _animator.SetBool("isSitting", false);


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

            _animator.SetBool("isSitting", true);

            if (tableDirection.x >= 0f)
                FaceDirection(true);
            else
                FaceDirection(false);

            if (_customer.IsEating())
                _animator.SetBool("isEating", true);
            else
                _animator.SetBool("isEating", false);
        }
        else if (_customer.CurrentSate == Customer.State.Dying)
        {
            _animator.SetTrigger("Death");
            _animator.SetBool("dead", true);
            _animator.SetInteger("DeadCount", Random.Range(1, 4));
        }
    }

    void FaceDirection(bool right)
    {
        this.transform.localScale = new Vector3((right ? 1f : -1f) * _startScale.x, _startScale.y, _startScale.z);
    }
}
