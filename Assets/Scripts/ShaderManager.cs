using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{

    private static ShaderManager _instance;
    public static ShaderManager Instance { get { return _instance; } }

    public enum _ActiveShader { Nothing, DayShader, NightShader, FogShader};
    public bool isFlashlightOn = false;

    [SerializeField]
    public Material _DayShader;

    [SerializeField]
    public Material _NightShader;

    [SerializeField]
    public Material _FogShader;

    [SerializeField]
    public GameObject _Flashlight;

    [SerializeField]
    public Camera _Camera;

    // Make sure there is only ever one GameManager
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        // Do stuff

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _Camera.GetComponent<CameraShaderApplicator>().wipeImageEffect();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _Camera.GetComponent<CameraShaderApplicator>().setImageEffect(_FogShader);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _Camera.GetComponent<CameraShaderApplicator>().setImageEffect(_DayShader);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _Camera.GetComponent<CameraShaderApplicator>().setImageEffect(_NightShader);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Flip the Boolean
            isFlashlightOn = !isFlashlightOn;
            
            // Set GameObject's Active based on this boolean
            _Flashlight.SetActive(isFlashlightOn);
        }
    }
}
