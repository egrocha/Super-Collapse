using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{

    public AudioSource sfx;
    public Toggle sfxToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(){
        if(sfxToggle.isOn){
            sfx.Play();
        }
    }

}
