using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
    public GameObject generator;
    NavMeshAgent agent;

    public Transform[] waypoints;
    public Transform[] generatorWaypoints;
    private Vector3 target;
    private Vector3 distance;
    private int rand;
    private int prevRand;
    private bool moveOn = true;
    private int phase = 0;
    public float pause = 2;
    private float waitingTime = 0.0f;
    bool isFree;   

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}

    // Update is called once per frame
    void Update()
    {
        if (phase < 4)
        {
            Move();
        }
        else if (phase == 4)
        {            
            StartCoroutine("CheckingForSpace");
        }            
    }
        

    private void Move()
    {
        if (moveOn == true)
        {
            ObtainTarget();
        }

        else
        {
            target.y = transform.position.y;
            distance = target - transform.position;
            //Debug.Log(distance.magnitude);

            if (distance.magnitude <= 0)
            {
                waitingTime += Time.deltaTime;
                //Debug.Log(waitingTime);

                if (waitingTime >= pause)
                {
                    phase++;
                    //Debug.Log("phase: " + phase);
                    
                    moveOn = true;
                    waitingTime = 0;
                }
            }
            else
            {
                agent.SetDestination(target);
            }
        }
    }


    private void ObtainTarget()
    {
        if (phase == 0)
        {
            rand = Random.Range(0, 4);            
        }
        else if (phase == 1)
        {
            rand = Random.Range(4, 8);            
        }
        else if (phase == 2)
        {
            rand = Random.Range(8, 12);            
        }
        else if (phase == 3)
        {
            rand = Random.Range(12, 16);            
        }
       
        if (waypoints[rand].GetComponent<WaypointBehaviour>().isAvailable == false)
        {
            ObtainTarget();
        }
        else
        {
            if (phase == 0)
            {
                prevRand = rand;                
            }
            else
            {
                waypoints[prevRand].GetComponent<WaypointBehaviour>().isAvailable = true;
                prevRand = rand;
            }

            waypoints[rand].GetComponent<WaypointBehaviour>().isAvailable = false;
            target = waypoints[rand].position;
        }

        //Debug.Log("rand: " + rand);
        moveOn = false;
    }


    private void ApproachGenerator()
    {
        rand = Random.Range(0, 7);        

        Debug.Log(rand);

        if (generatorWaypoints[rand].GetComponent<WaypointBehaviour>().isAvailable == false)
        {
            ApproachGenerator();
        }
        else
        {            
            waypoints[prevRand].GetComponent<WaypointBehaviour>().isAvailable = true;
            generatorWaypoints[rand].GetComponent<WaypointBehaviour>().isAvailable = false;
            agent.SetDestination(generatorWaypoints[rand].position);
            Debug.Log("Setting: " + rand + "To false");
        }      
    }


    private IEnumerator CheckingForSpace()
    {
        isFree = true;        
        phase++;

        for (int points = 0; points <= 7; points++)
        {
            if (generatorWaypoints[points].GetComponent<WaypointBehaviour>().isAvailable == false)
            {
                isFree = false;
                Debug.Log(points + " no space");
            }
            else
            {
                Debug.Log(points + " space");
                isFree = true;
                break;
            }            
        }        

        if (isFree == true)
        {            
            ApproachGenerator();
        }
        else
        {       
            Debug.Log("no space");
            yield return new WaitForSeconds(1);
            StartCoroutine("CheckingForSpace");
        }
    }    
} 