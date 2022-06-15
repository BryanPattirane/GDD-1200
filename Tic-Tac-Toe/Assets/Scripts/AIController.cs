using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{

    
    private GameState gameState;
    List<int> availableTiles = new List<int>();
    private CustomArray[] array;
    private string aIPiece;
    // Start is called before the first frame update
    private void Awake()
    {
        //Grabs gamestate script component
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();

        //Let AI sign be the oposite of player's
        if(gameState.playerPreference == "X")
        {
            aIPiece = "X";
        }
        else
        {
            aIPiece = "O";
        }
        
    }

    private void Start()
    {
        
    }
    public void CreatePossibleMoves()
    {
        
        string debugMoves = null;
        //Clear out the list first on every turn
        availableTiles.Clear(); 
        //Debug.ClearDeveloperConsole(); 

        for(int i = 0;i<array.Length;i++)
        {
            if(array[i].gameObject.GetComponent<Text>().text == "")
            {
                //Debuggin purposes, to check what's available for AI to choose from
                debugMoves = debugMoves + ","+i.ToString();
                //Add all available open tiles to be chosen on list
                availableTiles.Add(i);
            }
        }
        Debug.Log("Adding: " + debugMoves);
        Debug.Log("Available Tiles: "+availableTiles.Count.ToString());
    }    
    public void AIMove()
    {
        //Checks where AI can place its piece
        CreatePossibleMoves();
        //Does a chekc if there are still open tiles to choose from
        if (availableTiles.Count > 0)
        {
            int index = 0;
            //BlockWin(index);
            //Randomly choose the index to place piece at
            index = Random.Range(0, availableTiles.Count);
            //Debug.Log(index);
            //Places the piece
            array[availableTiles[index]].gameObject.GetComponent<Text>().text = gameState.aIPreference;
            //Check if the last move triggers a win condition
            gameState.WinCheck("AI");
        }
    }
    public int BlockWin(int index)
    {
        ///Purpose of this method is to the AI to play defensively and find all possible move that can be blocked from creating a 3
        int takenScore = 0;
        if (gameState.myArray[0].gameObject.GetComponent<Text>().text == "X")
        {
            takenScore += gameState.myArray[0].value;
        }
        else if (gameState.myArray[1].gameObject.GetComponent<Text>().text == "X")
        {
            takenScore += gameState.myArray[1].value;
        }
        else if (gameState.myArray[2].gameObject.GetComponent<Text>().text == "X")
        {
            takenScore += gameState.myArray[2].value;
        }
        index = takenScore - 15;
        
        return index;

    }

    // Update is called once per frame
    void Update()
    {
        array = gameState.myArray;
    }
}
