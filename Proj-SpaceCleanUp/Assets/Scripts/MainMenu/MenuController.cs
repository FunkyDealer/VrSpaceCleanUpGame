using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField]
    private TMP_Text MusicvolumeTextValue = null;
    [SerializeField]
    private Slider MusicVolumeSlider = null;
    [SerializeField]

    private TMP_Text EffectsvolumeTextValue = null;
    [SerializeField]
    private Slider EffectsVolumeSlider = null;

    [SerializeField]
    private float defaultVolume = 1.0f;

    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Levels to Load")]
    public string _newGameLevel;
    private string levelToLoad;

    private float newMusicVolume;
    private float newEffectsVolume;


    public void StartGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetEffectsVolume(float value)
    {

    }

    public void SetVolumeButtons()
    {
        MusicVolumeSlider.value = AppManager.inst.MusicVolume;
        MusicvolumeTextValue.text = AppManager.inst.MusicVolume.ToString("0.0");

        EffectsVolumeSlider.value = AppManager.inst.EffectsVolume;
        EffectsvolumeTextValue.text = AppManager.inst.EffectsVolume.ToString("0.0");

        newMusicVolume = AppManager.inst.MusicVolume;
        newEffectsVolume = AppManager.inst.EffectsVolume;
    }

    public void SetNewEffectsVolume(float volume)
    {
        newEffectsVolume = volume;
        EffectsvolumeTextValue.text = volume.ToString("0.0");
    }

    public void SetNewMusicVolume(float volume)
    {
        newMusicVolume = volume;
        MusicvolumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        AppManager.inst.SetEffectsVolume(newEffectsVolume);
        AppManager.inst.SetMusicVolume(newMusicVolume);

        StartCoroutine(ConfirmationBox());
    }

    public void ResetVolume()
    {
        AppManager.inst.SetMusicVolume(defaultVolume);
        MusicVolumeSlider.value = defaultVolume;
        MusicvolumeTextValue.text = defaultVolume.ToString("0.0");

        AppManager.inst.SetEffectsVolume(defaultVolume);
        EffectsVolumeSlider.value = defaultVolume;
        EffectsvolumeTextValue.text = defaultVolume.ToString("0.0");
    }



    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
