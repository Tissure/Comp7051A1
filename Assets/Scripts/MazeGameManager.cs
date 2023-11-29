using MazeAssignment;
using System.Collections.Generic;
using UnityEngine;

public class MazeGameManager : MonoBehaviour
{
    private static MazeGameManager _instance;
    public static MazeGameManager Instance { get { return _instance; } }


    public int score = 0;

    [SerializeField]
    private AI _ai;
    private List<List<Point>> maze;

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

    public void SetFloor(List<List<Point>> floor)
    {
        maze = floor;
    }

    public void SetAI(AI ai)
    {
        _ai = ai;
    }

    public void IncrementScore()
    {
        score = score + 1;
    }

    public void ResetAI()
    {
        _ai.transform.position = maze[(int)(Random.value * maze.Count)][(int)(Random.value * maze.Count)].pos;
        _ai.ResetAI();
    }
}
