using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameObject scoreText;
    public int playerScore = 0;

    TextMeshProUGUI scoreTextTMP;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("GameManager not instanciated");
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        scoreTextTMP = scoreText.GetComponent<TextMeshProUGUI>();
        setScore(0);
    }

    public void addScore(int scoreToAdd)
    {
        setScore(playerScore + scoreToAdd);
    }

    void setScore(int score)
    {
        playerScore = score;
        scoreTextTMP.text = playerScore.ToString();
    }
}
