using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour, IInteractible
{
    bool Active = true;

    [SerializeField]
    int code;

    [SerializeField]
    GameObject boosterObj;

    [SerializeField]
    BoosterObjectiveManager boosterManager;

    [SerializeField]
    bool Promt = true;
    

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(PlayerController player)
    {
        if (boosterManager.getObjectiveConfirmation() && Active) Action();
    }

    private void Action()
    {
        if (Promt)
        {
            gameObject.SetActive(false);

            boosterObj.SetActive(true);
        }

        SendReadyNotice();
        //active next

        Active = false;
    }

    private void SendReadyNotice()
    {
  
        if (Promt) boosterManager.receiveNoticePrompt(code);
        else boosterManager.ReceiveNoticeActivate();
    }

    
}
