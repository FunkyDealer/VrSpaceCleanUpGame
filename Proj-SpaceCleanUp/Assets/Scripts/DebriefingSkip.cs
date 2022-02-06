using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebriefingSkip : MonoBehaviour
{
    [SerializeField]
    string nextScene;

    public void Skip()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
}
