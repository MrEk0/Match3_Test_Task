using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    float initialScore = 0f;
    Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    public void IncreaseScore(float score)
    {
        initialScore += score;
        scoreText.text = initialScore.ToString();
    }

}
