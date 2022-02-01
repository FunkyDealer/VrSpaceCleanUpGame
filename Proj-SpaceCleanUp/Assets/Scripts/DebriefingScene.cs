using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebriefingScene : MonoBehaviour
{

    [SerializeField] private Light[] lights;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RectTransform[] rectTransforms;
    [SerializeField] private SpeechHud speechHud;
    
    private string[] _sentences =
    {
        "Litter, ever since their dawn, humanity has littered the planet with waste.",
        "But it was not until the age of space exploration that their trash left the earth.",
        "For not everything that goes up, does come down.",
        "Dead spacecraft, rocket boosters, lost equipment and even weapons clutter the earth’s orbit.",
        "Many of these split into thousands of fragments, flying at high speeds and staying in orbit for hundreds of years.",
        "These “Debris” are a hazard to everything, with even the smallest ones hitting with enough force to destroy vital equipment in satellites and spaceships.",
        "And with each collision, even more are generated.",
        "If humanity does not clean up after itself, they will face their ultimate doom, and become trapped in their own planet.",
        "Ah, you’ve finally arrived.",
        "We’ve got a special assignment for you, it’s an old satellite that needs removal.",
        "Yes, indeed. The reason why this is “special”, you see, is because there seems to be another satellite on collision route.",
        "There are also several debris surrounding it, but retrieval of the satellite is your main priority, we can’t afford that collision.",
        "We’ve had a scarcity of human resources lately, but I trust you can easily do the job alone.",
        "Your handler has already been assigned, and he’s taken care of the paperwork.",
        "Oh, and do be careful out there, that orbital altitude is known for its many deadly flying debris.",
        "Just recover the satellite and be on your way out.",
        "Good luck, cleaner."
    };
    
    private void Start()
    {
        //Turn off lights (set intensity to 0)
        foreach (var l in lights)
        {
            l.intensity = 1f;
        }

        StartCoroutine(RunDebriefingCoroutine());
    }

    private void ChangeAndPlay(int i)
    {
        audioSource.clip = audioClips[i];
        audioSource.Play();
    }
    
    private IEnumerator GrowImageCoroutine(int i, bool deletePrevious)
    {
        if (deletePrevious)
        {
            for (var j = 0; j < i; j++)
            {
                rectTransforms[j].gameObject.SetActive(false);
            }
        }

        var temp = Vector3.one;
        rectTransforms[i].localScale = Vector3.zero;
        rectTransforms[i].gameObject.SetActive(true);
        while (rectTransforms[i].localScale.x < temp.x)
        {
            rectTransforms[i].localScale += new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.004f);
        }
    }

    private IEnumerator ChangeLightIntensityCoroutine(ushort endInt)
    {
        while (lights[2].intensity < endInt * 20)
        {
            for (var i = 0; i < 3; i++)
            {
                lights[i].intensity += lights[i].intensity * Time.deltaTime * 2;
            }

            yield return new WaitForSeconds(0.006f);
        }
        
        while (lights[2].intensity > endInt)
        {
            for (var i = 0; i < 3; i++)
            {
                lights[i].intensity -= lights[i].intensity * Time.deltaTime;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
    
    private IEnumerator RunDebriefingCoroutine()
    {
        lights[3].intensity = 100;
        yield return new WaitForSeconds(1);
        ChangeAndPlay(0);
        speechHud.WriteText(_sentences[0]);
        StartCoroutine(GrowImageCoroutine(0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(GrowImageCoroutine(1, false));
        
        yield return new WaitForSeconds(audioClips[0].length - 0.7f);
        ChangeAndPlay(1);
        speechHud.WriteText(_sentences[1]);
        StartCoroutine(GrowImageCoroutine(2, true));
        yield return new WaitForSeconds(2f);
        StartCoroutine(GrowImageCoroutine(3, false));
        yield return new WaitForSeconds(2f);
        StartCoroutine(GrowImageCoroutine(4, false));
        
        yield return new WaitForSeconds(audioClips[1].length - 3.7f);
        ChangeAndPlay(2);
        speechHud.WriteText(_sentences[2]);
        StartCoroutine(GrowImageCoroutine(5, false));
        
        yield return new WaitForSeconds(audioClips[2].length + 0.3f);
        ChangeAndPlay(3);
        speechHud.WriteText(_sentences[3]);
        StartCoroutine(GrowImageCoroutine(6, true));
        yield return new WaitForSeconds(1f);
        StartCoroutine(GrowImageCoroutine(7, false));
        
        yield return new WaitForSeconds(audioClips[3].length - 0.7f);
        ChangeAndPlay(4);
        speechHud.WriteText(_sentences[4]);
        
        yield return new WaitForSeconds(audioClips[4].length + 0.3f);
        ChangeAndPlay(5);
        speechHud.WriteText(_sentences[5]);
        StartCoroutine(GrowImageCoroutine(8, true));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(GrowImageCoroutine(9, false));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(GrowImageCoroutine(10, false));
        
        yield return new WaitForSeconds(audioClips[5].length - 1.4f);
        ChangeAndPlay(6);
        speechHud.WriteText(_sentences[6]);
        
        
        yield return new WaitForSeconds(audioClips[6].length + 0.3f);
        ChangeAndPlay(7);
        speechHud.WriteText(_sentences[7]);
        StartCoroutine(GrowImageCoroutine(11, true));

        yield return new WaitForSeconds(audioClips[7].length + 1);
        rectTransforms[11].gameObject.SetActive(false);
        //for (var i = 0; i < audioClips.Length; i++)
        //{
        //    yield return new WaitForSeconds(audioClips[i].length + 0.3f);
        //    ChangeAndPlay(i+1);            
        //}
        yield return new WaitForSeconds(1);
        rectTransforms[11].gameObject.SetActive(false);
        
        StartCoroutine(ChangeLightIntensityCoroutine(100));

        yield return new WaitForSeconds(5);
        speechHud.WriteText(_sentences[8]);

        yield return new WaitForSeconds(3);
        speechHud.WriteText(_sentences[9]);
        
        yield return new WaitForSeconds(6);
        speechHud.WriteText(_sentences[10]);
        
        yield return new WaitForSeconds(8);
        speechHud.WriteText(_sentences[11]);
        
        yield return new WaitForSeconds(8);
        speechHud.WriteText(_sentences[12]);
        
        yield return new WaitForSeconds(6);
        speechHud.WriteText(_sentences[13]);
        
        yield return new WaitForSeconds(6);
        speechHud.WriteText(_sentences[14]);
        
        yield return new WaitForSeconds(6);
        speechHud.WriteText(_sentences[15]);
        
        yield return new WaitForSeconds(5);
        speechHud.WriteText(_sentences[16]);
        StartCoroutine(ChangeLightIntensityCoroutine(0));
        
        yield return new WaitForSeconds(3);
        lights[3].intensity = 0;

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
