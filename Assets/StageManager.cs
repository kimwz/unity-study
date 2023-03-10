using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject spawner;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    public void Restart()
    {
        gameOverCanvas.SetActive(false);
        player.GetComponent<CharacterMove>().Restart();
        Score.score = 0;

    }
}
