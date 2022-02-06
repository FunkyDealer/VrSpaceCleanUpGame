using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    public enum EMissionStatus
    {
        none,
        partial,
        complete
    }
    static private EMissionStatus missionStatus = EMissionStatus.none;
    static public EMissionStatus MissionStatus => missionStatus;
    static public void setMissionStatus(EMissionStatus status) => missionStatus = status;

    static private bool debriStatus = false;
    static public bool DebriStatus => debriStatus;
    static public void setDebriStatus(bool status) => debriStatus = status;

    private static AppManager _instance;
    public static AppManager inst { get { return _instance; } }

    private float musicVolume = 1;
    public float MusicVolume => musicVolume;

    private float effectsVolume = 1;
    public float EffectsVolume => effectsVolume;
    
   

     void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;

        float newMusicVolume = Mathf.Log(value) * 20;

        mixer.SetFloat("MusicVolume", newMusicVolume);
    }

    public void SetEffectsVolume(float value)
    {      
        effectsVolume = value;

        float newEffectsVolume = Mathf.Log(value) * 20;

        mixer.SetFloat("EffectsVolume", newEffectsVolume);
    }


    static public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }


    static public void resetGame()
    {
        missionStatus = EMissionStatus.none;
        debriStatus = false;
    }
}
