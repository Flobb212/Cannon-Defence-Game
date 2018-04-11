using UnityEngine;
using System.Collections;

public class Rockets : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private Transform m_transform;
    private Vector3 wantedRot;
    public float force = 100.0f;
    private AudioSource m_audioSource;
    public int damage = 50;

    public AudioClip clip;
    public GameObject explosionPrefab;    

    private bool explodeOnce = true;
    
    // Use this for initialization
    void Start()
    {
        m_transform = transform;
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(m_transform.forward * force, ForceMode.Impulse);
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.PlayOneShot(clip);
    }       

    void OnCollisionEnter(Collision col)
    {
        for (int loop = 0; loop < col.contacts.Length; loop++ )
        {
            wantedRot = col.contacts[loop].normal;
        }

        if (explodeOnce == true)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.Euler(wantedRot.x, wantedRot.y, wantedRot.z));
            Damage();

            m_transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            m_transform.GetComponentInChildren<ParticleSystem>().Stop();

            explodeOnce = false;
        }

        StartCoroutine("KillRocket");
    }

    public void Damage()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 3);

        foreach(Collider hit in col)
        {
            GameObject enemy = hit.gameObject;

            if(hit.CompareTag("Enemy") || hit.CompareTag("Plane"))
            {
                RaycastHit enemyHit;

                Vector3 enemDirection = enemy.transform.position - transform.position;

                if (Physics.Raycast(transform.position, enemDirection, out enemyHit, 3))
                {
                    if(enemyHit.collider == enemy.GetComponent<Collider>())
                    {
                        hit.gameObject.SendMessage("TakeDamage", damage);
                    }
                }
            }
        }
    }

    private IEnumerator KillRocket()
    {
        while(m_audioSource.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}