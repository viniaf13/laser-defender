using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText = default;
    GameSession gameSession = default;


    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreText = GetComponent<TextMeshProUGUI>();
        //if (!gameSession || !scoreText) return; 

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();

    }
}
