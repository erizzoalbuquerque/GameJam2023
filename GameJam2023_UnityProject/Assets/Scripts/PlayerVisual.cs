using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;

    Vector3 _startScale;
    Vector3 _lastVelocity;

    [SerializeField] Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _startScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastVelocity.x >= 0f && _playerMovement.CurrentVelocity.x < 0f)
        {
            Flip(false); // Flip right;
        }
        else if (_lastVelocity.x <= 0f && _playerMovement.CurrentVelocity.x > 0f)
        {
            Flip(true); // Flip left;
        } else
        {
            // Do nothing;
        }

        if ( _playerMovement.IsBreaking)
        {
            _animator.SetBool("Break", true);
            _animator.SetBool("Walk", false);
        }
        else 
        {
            _animator.SetBool("Break", false);
        }

        if (_playerMovement.CurrentVelocity.x > 0.2f)
        {
            _animator.SetBool("Walk", true);
        }
        if (_playerMovement.CurrentVelocity.y > 0.5f)
        {
            _animator.SetBool("Walk", true);
        }
        if (_playerMovement.CurrentVelocity.x < -0.2f)
        {
            _animator.SetBool("Walk", true);
        }
        if (_playerMovement.CurrentVelocity.y < -0.5f)
        {
            _animator.SetBool("Walk", true);
        }
        if (_playerMovement.CurrentVelocity.x == 0f)
        {
            if (_playerMovement.CurrentVelocity. y == 0f)
            {
                _animator.SetBool("Walk", false);
            }
        }
    }

    void Flip(bool right)
    {
        this.transform.localScale = new Vector3((right? 1f:-1f) * _startScale.x, _startScale.y, _startScale.z);
    }
}
