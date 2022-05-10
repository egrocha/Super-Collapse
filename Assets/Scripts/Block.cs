using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public string color;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void destroyBlock(){
        
    }

    void OnMouseDown(){
        var split = this.name.Split('_');
        var color = split[0];
        var x = Int32.Parse(split[1]);
        var y = Int32.Parse(split[2]);
        var gameCanvas = GameObject.Find("GameCanvas");
        gameCanvas.GetComponent<GameGrid>().DestroyBlocksCheck(color, x, y);
    }
}
