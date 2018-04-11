using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private float canFire = 0.0f;

    public Camera cam;
    public GameObject bulletHole;
            
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update()
    {
        canFire += Time.deltaTime;

	    if(Input.GetMouseButton(0) && canFire >= 0.02f)
        {
            Ray camRay = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;
            
            if (Physics.Raycast(camRay, out hit, 50.0f))
            {
                if (hit.transform.tag == "Environment")
                {
                    GameObject tempObj = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.forward, -hit.normal)) as GameObject;
                    tempObj.transform.SetParent(hit.collider.gameObject.transform);
                }
            }

            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            canFire = 0.0f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Stop();
        }
    }
}
