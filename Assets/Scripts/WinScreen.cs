using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    public GameObject gameCanvas;
    public GameObject titleCanvas;
    public GameObject winScreen;
    public GameObject scoreVal;
    public GameObject scoreText;

    public void WinGame(){
        var score = scoreVal.GetComponentInChildren<Text>().text;
        UpdateScore(score);
        gameCanvas.SetActive(false);
        winScreen.SetActive(true);
    }

    private void UpdateScore(string score){
        scoreText.GetComponentInChildren<Text>().text += score;
    }

    public void ToMainMenu(){
        gameCanvas.GetComponent<AudioManager>().StopBGM();
        winScreen.SetActive(false);
        titleCanvas.SetActive(true);
    }

    public void RestartGame(){
        winScreen.SetActive(false);
        gameCanvas.SetActive(true);
        gameCanvas.GetComponent<GameGrid>().RestartGame();
    }

}
