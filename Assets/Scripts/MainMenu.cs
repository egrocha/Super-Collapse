using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject gameCanvas;
    public GameObject titleCanvas;
    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame(){
        titleCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        if(gameStarted){
            gameCanvas.GetComponent<GameCanvas>().Unpause();
            gameCanvas.GetComponent<GameGrid>().RestartGame();
            gameCanvas.GetComponent<AudioManager>().PlayBGM();
        }
    }

    public void QuitGame(){
        //Application.Quit();
    }

    public void GameStarted(){
        gameStarted = true;
    }

}
