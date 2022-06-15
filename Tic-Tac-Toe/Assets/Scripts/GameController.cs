using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public Text gameText;
    private string textValue;
    private string playerType;
    public GameObject stateObject;
    GameState gameState;
    [SerializeField] AIController aiScript;
    private bool aIFirstMove;

    public string playerMoves;
    // Start is called before the first frame update

    private void Awake()
    {
        aIFirstMove = true;
        playerMoves = null;
        gameState = stateObject.GetComponent<GameState>();
        textValue = null;
        gameText = GetComponent<Text>();
        gameText.text = textValue;
        aiScript = GameObject.FindGameObjectWithTag("AI").GetComponent<AIController>();
        
    }
    void Start()
    {
    }

    public void ChangeText()
    {
        //Checks if empty then proceed to flip to player's chosen sign
        if (gameText.GetComponent<Text>().text == "")
        {
            gameText.GetComponent<Text>().text = gameState.playerPreference;
            playerType = "Player";
            
            //Checks win condition after every move
            gameState.WinCheck(playerType);
            //If game is still not won by player's last move, then AI turn
            if (gameState.gameOn)
            {
                aiScript.AIMove();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
