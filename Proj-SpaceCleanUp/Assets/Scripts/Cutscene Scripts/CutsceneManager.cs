using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    SpeechHud speechHud;

    [SerializeField]
    bool firstCutscene = false;



    [Header("Optional Values")]
    [SerializeField]
    Animator nextCutscene; //animator of the cutscene to play next

    [SerializeField]
    string nextScene;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Animator thisAnimator = GetComponent<Animator>();

        AnimatorSetter animatorSetter = GetComponent<AnimatorSetter>();

        animatorSetter.SetAnimatorVariables(thisAnimator);

        if (firstCutscene) thisAnimator.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLine(string line)
    {
        speechHud.WriteText(line);
    }

    public void StartNextCutscene(float delay)
    {

        StartCoroutine(StartCutScene(delay));
    }

    [HideInInspector]
    private IEnumerator StartCutScene(float delay, int dummy = 1)
    {
        yield return new WaitForSeconds(delay);
        nextCutscene.SetTrigger("Start");
    }

    public void ChangeScene(float delay)
    {
        StartCoroutine(Nextscene(delay));
    }

    [HideInInspector]
    private IEnumerator Nextscene(float delay, int dummy = 1)
    {
        yield return new WaitForSeconds(delay);
        AppManager.ChangeScene(nextScene);
    }
}
