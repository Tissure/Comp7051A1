using MazeAssignment;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MazeGameManager : MonoBehaviour, ISaveable
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
    private MazeGenerator mazeGenerator;
    [SerializeField]
    private GameState _gameState = GameState.Maze;
    [SerializeField]
    private AudioSource audioBGM;
    [SerializeField]
    private HudUI HUD;
    [SerializeField]
    private GameObject pauseMenu;

    // Make sure there is only ever one MazeGameManager
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        NewGame();
    }

    private void Update()
    {
        float dist = Vector3.Distance(player.transform.position, _ai.transform.position);
        SetMusicVolume(dist);
    }

    public void NewGame()
    {
        mazeGenerator.GenerateMaze();
        SetPlayer(maze[0][0].pos);
        SetAI(maze[maze.Count / 2][maze.Count / 2].pos);
    }
    public void SaveGame()
    {
        SaveDataManager.SaveJsonData(new ISaveable[] { this, _ai, player });
    }

    public void LoadGame()
    {
        SaveDataManager.LoadJsonData(new ISaveable[] { this, _ai, player });
    }

    public void SetMusicVolume(float volume)
    {
        audioBGM.volume = volume;
    }

    public void SetPlayer(Vector3 p)
    {
        player.transform.position = p;
    }

    public void SetFloor(List<List<Point>> floor)
    {
        maze = floor;
    }

    public void SetAI(Vector3 ai)
    {
        _ai.transform.position = ai;
        _ai.gameObject.SetActive(true);
    }

    public void SetPauseMenu(GameObject menu)
    {
        pauseMenu = menu;
    }

    public void IncrementScore()
    {
        score++;
        HUD.UpdateScore(score);
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

    public void PopulateSaveData(SaveData a_SaveData)
    {
        a_SaveData.state = Random.state;

    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        throw new System.NotImplementedException();
    }
}
