using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameState State { get { return this.state; } }

    private GameState state;
    private List<IGameListener> listeners = new();
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
    }
}
