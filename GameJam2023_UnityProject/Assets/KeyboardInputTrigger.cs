using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInputTrigger : MonoBehaviour
{
    [SerializeField] KeyCode _key;
    [SerializeField] UnityEvent _keyPressed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            _keyPressed.Invoke();
        }
    }
}
