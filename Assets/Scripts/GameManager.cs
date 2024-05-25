using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum GameState
{
    OFF = 0,
    PLAYING = 1,
    PAUSED = 2,
    FINISHED = 3
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action onEndGame;
    public GameState State { get { return this.state; } }

    private GameState state;
    public List<IGameListener> listeners = new List<IGameListener>();
    private void Awake()
    {
        instance = this;
        onEndGame += EndGame;
    }
    private void OnDisable()
    {
        onEndGame -= EndGame;
    }
    public void AddListener(IGameListener listener)
    {
        listeners.Add(listener);
    }
    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.OFF:
                break;
            case GameState.PLAYING:
                break;
            case GameState.PAUSED:
                break;
            case GameState.FINISHED:
                break;
        }
        this.state = state;
    }
    private void Update()
    {
    }
    private void EndGame()
    {
        ChangeGameState(GameState.FINISHED);
        foreach (var listner in listeners)
        {
            if (listner is IGameFinishedListener startListener)
            {
                startListener.OnGameFinished();
            }
        }
        Debug.Log("Game Finished");
    }
    public void StartGame()
    {
        foreach (var listner in listeners)
        {
            if (listner is IGameStartListener startListener)
            {
                startListener.OnGameStarted();
            }
        }
        ChangeGameState(GameState.PLAYING);
        Debug.Log("Game Started");
    }
}
