using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    //Player stuff
    private bool isAlive = true;

    //health
    private int currenthealth = 100;
    [SerializeField]
    private int maxHealth = 100;

    //backPack Space
    [SerializeField]
    private int maxSpace = 10;
    private int currentSpace = 0;
    public int getMaxSpace() => maxSpace;
    public int getCurrentSpace() => currentSpace;

    //Oxygen
    [SerializeField]
    private int maxOxygen = 100;
    private int currentOxygen = 100;

    //Story flags
    bool blackBox = false;

    [SerializeField]
    private bool Movable = true;

    private bool inField = true;

    private IInteractible[] myInteractibes;

    //Objectives
    private Dictionary<string, Objective> currentObjectives;

    private int CurrentMoney = 0;

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
    private ObjectivesHud _ObjectivesHud;


    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _backpack.UpdateText($"{currentSpace} / {maxSpace}");
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
        } 
        else { ResetInput(); }

        if (!inField)
        {
            _textWarning.SetText("<- Outside Mission Area ->");
        }
        else if (currentOxygen <= 10)
        {
            _textWarning.SetText("<- OXYGEN LEVELS CRITICAL ->");
        }
        else
        {
            _textWarning.SetText("");
        }

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
        RaycastHit hit;
        if (Physics.Raycast(myCamera.position, myCamera.forward, out hit, useDistance, layerMask: interactionMask))
        {
            if (hit.collider)
            {
                //Debug.Log($"Interacting with {hit.collider.transform.name}");
                IInteractible[] I = hit.collider.gameObject.GetComponents<IInteractible>();
                if (I != null)
                {
                    myInteractibes = I;
                }
                else
                {
                    myInteractibes = null;
                }
            }
        }


        ResetInput();
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
        _healthSlider.UpdateSlider(currenthealth * 0.01f);
        if (currenthealth < 0) isAlive = false;
    }

    public void pickUpObject(int ammount)
    {
        currentSpace += ammount;
        _backpack.UpdateText($"{currentSpace} / {maxSpace}");

        if (!backPackHalf && currentSpace >= (maxSpace / 2) && currentSpace < maxSpace) //Sound Player
        {
            SoundPlayer.PlayPlayerSound("BACKPACK HALF");
            backPackHalf = true;
        }
        else if (currentSpace >= maxSpace)
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
        int money = currentSpace * 10;
        CurrentMoney += money;

        currentSpace = 0;
        _backpack.UpdateText($"{currentSpace} / {maxSpace}");
        backPackHalf = false;
        SoundPlayer.PlayPlayerSound("BACKPACK EMPTY");
    }

    public void replenishOxygen()
    {
        currentOxygen = maxOxygen;
        _oxygenSlider.UpdateSlider(currentOxygen * 0.01f);
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


    public void addObjective(Objective o)
    {
        if (currentObjectives == null)
        {
            currentObjectives = new Dictionary<string, Objective>();
        }

        if (!currentObjectives.ContainsKey(o.questLine)) //if questline doesn't exist
        {
            currentObjectives.Add(o.questLine, o); //add it
        }
        else
        {
            currentObjectives[o.questLine] = o; //else change the objective to the new one
        }

        _ObjectivesHud.LoadString(o.description);
        Debug.Log($"new Objective: {o.description}");
    }

    public void RemoveObjective(Objective o)
    {
        currentObjectives.Remove(o.questLine);

        Debug.Log($"Completed Objective: {o.description}");
    }
   
    public Objective getObjective(Objective o) //get questline objective
    {
        if (currentObjectives.ContainsKey(o.questLine)) return currentObjectives[o.questLine];
        else return null;
    }

    //Sounds
    public void BackPackFullWarning()
    {
        SoundPlayer.PlayPlayerSound("NOSPACE");
    }

    public void receiveMoney(int ammount)
    {
        CurrentMoney += ammount;
    }
}
