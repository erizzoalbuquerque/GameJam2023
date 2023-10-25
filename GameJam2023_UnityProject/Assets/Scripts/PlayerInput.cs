using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;

    void Start()
    {
        
    }

    void Update()
    {
        UnityEngine.Vector2 input;
        
        input =  new UnityEngine.Vector2(Input.GetAxis("Horizontal"),  Input.GetAxis("Vertical"));

        _playerMovement.SetInput(input);
    }
}
