using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayer : MonoBehaviour
{
    [SerializeField]
    bool pcVersion = false;

    [SerializeField]
    float sensitivity = 1.5F;
    float rotationX = 0F;
    float rotationY = 0F;
    private float rotArrayX;
    float rotAverageX = 0F;
    private float rotArrayY;
    float rotAverageY = 0F;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pcVersion) cameraRotation();
    }

    private void cameraRotation()
    {
        //Resets the average rotation
        rotAverageY = 0f;
        rotAverageX = 0f;

        //Gets rotational input from the mouse
        rotationY += (Input.GetAxis("Mouse Y") * sensitivity) * 100 * Time.deltaTime;
        rotationX += (Input.GetAxis("Mouse X") * sensitivity) * 100 * Time.deltaTime;

        rotationY = Mathf.Clamp(rotationY, -90, 90);

        //Adds the rotation values to their relative array
        rotArrayY = rotationY;
        rotArrayX = rotationX;

        //Adding up all the rotational input values from each array
        rotAverageY += rotArrayY;
        rotAverageX += rotArrayX;

        //Get the rotation you will be at next as a Quaternion
        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
    }
}
