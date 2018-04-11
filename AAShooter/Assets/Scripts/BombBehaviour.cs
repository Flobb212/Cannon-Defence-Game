using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour
{
    private bool detonate = false;
    public GameObject explosion;
    private Transform m_transform;
    private AudioSource m_audio;
    public GameObject generator;
    private int damage = 20;
    private Collider[] col;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_transform = transform;
        generator = GameObject.Find("Generator");
    }

        void OnCollisionEnter()
    {
        if(detonate == false)
        {
            detonate = true;

            Instantiate(explosion, m_transform.position, m_transform.rotation);
            transform.GetComponent<MeshRenderer>().enabled = false;
            m_audio.Play();
            StartCoroutine("DestroyBomb");            

            col = Physics.OverlapSphere(transform.position, 1);
            foreach (Collider hit in col)
            {
                if (hit.CompareTag("Environment"))
                {
                    generator.SendMessage("TakeDamage", damage);
                }
            }
        }
    }

    private IEnumerator DestroyBomb()
    {
        while(m_audio.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
