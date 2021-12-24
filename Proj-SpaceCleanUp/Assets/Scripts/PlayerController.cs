using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5;

    [SerializeField]
    private Transform camera;

    private bool moving = false;
    private bool input = false;
    private bool longPress = false;
    private bool use = false;

    private bool moveInput = false;
    private Vector3 direction;
    private Rigidbody myRigidbody;

    //[SerializeField]
    //private HudController hudController;
    [SerializeField]
    private float useSeconds = 1;

    [SerializeField]
    int useDistance = 20;

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
                        direction = camera.forward.normalized;
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
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, useDistance))
        {
            if (hit.collider)
            {
                Debug.Log($"Interacting with {hit.collider.transform.name}");
                IInteractible[] I = hit.collider.gameObject.GetComponents<IInteractible>();
                if (I != null)
                {
                    foreach (var i in I)
                    {
                        i.Interact(this);
                    }
                    //I.Interact(this.gameObject);
                }
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
   
}
