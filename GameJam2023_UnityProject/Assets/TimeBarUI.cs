using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarUI : MonoBehaviour
{
    [SerializeField] Transform _progress;
    [SerializeField] Transform _originPoint;
    [SerializeField] Transform _endPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float normalizedTime = GameManager.Instance.CurrentGameTime / (float) GameManager.Instance.GameDuration;

        _progress.position = Vector3.Lerp(_originPoint.position, _endPoint.position, normalizedTime);
    }
}
