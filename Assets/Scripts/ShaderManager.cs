using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{

    private static ShaderManager _instance;
    public static ShaderManager Instance { get { return _instance; } }

    public enum WhichShader { Nothing, DayShader, NightShader, FogShader};
    public WhichShader _ActiveShader = WhichShader.Nothing;
    public bool isFlashlightOn = false;

    [SerializeField]
    public AudioSource _DayMusic;

    [SerializeField] 
    public AudioSource _NightMusic;

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
        //DontDestroyOnLoad(gameObject);
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

    // Update is called once per frame
    void Update()
    {
        //if (MazeGameManager.Instance.getAudio() != null)
        //    Debug.Log("CURRENT VOLUME: " + MazeGameManager.Instance.getAudio().volume);
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _Camera.GetComponent<CameraShaderApplicator>().wipeImageEffect();
            //MazeGameManager.Instance.SetMusicVolume(1.0f);
            MazeGameManager.Instance.getAudio().Stop();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetShaderAndMusicEffect(_FogShader, 0.25f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetShaderAndMusicEffect(_DayShader, _DayMusic, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetShaderAndMusicEffect(_NightShader, _NightMusic, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Flip the Boolean
            isFlashlightOn = !isFlashlightOn;
            
            // Set GameObject's Active based on this boolean
            _Flashlight.SetActive(isFlashlightOn);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle Music
            if (MazeGameManager.Instance.getAudio() != null)
            {
                if (MazeGameManager.Instance.getAudio().isPlaying)
                {
                    MazeGameManager.Instance.getAudio().Stop();
                } else
                {
                    MazeGameManager.Instance.getAudio().Play();
                }
            }
        }
    }

    private void SetShaderAndMusicEffect(Material shader, float volume)
    {
        if (MazeGameManager.Instance.getAudio() != null && MazeGameManager.Instance.getAudio().isPlaying)
        {
            MazeGameManager.Instance.getAudio().Stop();
        }

        _Camera.GetComponent<CameraShaderApplicator>().setImageEffect(shader);
        MazeGameManager.Instance.SetMusicVolume(volume);
        MazeGameManager.Instance.getAudio().Play();
    }

    private void SetShaderAndMusicEffect(Material shader, AudioSource music, float volume)
    {
        MazeGameManager.Instance.ChangeMusic(music);
        SetShaderAndMusicEffect(shader, volume);
    }
}
