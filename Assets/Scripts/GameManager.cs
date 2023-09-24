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

    [SerializeField]
    private GameObject Ball;
    private Rigidbody _BallRigidBody;

    [SerializeField]
    public float _speed = 3.0f;

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
        _BallRigidBody = Ball.GetComponent<Rigidbody>();
        Instantiate(Ball, new Vector3(0, 0, -5.5f), Quaternion.identity);
        startBall();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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
        startBall();
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

    public void startBall()
    {
        Debug.Log("ForceAdd");
        GameObject.Find("Ball(Clone)").GetComponent<Rigidbody>().AddForce(GenForceRandDir(), ForceMode.VelocityChange);
    }

    public Vector3 GenForceRandDir()
    {
        // Random Angle, Left or Right
        int angle = Random.Range(-2, 2) >= 1 ? -90 : 90;

        return new Vector3(Mathf.Sin(angle) * _speed, 0, Mathf.Cos(angle) * _speed);
    }

}