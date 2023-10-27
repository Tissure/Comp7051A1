using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    [SerializeField]
    public GameObject PanelPrefab;

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
        // Check for Player Tag
        // If Player, Instantiate Panel w/ Text ("You Win")

        if (other.transform.gameObject.tag == "Player") 
        {
            // We know for sure the other GameObject is a player, now instantiate the PanelPrefab as a child of Canvas
            Instantiate(PanelPrefab);
        }

    }

}
