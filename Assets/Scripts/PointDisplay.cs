using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointDisplay : MonoBehaviour
{

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        text.text = PongGameManager.Instance.getPointL().ToString() + "    " + PongGameManager.Instance.getPointR().ToString();
    }
}
