using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highscoreText;

public void OnShow()
    {
        scoreText.text = PlayScene.Instance.CurrentScore.ToString(); ;
        highscoreText.text = PlayScene.Instance.HighScore.ToString();
    }
}
