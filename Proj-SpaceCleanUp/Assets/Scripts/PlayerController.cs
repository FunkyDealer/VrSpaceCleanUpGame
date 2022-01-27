﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    GameManager gameManager;

    [Header("Movement")]
    [SerializeField]
    private float maxMovementSpeed = 5;
    private float currentMovementSpeed = 0;
    [SerializeField, Min(0.01f)]
    private float movSmoothLerp = 0.03f;

    [SerializeField]
    private Transform myCamera;

    private bool moving = false;
    private bool input = false;
    private bool longPress = false;
    private bool use = false;

    private bool moveInput = false;
    private Vector3 direction;
    private Rigidbody myRigidbody;
    private Camera myCameraGO;

    //[SerializeField]
    //private HudController hudController;
    [SerializeField]
    private float useSeconds = 1;

    [SerializeField]
    int useDistance = 20;
    [SerializeField]
    LayerMask interactionMask;

    [Header("Player Stuff")]
    //Player stuff
    private bool isAlive = true;
    //health
    private float currenthealth = 100;
    public float CurrentHealth => currenthealth;
    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth => maxHealth;
    //Oxygen
    [SerializeField]
    private int maxOxygen = 100;
    [SerializeField]
    private float oxygenUseRate = 0.5f;
    private float currentOxygen = 100;
    private bool oxygenHalf = false;
    private bool oxygenLow = false; 
    private int oxygenUpgradeLevel = 1;
    public int getOxygenUpgradeLevel => oxygenUpgradeLevel;

    private bool outOfOxygen = false;
    [SerializeField]
    private float HealthLoseRate = 1f;

    //backPack Space
    [SerializeField]
    private int maxBackPackSpace = 10;
    private int currentBackPackSpace = 0;
    public int getMaxBackPackSpace() => maxBackPackSpace;
    public int getCurrentSpace() => currentBackPackSpace;
    private int BackPackSpaceUpgradeLevel = 1;
    public int getBackPackSpaceUpgradeLevel => BackPackSpaceUpgradeLevel;

    //Story flags
    bool blackBox = false;

    [SerializeField]
    private bool Movable = true;

    private bool inField = true;

    private IInteractible[] myInteractibes;

    private int currentMoney = 0;
    public int CurrentMoney => currentMoney;

    private bool death = false;

    //Sound
    [Header("Sound")]
    [SerializeField]
    PlayerSoundPlayer SoundPlayer;
    bool backPackHalf = false; //set bool true when backpack reaches half, set back to false when backpack emptied

    //HUD Connections
    [Header("HUD Connections")]
    [SerializeField]
    private HealthSlider _healthSlider; //Variable updated in LoseHealth
    [SerializeField]
    private OxygenSlider _oxygenSlider; //Variable updated in ReplenishOxygen
    [SerializeField]
    private Backpack _backpack;         //Variable updated in PickupObject and PlaceTrashInStorage
    [SerializeField]
    private TextWarning _textWarning;   //Variable updated in Update
    [SerializeField]
    private HoverInfoHud _infoHud;


    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _backpack.UpdateBackPack(currentBackPackSpace, maxBackPackSpace);
        _backpack.updateMoney(currentMoney);
        _oxygenSlider.UpdateSlider(currentOxygen, maxOxygen);
        _healthSlider.UpdateSlider(currenthealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                input = true;
                
                StartCoroutine(CheckForUse());

                //hudController.StartReticle(useSeconds);
            }

            if (Input.GetButtonUp("Fire1"))
            {
                input = false;

                if (!longPress )
                {
                    if (Movable)
                    {
                        moving = !moving;
                        SoundPlayer.PlayPlayerSound("GAS");
                        if (moving)
                        {                            
                            direction = myCamera.forward.normalized;
                        }
                    }
                }
                else
                {
                    use = true;
                    longPress = false;
                }

                //hudController.resetRetticle();
            }
            
            if (!outOfOxygen) currentOxygen = currentOxygen - oxygenUseRate * Time.deltaTime;


            if (outOfOxygen)
            {
                currenthealth = currenthealth - HealthLoseRate * Time.deltaTime;
            }

            if (isAlive && currenthealth <= 0)
            {
                isAlive = false;
                StartCoroutine(Die());
            }

            if (Input.GetButtonDown("Jump"))
            {
                loseHealth(17);
                receiveMoney(4);
            }


        } 
        else { ResetInput(); }  

    }

    void FixedUpdate()
    {
        if (moving)
        {
            currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, maxMovementSpeed, movSmoothLerp);

            myRigidbody.velocity = direction * currentMovementSpeed;
        }
        else if (use)
        {
            //do something;

            Debug.Log("Using");
            Interact();            

        }
        else
        {
            currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, 0, movSmoothLerp);
            myRigidbody.velocity = direction * currentMovementSpeed;
        }

        //Interactions
        calculateInteraction();

        if (!oxygenHalf && currentOxygen <= maxOxygen / 2)
        {
            oxygenHalf = true;
            SoundPlayer.PlayPlayerSound("OXYGEN HALF");
        }
        if (oxygenHalf && !oxygenLow && currentOxygen <= 20)
        {
            oxygenLow = true;
            SoundPlayer.PlayPlayerSound("OXYGEN LOW");
        }
        if (currentOxygen <= 0)
        {
            outOfOxygen = true;
        }

        if (!inField)
        {
            _textWarning.SetText("<- Outside Mission Area ->");
        }
        else if (currentOxygen <= 20)
        {
            _textWarning.SetText("<- OXYGEN LEVELS CRITICAL ->");
        }
        else
        {
            _textWarning.SetText("");
        }

        _oxygenSlider.UpdateSlider((int)currentOxygen, maxOxygen);
        _healthSlider.UpdateSlider((int)currenthealth, maxHealth);

        ResetInput();
    }

    private void calculateInteraction()
    {
        //Interactions
        RaycastHit hit;
        if (Physics.Raycast(myCamera.position, myCamera.forward, out hit, useDistance, layerMask: interactionMask))
        {
            if (hit.collider)
            {
                //Debug.Log($"Interacting with {hit.collider.transform.name}");
                IInteractible[] I = hit.collider.gameObject.GetComponents<IInteractible>();
                if (I != null)
                {
                    if (I.Length > 0)
                    {
                        myInteractibes = I;
                        (string, string) info = I[0].getInfo(this);

                        _infoHud.LoadText(info.Item1, info.Item2);
                    }
                    else
                    {
                        myInteractibes = null;
                        _infoHud.UnloadText();
                    }
                }
                else
                {
                    myInteractibes = null;
                    _infoHud.UnloadText();
                }
            }
        }
        else
        {
            myInteractibes = null;
            _infoHud.UnloadText();
        }
    }

    //reset jump input
    private void ResetInput()
    {
        use = false;
    }

    private IEnumerator CheckForUse()
    {
        yield return new WaitForSeconds(useSeconds);

        if (input)
        {
            longPress = true;
            moving = false;
            Debug.Log("Long Press");
        }
    }

    void Interact()
    {
        if (myInteractibes != null)
        {
            //myInteractibes.Interact(this);

            foreach (var i in myInteractibes)
            {
                i.Interact(this);
            }

        }
    }

    public void ChangeMovement(bool mv)
    {
        Movable = mv;
    }

    public void loseHealth(int damage)
    {
        currenthealth -= damage;
        _healthSlider.UpdateSlider(currenthealth, maxHealth);
        if (currenthealth < 0) isAlive = false;
    }

    public void RecoverHealth()
    {
        currenthealth = maxHealth;
        _healthSlider.UpdateSlider(currenthealth, maxHealth);
    }

    public void pickUpObject(int ammount)
    {
        currentBackPackSpace += ammount;
        _backpack.UpdateBackPack(currentBackPackSpace, maxBackPackSpace);

        if (!backPackHalf && currentBackPackSpace >= (maxBackPackSpace / 2) && currentBackPackSpace < maxBackPackSpace) //Sound Player
        {
            SoundPlayer.PlayPlayerSound("BACKPACK HALF");
            backPackHalf = true;
        }
        else if (currentBackPackSpace >= maxBackPackSpace)
        {
            SoundPlayer.PlayPlayerSound("BACKPACK FULL");
        }
        else
        {
            SoundPlayer.PlayPlayerSound("DEBRI");
        }
    }

    public void PlaceTrashInStorage()
    {
        int money = currentBackPackSpace * 10;
        receiveMoney(money);

        currentBackPackSpace = 0;
        _backpack.UpdateBackPack(currentBackPackSpace, maxBackPackSpace);
        backPackHalf = false;
        SoundPlayer.PlayPlayerSound("BACKPACK EMPTY");
    }

    public void replenishOxygen()
    {
        currentOxygen = maxOxygen;
        SoundPlayer.PlayPlayerSound("OXYGENREPLENISH");
        oxygenHalf = false;
        oxygenLow = false;
        outOfOxygen = false;
        _oxygenSlider.UpdateSlider(currentOxygen, maxOxygen);
    }

    public void getBlackBox()
    {
        blackBox = true;
    }

    public void setinField(bool b)
    {
        inField = b;

        if (!inField)
        {
            //do something in hud;
            Debug.Log("You have left the mission Field");
        }
        else
        {
            Debug.Log("You have arrived at the mission Field");
        }
    }

    //Sounds
    public void BackPackFullWarning()
    {
        SoundPlayer.PlayPlayerSound("NOSPACE");
    }

    public void receiveMoney(int ammount)
    {
        currentMoney += ammount;
        _backpack.updateMoney(currentMoney);
    }

    public void PayMoney(int ammount)
    {
        currentMoney -= ammount;
        _backpack.updateMoney(currentMoney);
    }

    public void UpgradeOxygen(int newMax, int newLevel)
    {
        maxOxygen = newMax;
        oxygenUpgradeLevel = newLevel;

        _oxygenSlider.UpdateSlider(currentOxygen, maxOxygen);
    }

    public void UpgradeBackPack(int newMax, int newLevel)
    {
        maxBackPackSpace = newMax;
        BackPackSpaceUpgradeLevel = newLevel;

        _backpack.UpdateBackPack(currentBackPackSpace, maxBackPackSpace);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);

        gameManager.changeToMainMenu();
        
    }

}
