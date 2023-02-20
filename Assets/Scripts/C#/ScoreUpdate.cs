using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField]
    private Sprite[] scoreNum;
    [SerializeField]
    private Image score1;
    [SerializeField]
    private Image score2;

    public void UpdateScore()
    {
        if (PlayScene.Instance.CurrentScore < 10)
        {
            score1.gameObject.SetActive(false);
            score2.sprite = scoreNum[PlayScene.Instance.CurrentScore];
        }
        else
        {
            score1.gameObject.SetActive(true);
            score1.sprite = scoreNum[PlayScene.Instance.CurrentScore / 10];
            score2.sprite = scoreNum[PlayScene.Instance.CurrentScore % 10];
        }
    }
}
