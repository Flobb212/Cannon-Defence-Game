using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    public int initialHealth = 50;
    public int currentHealth;

    private int spawnPoint;
    private Transform m_transform;
    private Transform leftPoint;
    private Transform rightPoint;
    private Transform spawnLoc;
    int rand;

    private float interValue = 0.0f;
    public float timeToInterpolate = 4.0f;
    private float timerOfInterp = 0.0f;
        
    public GameObject generator;
    private RaycastHit seeGenerator;
    private Vector3 genDirection;
    public GameObject bulletPrefab;    
    private float attackTimer = 0.0f;

    // Use this for initialization
    void Start()
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
    void Update()
    {
        transform.LookAt(generator.transform);
        genDirection = generator.transform.position - transform.position;

        if(Physics.Raycast(transform.position, genDirection, out seeGenerator))
        {
            Debug.DrawRay(transform.position, generator.transform.position, Color.blue);

            attackTimer += Time.deltaTime;
            Debug.Log("Raycast works");

            if (attackTimer >= 1 && seeGenerator.transform.tag == "Generator")
            {
                Debug.Log("FIRE");
                attackTimer = 0.0f;
                Attack();
            }
        }
    }

    private void Attack()
    {
        Instantiate(bulletPrefab, this.GetComponentInChildren<Transform>().position, this.GetComponentInChildren<Transform>().rotation);
    }

    private IEnumerator StartInterp()
    {
        rand = Random.Range(1, 5);
        interValue = 0f;
        yield return new WaitForSeconds(rand);
        timerOfInterp = 0.0f;

        rand = Random.Range(1, 3);

        if(rand == 1)
        {
            while (interValue <= 1)
            {
                timerOfInterp += Time.deltaTime;
                interValue = timerOfInterp / timeToInterpolate;
                m_transform.position = Vector3.Lerp(spawnLoc.position, leftPoint.position, interValue);
                yield return null;
            }
        }
        else
        {
            while (interValue <= 1)
            {
                timerOfInterp += Time.deltaTime;
                interValue = timerOfInterp / timeToInterpolate;
                m_transform.position = Vector3.Lerp(spawnLoc.position, rightPoint.position, interValue);
                yield return null;
            }
        }

        StartCoroutine("ReturnInterp");
    }

    private IEnumerator ReturnInterp()
    {
        rand = Random.Range(1, 5);
        interValue = 0f;
        yield return new WaitForSeconds(rand);
        timerOfInterp = 0.0f;

        while (interValue <= 1)
        {
            timerOfInterp += Time.deltaTime;

            interValue = timerOfInterp / timeToInterpolate;

            m_transform.position = Vector3.Lerp(transform.position, spawnLoc.position, interValue);

            yield return null;
        }

        StartCoroutine("StartInterp");
    }

    public void SpawnPoint(int location)
    {
        spawnPoint = location;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void setPoints(Transform left, Transform right, Transform spawn)
    {
        leftPoint = left;
        rightPoint = right;
        spawnLoc = spawn;
    }

    void Die()
    {
        FindObjectOfType<SpawningSystem>().FreeSpawn(spawnPoint);
        Destroy(gameObject);
    }
}
