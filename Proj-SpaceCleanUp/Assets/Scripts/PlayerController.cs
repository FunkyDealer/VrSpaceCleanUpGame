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


    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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

            if (!longPress)
            {
                moving = !moving;
                direction = camera.forward.normalized;
            }
            else
            {
                use = true;
                longPress = false;
            }

            //hudController.resetRetticle();
        }

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

}
