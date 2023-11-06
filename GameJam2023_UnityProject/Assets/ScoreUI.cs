using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Color _normalColor = Color.white;
    [SerializeField] Color _dangerColor = Color.red;

    int _currentScore;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int newScore)
    {
        _currentScore = newScore;
        _text.text = _currentScore.ToString();

        if (_currentScore > 50)
            _text.color = _normalColor;
        else
            _text.color = _dangerColor;
    }
}
