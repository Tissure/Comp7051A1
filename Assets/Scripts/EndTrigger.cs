using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    [SerializeField]
    public GameObject PanelPrefab;

    [SerializeField]
    public GameObject canvas;
    private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        child = Instantiate(PanelPrefab, canvas.gameObject.transform);
        child.transform.SetParent(canvas.transform);
        child.SetActive(false);
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

            child.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Destroy(gameObject);

        }

    }

}
