using UnityEngine;
using System.Collections;

public class P1Look : MonoBehaviour 
{
    public enum RotationAxes { MouseX = 0, MouseY = 1 }
    public RotationAxes axes = RotationAxes.MouseX;
    public float sensitivityX = 5.0f;
    public float sensitivityY = 5.0f;
    public float minimumX = -90.0f;
    public float maximumX = 90.0f;
    public float minimumY = -60.0f;
    public float maximumY = 60.0f;
    float rotationX = 0.0f;
    float rotationY = 0.0f;

    void Update()
    {        
            if (axes == RotationAxes.MouseX)
            {
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationX, 0);
            }

            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }              
    }
}
