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
    public List<IGameListener> listeners = new List<IGameListener>();
    private void Awake()
    {
        AddListener(GetComponentInChildren<BattleManager>());
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
        Debug.Log(state);
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeGameState(GameState.PLAYING);
            foreach (var listner in listeners)
            {
                if (listner is IGameStartListener startListener)
                {
                    startListener.OnGameStarted();
                }
            }
            ChangeGameState(GameState.PLAYING);
        }
    }
}
