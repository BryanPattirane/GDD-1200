using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    
    public bool gameOn = true;
    public GameObject gameOver;
    private Text gameOverText;
    public GameObject score;
    private Text scoreText;
    private string winnerIs;
    public GameState gameState;
    public string playerPreference;
    public string aIPreference;
    public CustomArray[] myArray;
    private GameObject[] objects;
    private GameObject[] menus;
    private AIController aiScript;
    private GameObject xButton;
    private GameObject oButton;
    private GameObject selectSign;

    
    private void Awake()
    {
        gameOverText = gameOver.GetComponent<Text>();
        scoreText = score.GetComponent<Text>();
        gameOverText.enabled = false;
        gameOn = true;
        objects = GameObject.FindGameObjectsWithTag("Pieces");
        menus = GameObject.FindGameObjectsWithTag("UI");
        aiScript = GameObject.FindGameObjectWithTag("AI").GetComponent<AIController>();
        gameState = new GameState();
        winnerIs = null;
        gameOverText.enabled = false;
        xButton = GameObject.FindGameObjectWithTag("X");
        oButton = GameObject.FindGameObjectWithTag("O");
        selectSign = GameObject.FindGameObjectWithTag("SelectSign");
        xButton.GetComponent<Image>().color = Color.green;
        oButton.GetComponent<Image>().color = Color.yellow;
        foreach (var obj in menus)
        {
            obj.GetComponent<Button>().enabled = false;
            obj.GetComponent<Image>().enabled = false;
        }
        foreach (var obj in objects)
        {
            obj.GetComponent<Button>().enabled = false;
        }
        

    }
    public void ChooseX()
    {
        playerPreference = "X";
        aIPreference = "O";
        oButton.GetComponent<Button>().enabled = false;
        xButton.GetComponent<Button>().enabled = false;
        foreach (var obj in objects)
        {
            obj.GetComponent<Button>().enabled = true;
        }
        selectSign.gameObject.SetActive(false);
    }
    public void ChooseO()
    {
        playerPreference = "O";
        aIPreference = "X";
        oButton.GetComponent<Button>().enabled = false;
        xButton.GetComponent<Button>().enabled = false;
        foreach (var obj in objects)
        {
            obj.GetComponent<Button>().enabled = true;
        }
        selectSign.gameObject.SetActive(false);
        //If player choose O then AI will go first
        aiScript.AIMove();
    }
    // Update is called once per frame
    void Update()
    {
        
        //Handles winning condition
        if (!gameOn)
        {
            scoreText.text = winnerIs;
            gameOverText.enabled = true;
            //Disabled all objects from onClick
            foreach (var obj in objects)
            {
                obj.GetComponent<Button>().enabled = false;
            }
            foreach (var obj in menus)
            {
                obj.GetComponent<Button>().enabled = true;
                obj.GetComponent<Image>().enabled = true;
            }

        }
        
        
    }

    public void ResetGame()
    {
        //Reset all objects to start conditions
        gameOn = true;
        scoreText.text = null;
        gameOverText.enabled = false;
        foreach (var obj in menus)
        {
            obj.GetComponent<Button>().enabled = false;
            obj.GetComponent<Image>().enabled = false;
        }
        foreach (var obj in objects)
        {
            obj.GetComponent<Text>().text = "";
            obj.GetComponent<Button>().enabled = false;
            //obj.GetComponent<Image>().color = Color.white;
        }

        foreach (var items in myArray)
        {
            
            items.gameObject.GetComponentInParent<Image>().color = Color.white;
            
        }
        oButton.GetComponent<Button>().enabled = true;
        xButton.GetComponent<Button>().enabled = true;
        selectSign.gameObject.SetActive(true);

    }

    public void WinCheck(string playerType)
    {
        int openTiles = 0;
        //for test
        List<int> test = new List<int>();
        for (int i = 0; i < myArray.Length; i++)
        {
            if (myArray[i].gameObject.GetComponent<Text>().text == "")
            {
                openTiles++;
                
            }
        }

        
        
            int playerScore = 0;
            int playerMoveCount = 0;
        string valCheck;//= playerPreference;

            //Check which player just made the move
            if (playerType == "Player")
            {
                valCheck = playerPreference;
            }
            else
            {
                valCheck = aIPreference;
            }

            //Checks if win within 3 moves
            foreach (var items in myArray)
            {
                if (items.gameObject.GetComponent<Text>().text == valCheck)
                {
                    playerScore += items.value;
                    playerMoveCount++;
                }
            }
            //Resolve game if won under 3 moves
            if (playerScore == 15 && playerMoveCount == 3)
            {
                gameOn = false;
                foreach(var items in myArray)
                {
                    if (items.gameObject.GetComponent<Text>().text == valCheck && valCheck=="O")
                    {
                        items.gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    }
                    else if (items.gameObject.GetComponent<Text>().text == valCheck && valCheck == "X")
                    {
                        items.gameObject.GetComponentInParent<Image>().color = Color.green;
                    }
            }
            }
            //Resolve for 4+ moves
            else if (playerScore > 15)
            {

                //Checks all rows
                if (myArray[0].gameObject.GetComponent<Text>().text == valCheck && myArray[1].gameObject.GetComponent<Text>().text == valCheck && myArray[2].gameObject.GetComponent<Text>().text == valCheck)
                {
                    gameOn = false;
                    

                
            if (valCheck == "O")
                {
                    myArray[0].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[1].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[2].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                }
                else
                {
                    myArray[0].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[1].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[2].gameObject.GetComponentInParent<Image>().color = Color.green;
            }

            }

            if (myArray[3].gameObject.GetComponent<Text>().text == valCheck && myArray[4].gameObject.GetComponent<Text>().text == valCheck && myArray[5].gameObject.GetComponent<Text>().text == valCheck)
                {
                    gameOn = false;
                if (valCheck == "O")
                {
                    myArray[3].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[4].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[5].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                }
                else
                {
                    myArray[3].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[4].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[5].gameObject.GetComponentInParent<Image>().color = Color.green;
                }
            }
                if (myArray[6].gameObject.GetComponent<Text>().text == valCheck && myArray[7].gameObject.GetComponent<Text>().text == valCheck && myArray[8].gameObject.GetComponent<Text>().text == valCheck)
                {
                    gameOn = false;
                if (valCheck == "O")
                {
                    myArray[6].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[7].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[8].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                }
                else
                {
                    myArray[6].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[7].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[8].gameObject.GetComponentInParent<Image>().color = Color.green;
                }
            }
                //Checks all columns
                if (myArray[0].gameObject.GetComponent<Text>().text == valCheck && myArray[3].gameObject.GetComponent<Text>().text == valCheck && myArray[6].gameObject.GetComponent<Text>().text == valCheck)
                {
                    gameOn = false;
                if (valCheck == "O")
                {
                    myArray[0].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[3].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[6].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                }
                else
                {
                    myArray[0].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[3].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[6].gameObject.GetComponentInParent<Image>().color = Color.green;
                }
            }
                if (myArray[1].gameObject.GetComponent<Text>().text == valCheck && myArray[4].gameObject.GetComponent<Text>().text == valCheck && myArray[7].gameObject.GetComponent<Text>().text == valCheck)
                {
                    gameOn = false;
                    if (valCheck == "O")
                    {
                        myArray[1].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                        myArray[4].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                        myArray[7].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    }
                    else
                    {
                        myArray[1].gameObject.GetComponentInParent<Image>().color = Color.green;
                        myArray[4].gameObject.GetComponentInParent<Image>().color = Color.green;
                        myArray[7].gameObject.GetComponentInParent<Image>().color = Color.green;
                    }

                }
                if (myArray[2].gameObject.GetComponent<Text>().text == valCheck && myArray[5].gameObject.GetComponent<Text>().text == valCheck && myArray[8].gameObject.GetComponent<Text>().text == valCheck)
                {
                    gameOn = false;
                if (valCheck == "O")
                {
                    myArray[2].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[5].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[8].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                }
                else
                {
                    myArray[2].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[5].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[8].gameObject.GetComponentInParent<Image>().color = Color.green;
                }
            }
                //Check both Diagonals
                if (myArray[2].gameObject.GetComponent<Text>().text == valCheck && myArray[4].gameObject.GetComponent<Text>().text == valCheck && myArray[6].gameObject.GetComponent<Text>().text == valCheck)
                {
                    gameOn = false;
                if (valCheck == "O")
                {
                    myArray[2].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[4].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[6].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                }
                else
                {
                    myArray[2].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[4].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[6].gameObject.GetComponentInParent<Image>().color = Color.green;
                }
            }
                if (myArray[0].gameObject.GetComponent<Text>().text == valCheck && myArray[4].gameObject.GetComponent<Text>().text == valCheck && myArray[8].gameObject.GetComponent<Text>().text == valCheck)
                {
                    gameOn = false;
                if (valCheck == "O")
                {
                    myArray[0].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[4].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                    myArray[8].gameObject.GetComponentInParent<Image>().color = Color.yellow;
                }
                else
                {
                    myArray[0].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[4].gameObject.GetComponentInParent<Image>().color = Color.green;
                    myArray[8].gameObject.GetComponentInParent<Image>().color = Color.green;
                }
            }

            }
            //return playerCount;
            if (!gameOn && playerType == "Player")
            {
                winnerIs = "You Won!";
                Debug.Log("-------------------");
            }
            else if (!gameOn && playerType == "AI")
            {
                winnerIs = "You Lost!";
                Debug.Log("-------------------");
            }
        
        else if (!gameOn || openTiles==0)
        {
            gameOn = false;
            winnerIs = "You Tied!";
            Debug.Log("-------------------");
        }
        Debug.Log("End of Win Check");

    }



//Custom Array Set Up
public void SetValue(int index, CustomArray customArray)
{
    myArray[index] = customArray;
}
public CustomArray GetValue(int index)
{
    return myArray[index];
}


}
[System.Serializable]
public class CustomArray
{
    public int value;
    public GameObject gameObject;
}
