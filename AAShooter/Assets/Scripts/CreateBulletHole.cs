using UnityEngine;
using System.Collections;

public class CreateBulletHole : MonoBehaviour
{
    public Camera mainCam;
    public GameObject bulletHole;

    // Update is called once per frame
    void Update()
    {
        Ray camRay = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100.0f))
        {
            if (Input.GetMouseButtonDown(0))
            {                
                GameObject tempObj = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.forward, -hit.normal)) as GameObject;
                tempObj.transform.SetParent(hit.collider.gameObject.transform);                
            }
        }
    }
}
