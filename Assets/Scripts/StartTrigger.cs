using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{

    [SerializeField]
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Beginning of the game, instantiate playerPrefab
        // at this.gameObject.transform.position, transform.rotation
        MazeGameManager.Instance.SetPlayer(Instantiate(playerPrefab, transform.position, transform.rotation).GetComponent<Player>());
    }
}
