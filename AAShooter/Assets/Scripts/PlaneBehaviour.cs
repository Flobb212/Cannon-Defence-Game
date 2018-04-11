using UnityEngine;
using System.Collections;

public class PlaneBehaviour : MonoBehaviour
{
    public int initialHealth = 100;
    private int currentHealth;
    private Transform m_transform;

    public Transform startObj;
    public Transform endObj;
    private float interValue = 0.0f;
    public float timeToInterpolate = 10.0f;

    private bool attack = false;
    public GameObject bomb;

    // Use this for initialization
    void Start ()
    {
        currentHealth = initialHealth;
        m_transform = GetComponent<Transform>();

        Invoke("InitialCoroutine", 0.0f);
    }

    private void InitialCoroutine()
    {
        StartCoroutine("StartInterp");
    }

    // Update is called once per frame
    void Update ()
    {
	    if(transform.position.z <= -40)
        {
            if(attack == false)
            {
                attack = true;
                DropBomb();
            }
        }
	}

    private void DropBomb()
    {
        Instantiate(bomb, (this.transform.position - new Vector3(0, 2, 0)), this.transform.rotation);
    }

    private IEnumerator StartInterp()
    {
        float timer = 0;

        interValue = 0;

        while (interValue <= 1)
        {
            timer += Time.deltaTime;

            interValue = timer / timeToInterpolate;

            m_transform.position = Vector3.Lerp(startObj.position, endObj.position, interValue);

            yield return null;
        }
        Destroy(transform.parent.gameObject);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {        
        Destroy(transform.parent.gameObject);
    }
}
