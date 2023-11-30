using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        Ball Triggers this Volume indicating the round is lost.
        Detect which side of field lost.
            - Ball Vector.X was either Positive or Negative.
        Call GameManager Singleton
            - Iterate winning side's points
            - Reset Round
        */

        PongGameManager.Instance.AddPoint((int)other.transform.position.x);
        PongGameManager.Instance.PrintPoints();
        PongGameManager.Instance.RestartLevel();
    }

}
