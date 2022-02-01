using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayInitialCutScene : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    [SerializeField]
    float startDelay = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(startDelay);


    }
}
