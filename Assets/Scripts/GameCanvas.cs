using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour
{

    public GameObject gameCanvas;
    public GameObject titleCanvas;
    public GameObject pauseText;
    public GameObject loseText;
    public GameObject winText;
    public GameObject scoreText;
    public GameObject levelText;
    public GameObject linesLeftText;
    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu(){
        gameCanvas.SetActive(false);
        titleCanvas.SetActive(true);
        gameCanvas.GetComponent<AudioManager>().StopBGM();
    }

    public void Pause(){
        if(!paused){
            Time.timeScale = 0;
            gameCanvas.GetComponent<AudioManager>().PauseBGM();
            pauseText.SetActive(true);
            GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "Unpause";
        }
        else{
            Time.timeScale = 1;
            gameCanvas.GetComponent<AudioManager>().PlayBGM();
            pauseText.SetActive(false);
            GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "Pause";
        }
        paused = !paused;
    }

    public void Unpause(){
        Time.timeScale = 1;
        gameCanvas.GetComponent<AudioManager>().PlayBGM();
        pauseText.SetActive(false);
        GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "Pause";
    }

    public void LoseGame(){
        Time.timeScale = 0;
        loseText.SetActive(true);
    }

    public void WinGame(){
        Time.timeScale = 0;
        winText.SetActive(true);
    }

    public void Restart(){
        loseText.SetActive(false);
        pauseText.SetActive(false);
        ResetScore();
        gameCanvas.GetComponent<GameGrid>().RestartGame();
    }

    public void UpdateLevel(int level){
        levelText.GetComponentInChildren<Text>().text = level.ToString();
    }

    public void UpdateLinesLeft(int lines){
        linesLeftText.GetComponentInChildren<Text>().text = lines.ToString();
    }

    public void UpdateScore(int count, int level){
        var score = Int32.Parse(scoreText.GetComponentInChildren<Text>().text);
        score += count*level*10;
        scoreText.GetComponentInChildren<Text>().text = score.ToString();
    }

    public void ResetScore(){
        scoreText.GetComponentInChildren<Text>().text = "0";
    }

    public void ResetLinesLeft(int lines){
        linesLeftText.GetComponentInChildren<Text>().text = lines.ToString();
    }

}
