using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private bool topDown = false;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {//get mouse input

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            topDown = true;
            GameObject.Find("CameraPos").transform.localPosition = new Vector3(0, 12, 0);
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            topDown = false;
            GameObject.Find("CameraPos").transform.localPosition = new Vector3(0, 0, 0);
        }

        if(!topDown)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //rotate cam
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            yRotation += mouseX;
            transform.rotation = Quaternion.Euler(60, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }


    }
}
