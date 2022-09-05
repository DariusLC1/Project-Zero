using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] int sensHori;
    [SerializeField] int sensVert;

    [SerializeField] int lockVertMin;
    [SerializeField] int lockVertMax;

    [SerializeField] bool invert;

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // getting the input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensHori;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensVert;

        if (invert)
        {
            xRotation += mouseY;
        }
        else
        {
            xRotation -= mouseY;
        }

        if(gameManager.instance.playerScript.isDashable == false)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 90, 0.5f);
        }
        else if (gameManager.instance.playerScript.isDashable == true)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 0.5f);
        }
        // clamp the rotation
        xRotation = Mathf.Clamp(xRotation, lockVertMin, lockVertMax);

        // rotate the camera on the x-axis
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // rotate the player
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
