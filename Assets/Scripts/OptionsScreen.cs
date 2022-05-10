using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScreen : MonoBehaviour
{

    public GameObject optionsScreen;
    public GameObject gameCanvas;
    public GameObject titleCanvas;
    private bool restoreTitle = false;
    private bool restoreGame = false;

    public void OpenOptions(GameObject oldObject){
        if(gameCanvas.activeSelf){
            gameCanvas.GetComponent<GameCanvas>().Pause();
            gameCanvas.SetActive(false);
            restoreGame = true;
        }
        else if(titleCanvas.activeSelf){
            titleCanvas.SetActive(false);
            restoreTitle = true;
        }
        optionsScreen.SetActive(true);
    }

    public void CloseOptions(){
        if(restoreGame){
            gameCanvas.SetActive(true);
            gameCanvas.GetComponent<GameCanvas>().Unpause();
            restoreGame = false;
        }
        if(restoreTitle){
            titleCanvas.SetActive(true);
            restoreTitle = false;
        }
        optionsScreen.SetActive(false);
    }

}
