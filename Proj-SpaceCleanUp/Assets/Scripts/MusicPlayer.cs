using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> songList;

    [SerializeField]
    AudioSource source;


    [SerializeField]
    float startDelay = 3;

    bool started = false;
    int currentlyPlayingSong = 0;

    void Awake()
    {
        StartCoroutine(startPlaying());


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (started && !source.isPlaying)
        {            

            currentlyPlayingSong = currentlyPlayingSong + 1;
            if (currentlyPlayingSong == songList.Count) currentlyPlayingSong = 0;

            source.PlayOneShot(songList[currentlyPlayingSong]);
        }



    }

    IEnumerator startPlaying()
    {
        yield return new WaitForSeconds(startDelay);

        currentlyPlayingSong = Random.Range(0, songList.Count);
        source.PlayOneShot(songList[currentlyPlayingSong]);
        started = true;
    }


}
