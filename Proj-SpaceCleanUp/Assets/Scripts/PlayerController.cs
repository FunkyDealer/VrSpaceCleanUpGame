using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5;

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

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                input = true;

                RaycastHit hit;

                if (Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, 20.0f))
                {
                    if (hit.collider.CompareTag("UI"))
                    {
                        Debug.LogWarning("we are not movable!");
                        //moving = false;
                        //Movable = false;
                    }
                }
                
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
                        direction = myCamera.forward.normalized;
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


    }

    void FixedUpdate()
    {
        if (moving)
        {
            myRigidbody.velocity = direction * movementSpeed;



        }
        else if (use)
        {
            //do something;

            Debug.Log("Using");
            Interact();            

        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
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


    public void loseHealth(int damage)
    {
        currenthealth -= damage;
        if (currenthealth < 0) isAlive = false;
    }

    public void pickUpObject(int ammount)
    {
        currentSpace += ammount;
    }

    public void PlaceTrashInStorage()
    {
        currentSpace = 0;
    }

    public void replenishOxygen()
    {
        currentOxygen = maxOxygen;
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

}
