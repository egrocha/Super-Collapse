using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioSource bgm;
    public Toggle bgmToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBGM(){
        if(bgmToggle.isOn){
            bgm.Play();
        }
    }

    public void PauseBGM(){
        bgm.Pause();
    }

    public void StopBGM(){
        bgm.Stop();
    }

}
