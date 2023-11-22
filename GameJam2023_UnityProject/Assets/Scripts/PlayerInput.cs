using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;

    void Start()
    {
        
    }

    void Update()
    {
        //UnityEngine.Vector2 input;

        /*
        input =  new UnityEngine.Vector2(Input.GetAxis("Horizontal"),  Input.GetAxis("Vertical"));

        _playerMovement.SetInput(input);
        */

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPosition = gameObject.transform.position;

            _playerMovement.SetInput((Vector2) (mousePos - playerPosition));
            
        } else
        {
            _playerMovement.SetInput(Vector2.zero);
        }
    }
}
