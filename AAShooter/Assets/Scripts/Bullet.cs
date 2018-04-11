using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private Transform m_transform;
    public float force = 100.0f;

    private Vector3 wantedRot;
    public GameObject bloodPrefab;    
    private GameObject bloodSplat;
    public GameObject sparkPrefab;
    private GameObject spark;
    private float timer = 0.0f;    

    public int damage = 2;
    
    // Use this for initialization
    void Start()
    {
        m_transform = transform;
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(m_transform.forward * force, ForceMode.Impulse);
    }    

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 5)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Enemy" || col.transform.tag == "Plane")
        {
            col.gameObject.SendMessage("TakeDamage", damage);

            if (col.transform.tag == "Enemy")
            {
                for (int loop = 0; loop < col.contacts.Length; loop++)
                {
                    wantedRot = col.contacts[loop].normal;
                }

                bloodSplat = Instantiate(bloodPrefab, transform.position, Quaternion.Euler(wantedRot.x, wantedRot.y, wantedRot.z)) as GameObject;
            }
            else
            {
                for (int loop = 0; loop < col.contacts.Length; loop++)
                {
                    wantedRot = col.contacts[loop].normal;
                }

                spark = Instantiate(sparkPrefab, transform.position, Quaternion.Euler(wantedRot.x, wantedRot.y, wantedRot.z)) as GameObject;
            }
        }
        Destroy(gameObject);
    }
}