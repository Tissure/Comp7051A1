using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int PointL = 0;
    private int PointR = 0;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    // Make sure there is only ever one GameManager
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(int side)
    {
        if (side > 0)
        {
            PointR += 1;
        }
        else
        {
            PointL += 1;
        }
    }

    // Reset the Level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Testing to see if it works.
    public void PrintPoints()
    {
        Debug.Log(PointL.ToString() + " / " + PointR.ToString());
    }

    public int getPointL()
    {
        return PointL; 
    }

    public int getPointR()
    {
        return PointR;
    }


}
