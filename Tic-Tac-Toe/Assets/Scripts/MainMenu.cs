using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameState gameState;
    private GameObject instructions;

    private void Awake()
    {

        instructions = GameObject.FindGameObjectWithTag("Instructions");
        instructions.gameObject.SetActive(false);
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainTicTac");
    }
    public void Instructions()
    {
        instructions.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }



    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            instructions.gameObject.SetActive(false);
        }
    }
}
