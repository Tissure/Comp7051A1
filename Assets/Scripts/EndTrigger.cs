using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    [SerializeField]
    public GameObject PanelPrefab;

    [SerializeField]
    public GameObject canvas;

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

        if (other.tag == "Player")
        {
            // We know for sure the other GameObject is a player, now instantiate the PanelPrefab as a child of Canvas

            GameObject child = Instantiate(PanelPrefab, canvas.gameObject.transform);
            child.transform.SetParent(canvas.transform);

        }

    }

}
