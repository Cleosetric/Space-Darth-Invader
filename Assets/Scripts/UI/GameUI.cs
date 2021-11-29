using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textWave;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);

        gameManager = GameManager.Instance;
        gameManager.onScoreChange += Refresh;
        gameManager.onNewWaveEnter += Refresh;

        gameManager.onPlayerDeath += GameOver;
        gameManager.onWaveEliminated += Victory;

        textScore.SetText("Score: 0");
        textWave.SetText("Wave: 1");
    }

    void GameOver(){
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    void Victory(){
        Time.timeScale = 0;
        victoryUI.SetActive(true);
    }

    void Refresh(){
        textScore.SetText("Score: " + (gameManager.gameScore).ToString());
        textWave.SetText("Wave: " + (gameManager.wave).ToString());
    }
}
