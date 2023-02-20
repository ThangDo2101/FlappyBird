using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class PlayScene : MonoBehaviour
{

    private static PlayScene _instance;
    public static PlayScene Instance { get { return _instance; } }
    public ScoreUpdate scoreUpdate;
    public GameObject bird;
    public GameObject pipePool;
    public GameObject ground;
    public ScorePanel restartUI;
    [HideInInspector]
    public int CurrentScore;
    [HideInInspector]
    public int HighScore;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt("HS", 0);
        scoreUpdate.UpdateScore();
        bird.GetComponent<Animator>().Play("Flying");
    }

    public void Fail()
    {
        Debug.Log("FAIL");
        bird.GetComponent<Animator>().Play("Dead");
        bird.GetComponent<Behavior>().enabled = false;
        pipePool.GetComponent<Behavior>().enabled = false;
        foreach (Transform pipe in pipePool.transform)
        {
            pipe.GetComponent<Behavior>().enabled = false;
            foreach (Transform pChild in pipe)
            {
                pChild.GetComponent<Collider2D>().enabled = false;
            }
        }
        ground.GetComponent<Animator>().speed = 0;
    }

    public void GameOver()
    {
        Debug.Log("GAMEOVER");
        bird.GetComponent<Animator>().Play("Dead");
        Time.timeScale = 0;
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("HS", HighScore);
        }
        scoreUpdate.gameObject.SetActive(false);
        restartUI.gameObject.SetActive(true);
        restartUI.OnShow();
    }
}
