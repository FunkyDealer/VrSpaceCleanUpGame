using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    private ushort _id;
    private string _speaker;
    private string _sentence;

    public Dialog(ushort id, string speaker, string sentence)
    {
        _id = id;
        _speaker = speaker;
        _sentence = sentence;
    }

    public string GetSpeaker() { return _speaker; }
    public string GetSentence() { return _sentence; }
}

public class DialogDataBase
{
    private List<Dialog> _dialogs;
    
    public DialogDataBase()
    {
        _dialogs = new List<Dialog>();
        _dialogs.Add(new Dialog(0,"Handler", "Hey! Wake up!"));
        _dialogs.Add(new Dialog(1,"Handler", "Great, you’ve finally arrived at the scene, head over to the satellite, and place both thrusters. You have 30 minutes before the incoming satellite crashes into that one."));
        _dialogs.Add(new Dialog(2,"Handler", "This is your last shift of the week, I’m sure you’re eager to get back home, so make it fast."));
        _dialogs.Add(new Dialog(3,"Handler", "Your backpack is full, before you can pick up anymore, you need to drop what you’re carrying in your ship’s cargo."));
        _dialogs.Add(new Dialog(4,"Handler", "Place both thrusters, I’ve marked their positions with 3D displays."));
        _dialogs.Add(new Dialog(5,"Handler", "After you’ve placed them, you’ll need to clean up any debris that are on the way between the satellite and your ship."));
        _dialogs.Add(new Dialog(6,"Handler", "Now activate the thrusters and the satellite will move itself into position, after that you can come back to base."));
        _dialogs.Add(new Dialog(7,"Handler", "You found the black box? Place it in the cargo along with the other trash."));
        _dialogs.Add(new Dialog(8,"Handler", "Good job, the ship’s computer will link the ship to the satellite, see you back at base."));
        _dialogs.Add(new Dialog(9,"Computer", "Analyzes complete. Country of origin – China. Records indicate that it was hit by something 10 years ago."));
        _dialogs.Add(new Dialog(10,"Computer", "The records indicate 3 rods and various locations organized by a priority factor, these locations are various cities in the US and Europe. One of the rods seems to have been used."));
        _dialogs.Add(new Dialog(11,"Handler", "What are you doing? Your job is to clean up the satellite, and it ends there. The information on that black box is confidential, so get back to your mission immediately."));
        _dialogs.Add(new Dialog(12,"Player", "What’s the meaning of this information? These rods and locations, is this satellite some sort of weapon?"));
        _dialogs.Add(new Dialog(13,"Handler", "Don’t ask questions and just do as you’re told."));
        _dialogs.Add(new Dialog(14,"Player (Thinking)", "Something’s not right here. Maybe I can get someone to hack into the box’s files and further investigate it."));
        _dialogs.Add(new Dialog(15,"Friend", "Yo! I looked over the data and this is big stuff, the satellite is indeed a Chinese orbital weapon, but we still need more proof."));
        _dialogs.Add(new Dialog(16,"Friend", "The file says there’s a hatch, on the lower half, that can opened. Maybe you could go check it out, and then tell me what you find."));
        _dialogs.Add(new Dialog(17,"Friend", "Right there! Those are the rods, but one seems to be missing, maybe they used it in a test? can you bring the satell"));
        _dialogs.Add(new Dialog(18,"Handler", "That’s enough meddling! Your orders are to bring the satellite back to base! Do it now!"));
        _dialogs.Add(new Dialog(19,"Handler", "We’ll be putting the ship on auto pilot now, but good work on finishing the job."));
    }

    public string GetName(ushort i) { return _dialogs[i].GetSpeaker(); }
    public string GetSentence(ushort i) { return _dialogs[i].GetSentence(); }
}

public class DialogManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SpeechHud speechHud;

    private DialogDataBase _dialogDataBase;
    
    private void Awake()
    {
        _dialogDataBase = new DialogDataBase();
    }

    private void Start()
    {
        playerController.ChangeMovement(false);
        StartCoroutine(RunSpeechOnStartCoroutine());
    }

    public void RunSpeech(ushort id, ushort number)
    {
        StartCoroutine(RunSpeechCoroutine(id, number));
    }

    private IEnumerator RunSpeechCoroutine(ushort id, ushort number)
    {
        for (var i = id; i < id + number - 1; i++)
        {
            speechHud.WriteText(_dialogDataBase.GetSentence(i), _dialogDataBase.GetName(i));
            yield return new WaitUntil(() => speechHud.StoppedTyping() == 0);
            yield return new WaitForSeconds(1);
        }
    }
    
    private IEnumerator RunSpeechOnStartCoroutine()
    {
        speechHud.WriteText(_dialogDataBase.GetSentence(0), _dialogDataBase.GetName(0));
        yield return new WaitUntil(() => speechHud.StoppedTyping() == 0);
        yield return new WaitForSeconds(1);
        
        speechHud.WriteText(_dialogDataBase.GetSentence(1), _dialogDataBase.GetName(1));
        yield return new WaitUntil(() => speechHud.StoppedTyping() == 0);
        yield return new WaitForSeconds(1);
        
        playerController.ChangeMovement(true);
        speechHud.WriteText(_dialogDataBase.GetSentence(2), _dialogDataBase.GetName(2));
    }
}
