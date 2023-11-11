using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        _text.text = GameManager.lastScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
