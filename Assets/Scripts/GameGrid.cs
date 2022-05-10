using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random=UnityEngine.Random;

public class GameGrid : MonoBehaviour
{

    private int width = 12;
    private int height = 14;
    private int gap = 48;
    private int posX = -445;
    private int posY = -295;
    private int stripPosX = -445;
    private int stripPosY = -335;
    private int linesLeft = 100;
    private int levelCounter = 0;
    private int level = 1;
    private double cycle = 0.55;
    private double nextActionTime;

    private GameObject block;
    private GameObject strip;

    private string[][] grid;
    private List<(int, int)> checkedBlocks = new List<(int, int)>();
    private List<string> blocksToDestroy = new List<string>();
    private List<int> changedColumns = new List<int>();
    private List<string> stripColors = new List<string>();

    public GameObject gameCanvas;
    public GameObject titleCanvas;
    public GameObject winCanvas;
    public GameObject bluePrefab;
    public GameObject greenPrefab;
    public GameObject redPrefab;
    public GameObject blueStripPrefab;
    public GameObject greenStripPrefab;
    public GameObject redStripPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InitiateGrid();
        CreateGrid();
        gameCanvas.GetComponent<AudioManager>().PlayBGM();
        titleCanvas.GetComponent<MainMenu>().GameStarted();
        nextActionTime = Time.time + cycle;
    }

    // Update is called once per frame
    void Update(){
        if(Time.time > nextActionTime & linesLeft > 0){
            nextActionTime += cycle;
            SpawnRandomStrip();
            if(stripColors.Count == 12){
                AddBlockLine();
                DeleteStrips();
                levelCounter++;
                if(levelCounter == 10){
                    levelCounter = 0;
                    level++;
                    cycle = cycle - 0.05;
                    gameCanvas.GetComponent<GameCanvas>().UpdateLevel(level);
                }
                linesLeft--;
                gameCanvas.GetComponent<GameCanvas>().UpdateLinesLeft(linesLeft);
                if(linesLeft == 0){
                    winCanvas.GetComponent<WinScreen>().WinGame();
                }
                stripColors.Clear();
            }
        }
    }

    private void InitiateGrid(){
        grid = new string[height][];
        for(int y = 0; y < width; y++){
            grid[y] = new string[height];
        }
    }

    private void CreateGrid(){
        posX = -445;
        posY = -295;
        for(int y = 0; y < 4; y++){
            for(int x = 0; x < width; x++){
                SpawnRandomBlock(x, y);
                posX = posX + gap;
            }
            posX = -445;
            posY = posY + gap;
        }
    }

    public void RestartGame(){
        var wasPaused = false;
        if(Time.timeScale == 0){
            wasPaused = true;
        }
        Time.timeScale = 0;
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                if(grid[x][y] != null){
                    Destroy(GameObject.Find(grid[x][y]+"_"+x+"_"+y));
                    grid[x][y] = null;
                }
            }
        }
        DeleteStrips();
        ResetVars();
        CreateGrid();
        if(!wasPaused){
            Time.timeScale = 1;
        }
        nextActionTime = Time.time + cycle;
    }

    private void ResetVars(){
        cycle = 0.55;
        level = 1;
        stripPosX = -445;
        stripPosY = -335;
        linesLeft = 100;
        stripColors.Clear();
        gameCanvas.GetComponent<GameCanvas>().ResetScore();
        gameCanvas.GetComponent<GameCanvas>().ResetLinesLeft(linesLeft);
    }

    private void SpawnRandomStrip(){
        int random = Random.Range(1, 100);
        var color = "";
        var count = stripColors.Count;
        if(1 <= random && random <= 33){
            strip = Instantiate(blueStripPrefab, new Vector3(stripPosX+(gap*count), stripPosY, -2), Quaternion.identity);
            color = "blue";
        }
        if(33 <= random && random <= 66){
            strip = Instantiate(greenStripPrefab, new Vector3(stripPosX+(gap*count), stripPosY, -2), Quaternion.identity);
            color = "green";
        }
        if(66 <= random && random <= 99){
            strip = Instantiate(redStripPrefab, new Vector3(stripPosX+(gap*count), stripPosY, -2), Quaternion.identity);
            color = "red";
        }
        strip.transform.SetParent(gameCanvas.transform, false);
        strip.name = color+"_"+count;
        stripColors.Add(strip.name);
    }

    private void AddBlockLine(){
        var color = "";
        for(int y = height-1; y > 0; y--){
            for(int x = 0; x < width; x++){
                if(y == height-1 & grid[x][y] != null){
                    gameCanvas.GetComponent<GameCanvas>().LoseGame();
                }
                if(grid[x][y-1] != null){
                    grid[x][y] = grid[x][y-1];
                    block = GameObject.Find(grid[x][y-1]+"_"+x+"_"+(y-1));
                    if(block != null){
                        var currX = block.transform.position.x;
                        var currY = block.transform.position.y;
                        block.name = grid[x][y]+"_"+x+"_"+y;
                        block.transform.position = new Vector3(currX, currY+gap, -2);
                    }
                }
            }
        }
        for(int x = 0; x < width; x++){
            color = stripColors[x].Split('_')[0];
            var newX = -445;
            var newY = -295;
            if(color == "blue"){
                block = Instantiate(bluePrefab, new Vector3(newX+(x*gap), newY, -2), Quaternion.identity);
            }
            else if(color == "green"){
                block = Instantiate(greenPrefab, new Vector3(newX+(x*gap), newY, -2), Quaternion.identity);
            }
            else{
                block = Instantiate(redPrefab, new Vector3(newX+(x*gap), newY, -2), Quaternion.identity);
            }
            block.transform.SetParent(gameCanvas.transform, false);
            block.name = color + "_" + x + "_" + 0;
            grid[x][0] = color;
        }
    }

    private void DeleteStrips(){
        for(int i = 0; i < stripColors.Count; i++){
            Destroy(GameObject.Find(stripColors[i]));
        }
    }

    private void SpawnRandomBlock(int x, int y){
        int random = Random.Range(1, 100);
        var color = "";
        if(1 <= random && random <= 33){
            block = Instantiate(bluePrefab, new Vector3(posX, posY, -2), Quaternion.identity);
            color = "blue";
        }
        if(33 <= random && random <= 66){
            block = Instantiate(greenPrefab, new Vector3(posX, posY, -2), Quaternion.identity);
            color = "green";
        }
        if(66 <= random && random <= 99){
            block = Instantiate(redPrefab, new Vector3(posX, posY, -2), Quaternion.identity);
            color = "red";
        }
        block.transform.SetParent(gameCanvas.transform, false);
        block.name = color + "_" + x + "_" + y;
        grid[x][y] = color;
    }

    public void DestroyBlocksCheck(string color, int x, int y){
        CheckBlock(color, x, y);
        CheckNearbyBlocks(color, x, y);
        if(blocksToDestroy.Count >= 3){
            gameCanvas.GetComponent<SFXManager>().PlaySFX();
            DestroyBlocks();
            checkedBlocks.Clear();
            blocksToDestroy.Clear();
            DropBlocks();
            CenterBlocks();
        }
        checkedBlocks.Clear();
        blocksToDestroy.Clear();
        changedColumns.Clear();
    }

    private void CheckNearbyBlocks(string color, int x, int y){
        if(x < width - 1){
            CheckBlock(color, x+1, y);
        }
        if(x > 0){
            CheckBlock(color, x-1, y);
        }
        if(y < height - 1){
            CheckBlock(color, x, y+1);
        }
        if(y > 0){
            CheckBlock(color, x, y-1);
        }
    }

    private void CheckBlock(string color, int x, int y){
        if(!checkedBlocks.Contains((x, y))){
            checkedBlocks.Add((x, y));
            if(color == grid[x][y]){
                blocksToDestroy.Add(color+"_"+x+"_"+y);
                if(!changedColumns.Contains(x)){
                    changedColumns.Add(x);
                }
                CheckNearbyBlocks(color, x, y);
            }
        }
        return;
    }

    private void DestroyBlocks(){
        var x = 0;
        var y = 0;
        gameCanvas.GetComponent<GameCanvas>().UpdateScore(blocksToDestroy.Count, level);
        for(int i = 0; i < blocksToDestroy.Count; i++){
            var split = blocksToDestroy[i].Split('_');
            x = Int32.Parse(split[1]);
            y = Int32.Parse(split[2]);
            grid[x][y] = null;
            Destroy(GameObject.Find(blocksToDestroy[i]));
        }
    }

    private void DropBlocks(){
        for(int x = 0; x < width; x++){
            for(int y = 1; y < height; y++){
                AdjustYPosition(x, y);
            }
        }
    }

    private void AdjustYPosition(int x, int y){
        block = GameObject.Find(grid[x][y]+"_"+x+"_"+y);
        if(block != null){
            while(y > 0 && grid[x][y] != null && grid[x][y-1] == null){
                var currX = block.transform.position.x;
                var currY = block.transform.position.y;
                grid[x][y-1] = grid[x][y];
                grid[x][y] = null;
                block.transform.position = new Vector3(currX, currY-gap, -2);
                block.name = grid[x][y-1]+"_"+x+"_"+(y-1);
                y--;
            }
        }
    }
    
    private void CenterBlocks(){
        var center = 6;
        var leftSide = new List<int>();
        var rightSide = new List<int>();
        for(int i = 0; i < changedColumns.Count; i++){
            var col = changedColumns[i];
            if(IsColumnEmpty(col)){
                if(col < center){
                    ShiftBlocksRight(col);
                }
                else{
                    ShiftBlocksLeft(col);
                }
            }
        }
    }

    private bool IsColumnEmpty(int col){
        if(grid[col][0] == null){
            return true;
        }
        return false;
    }

    private void ShiftBlocksRight(int col){
        for(int x = col; x >= 0; x--){
            if(x < col & IsColumnEmpty(x) & !IsColumnEmpty(x+1)) x = x+1;
            for(int y = 0; y < height; y++){
                if(grid[x][y] != null){
                    block = GameObject.Find(grid[x][y]+"_"+x+"_"+y);
                    if(block){
                        var currX = block.transform.position.x;
                        var currY = block.transform.position.y;
                        grid[x+1][y] = grid[x][y];
                        grid[x][y] = null;
                        block.transform.position = new Vector3(currX+gap, currY, -2);
                        block.name = grid[x+1][y]+"_"+(x+1)+"_"+y;
                    }
                }
            }
        }
    }

    private void ShiftBlocksLeft(int col){
        for(int x = col; x < width; x++){
            for(int y = 0; y < height; y++){
                if(grid[x][y] != null){
                    block = GameObject.Find(grid[x][y]+"_"+x+"_"+y);
                    if(block){
                        var currX = block.transform.position.x;
                        var currY = block.transform.position.y;
                        grid[x-1][y] = grid[x][y];
                        grid[x][y] = null;
                        block.transform.position = new Vector3(currX-gap, currY, -2);
                        block.name = grid[x-1][y]+"_"+(x-1)+"_"+y;
                    }
                }
            }
        }
    }
}
