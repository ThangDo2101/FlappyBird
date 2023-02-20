using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe")
        {
            PlayScene.Instance.Fail();
        }
        else if (collision.gameObject.tag == "Ground")
        {
            PlayScene.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pass")
        {
            PlayScene.Instance.CurrentScore++;
            PlayScene.Instance.scoreUpdate.UpdateScore();
        }
    }
}
