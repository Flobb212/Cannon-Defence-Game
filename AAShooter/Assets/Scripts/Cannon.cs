using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    public GameObject rocketPrefab;
    public Transform rocketSpawn;
    private float canFire = 0.0f;

    public Camera cam;
    public GameObject rocketHole;    

    // Update is called once per frame
    void Update()
    {
        canFire += Time.deltaTime;

        if(Input.GetAxis("Fire2") == -1 && canFire >= 1.0f)
        {
            Instantiate(rocketPrefab, rocketSpawn.position, rocketSpawn.rotation);
            canFire = 0.0f;
        }
    }
}
