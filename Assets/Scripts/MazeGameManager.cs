using MazeAssignment;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MazeGameManager : MonoBehaviour
{
    private static MazeGameManager _instance;
    public static MazeGameManager Instance { get { return _instance; } }


    public int score = 0;

    [SerializeField]
    private Player player;
    [SerializeField]
    private AI _ai;
    private List<List<Point>> maze;

    [SerializeField]
    private GameState _gameState = GameState.Maze;

    [SerializeField]
    private GameObject pauseMenu;

    // Make sure there is only ever one MazeGameManager
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public void SetFloor(List<List<Point>> floor)
    {
        maze = floor;
    }

    public void SetAI(AI ai)
    {
        _ai = ai;
    }

    public void SetPauseMenu(GameObject menu)
    {
        pauseMenu = menu;
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetAI()
    {
        _ai.transform.position = maze[(int)(Random.value * maze.Count)][(int)(Random.value * maze.Count)].pos;
        _ai.ResetAI();
    }

    public void TogglePause()
    {
        _gameState = (_gameState == GameState.Paused) ? GameState.Maze : GameState.Paused;
        CheckGameState();
    }

    public void LeaveGame()
    {
        _gameState = GameState.None;
        CheckGameState();
    }

    private void CheckGameState()
    {
        switch (_gameState)
        {
            case GameState.None:
                gameStateNone();
                break;
            case GameState.Maze:
                gameStateMaze();
                break;
            case GameState.Paused:
                gameStatePause();
                break;
        }
    }

    void gameStateMaze()
    {
        Debug.Log("Maze");
        Time.timeScale = 1f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        player.enabled = true;
        pauseMenu.SetActive(false);
    }

    void gameStatePause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        player.enabled = false;
        pauseMenu.SetActive(true);

    }

    void gameStateNone()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }
}
