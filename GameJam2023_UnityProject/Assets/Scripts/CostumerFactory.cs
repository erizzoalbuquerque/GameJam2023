using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerFactory : MonoBehaviour
{
    [SerializeField] int _minWaitTime = 3;
    [SerializeField] int _maxWaitTime = 6;

    [SerializeField] Customer _customerPrefab;

    bool _resetWaitTime = true;
    float _lastCreateTime = 0;
    int _waitTime;

    // Start is called before the first frame update
    void Start()
    {
        CreateCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_resetWaitTime)
        {
            _waitTime = Random.Range(_minWaitTime, _maxWaitTime);
            _resetWaitTime = false;
        } 

        if ((Time.time - _lastCreateTime) > _waitTime)
        {
            CreateCustomer();
        }
    }

    void CreateCustomer()
    {
        Table _table = RoomManager.Instance.GetFreeTable();

        if (_table is not null)
        {
            Customer _customer = Instantiate(_customerPrefab, gameObject.transform);
            _customer.Initialize(_table);
        }

        _lastCreateTime = Time.time;
        _resetWaitTime = true;
    }
}
