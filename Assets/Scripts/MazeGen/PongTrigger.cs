using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PongTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MazeGameManager.Instance.SetPlayer(new (transform.position.x - 1, transform.position.y, transform.position.z - 1));
            MazeGameManager.Instance.SaveGame();
            SceneManager.LoadScene("VsAiScene");
        }
    }

}
