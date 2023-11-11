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
    Vector3 _startTextLocalScale;

    // Start is called before the first frame update
    void Start()
    {
        _startTextLocalScale = _text.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentScore <= 0f)
        {
            _text.transform.localScale = _startTextLocalScale * (1f + 0.2f * Mathf.Abs(Mathf.Sin(Mathf.PI * 2f * Time.time)));
        }
        
    }

    public void SetScore(int newScore)
    {
        _currentScore = newScore;
        _text.text = _currentScore.ToString();

        if (_currentScore >= 100)
            _text.color = _normalColor;
        else
            _text.color = _dangerColor;
    }
}
