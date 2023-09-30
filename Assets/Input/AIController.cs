using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AIController : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 5.0f;

    private Transform ballTransform;
    private void Start()
    {
        ballTransform = GameObject.Find("Ball").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Vector2 v2;
        _ = (transform.position.z > ballTransform.position.z) ? (v2 = Vector2.down) : (v2 = Vector2.up);

        transform.Translate(new Vector3(v2.y, v2.x, 0) * movementSpeed * Time.deltaTime);


    }
}
