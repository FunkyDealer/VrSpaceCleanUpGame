using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource GasReleaseSource;
    [SerializeField]
    AudioSource SuitVoiceSource;

    //Audio Clips
    [Header("Suit voice Clips")]
    [SerializeField]
    AudioClip BackPackFullClip;
    [SerializeField]
    AudioClip BackPackHalfFullClip;
    [SerializeField]
    AudioClip BackPackEmptyClip;
    [SerializeField]
    AudioClip DebriPickClip;
    [SerializeField]
    AudioClip OxygenHalfClip;
    [SerializeField]
    AudioClip OxygenLowClip;
    [SerializeField]
    AudioClip OxygenReplenish;
    [SerializeField]
    AudioClip SuitRepairedClip;
    [SerializeField]
    AudioClip NotEnoughSpaceClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlayerSound(string sound)
    {
        switch (sound)
        {
            case "GAS":
                GasReleaseSource.Play();
                break;
            case "BACKPACK HALF":
                SuitVoiceSource.PlayOneShot(BackPackHalfFullClip);
                break;
            case "BACKPACK FULL":
                SuitVoiceSource.PlayOneShot(BackPackFullClip);
                break;
            case "BACKPACK EMPTY":
                SuitVoiceSource.PlayOneShot(BackPackEmptyClip);
                break;
            case "DEBRI":
                SuitVoiceSource.PlayOneShot(DebriPickClip);
                break;
            case "OXYGEN HALF":
                SuitVoiceSource.PlayOneShot(OxygenHalfClip);
                break;
            case "OXYGEN LOW":
                SuitVoiceSource.PlayOneShot(OxygenLowClip);
                break;
            case "OXYGENREPLENISH":
                SuitVoiceSource.PlayOneShot(OxygenReplenish);
                break;
            case "SUIT":
                SuitVoiceSource.PlayOneShot(SuitRepairedClip);
                break;
            case "NOSPACE":
                SuitVoiceSource.PlayOneShot(NotEnoughSpaceClip);
                break;
            default:
                throw new System.Exception($"{sound} is not a reconignized word");
        }
    }

}
