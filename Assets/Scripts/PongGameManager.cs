using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PongGameManager : MonoBehaviour
{

    private int PointL = 0;
    private int PointR = 0;

    private static PongGameManager _instance;
    public static PongGameManager Instance { get { return _instance; } }
    [SerializeField]
    private GameObject P1;
    [SerializeField]
    private GameObject P2;
    [SerializeField]
    private GameObject Ball;
    [SerializeField]
    private PointDisplay PointDisplay;
    private Rigidbody _BallRigidBody;
    [SerializeField]
    private float paddleInitPos = -5.5f;

    [SerializeField]
    public float _speed = 3.0f;

    // Make sure there is only ever one PongGameManager
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
        //Instantiate(Ball, new Vector3(0, 0, -5.5f), Quaternion.identity);
        startBall();

    }

    public void AddPoint(int side)
    {
        if (side < 0)
        {
            PointR += 1;
        }
        else
        {
            PointL += 1;
        }
        PointDisplay.UpdateScore();
    }

    // Reset the Level
    public void RestartLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
        P1.transform.position = new Vector3(P1.transform.position.x, P1.transform.position.y, paddleInitPos);
        P2.transform.position = new Vector3(P2.transform.position.x, P2.transform.position.y, paddleInitPos);
        Ball.transform.position = new Vector3(0, 0, -5.5f);
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
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponent<Rigidbody>().AddForce(GenForceRandDir(), ForceMode.VelocityChange);
    }

    public Vector3 GenForceRandDir()
    {
        // Random Angle, Left or Right
        int angle = Random.Range(-2, 2) >= 1 ? -90 : 90;

        return new Vector3(Mathf.Sin(angle) * _speed, 0, Mathf.Cos(angle) * _speed);
    }



}
