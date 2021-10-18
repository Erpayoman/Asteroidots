using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMono : MonoBehaviour
{
    public static GameManagerMono instance;

    public GameObject startUI, playingUI, gameOverUI;

    public enum GameState { None,Start, Playing, GameOver };

    private GameState currentState = GameState.None;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CurrentState = GameState.Start;
    }

    public GameState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
            switch(currentState)
            {
                case GameState.Start:

                    startUI.SetActive(true);
                    playingUI.SetActive(false);
                    gameOverUI.SetActive(false);

                break;
                case GameState.Playing:

                    startUI.SetActive(false);
                    playingUI.SetActive(true);
                    gameOverUI.SetActive(false);

                break;
                case GameState.GameOver:
                    startUI.SetActive(false);
                    playingUI.SetActive(false);
                    gameOverUI.SetActive(true);
                break;
            }
        }
    }
    public void OnClickStartGame()
    {
        CurrentState = GameState.Playing;
    }
}
