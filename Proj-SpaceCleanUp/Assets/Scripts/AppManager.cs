using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    private static AppManager _instance;
    public static AppManager inst { get { return _instance; } }

    private float musicVolume = 1;
    public float MusicVolume => musicVolume;
    public void SetMusicVolume(float value) => musicVolume = value;

    private float effectsVolume = 1;
    public float EffectsVolume => effectsVolume;
    public void SetEffectsVolume(float value) => effectsVolume = value;

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
}
