using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] Transform seat;
    [SerializeField] bool randomSeatPostion = false;

    Customer _customer;

    // Start is called before the first frame update
    void Start()
    {
        if (randomSeatPostion) 
        {
            int rand = Random.Range(0, 2);//a number 0 or 1

            if (rand == 0) 
            { 
                //Do Nothing;
            }
            else
            {
                this.transform.localScale = new Vector3(-1f,1f,1f);
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetSeatPosition()
    {
        return seat.transform.position;
    }

    public void Reserve(Customer customer)
    {
        this._customer = customer;
    }

    public void FreeTable()
    {
        _customer = null;
    }

    public bool IsFree()
    {
        if (_customer == null)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_customer != null && collision.tag == "Player")
        {
            _customer.DeliverFood();
        }
    }
}
