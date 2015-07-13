using UnityEngine;
using System.Collections;
using System.Linq;

public class Waypointfollower : MonoBehaviour {

	//public Transform[] wayPoint = new Transform[9]; 
	public GameObject[] wayPoint;
	public GameObject player;
	int currentWayPoint = 0;
	private NavMeshAgent agent;
	public Transform destination;
	//float rotationSpeed = 6.0f;
	public float accelerate = 1.8f;

	

	

	
	// Use this for initialization
	//to do, write a function that goes out and finds the waypoints
	void Start () 
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
		wayPoint = GameObject.FindGameObjectsWithTag("Waypoint").OrderBy( go => go.name ).ToArray(); 
		player = GameObject.Find("Player");

	}
	

	void Update () 
	{
		if(currentWayPoint >= (wayPoint.Length))
		{

			player.GetComponent<follow>().enabled = false;


		}
		else
		{
			if (Input.GetAxis("Mouse ScrollWheel")  < 0) // forward
			{
				agent.speed -= 0.8f;
			}
			if (Input.GetAxis("Mouse ScrollWheel")  > 0) // forward
			{
				agent.speed += 0.8f;
			}
			if(agent.speed <=0)
			{	
				agent.speed =0;
			}else{


				walk();
			}
		}
	}
	
	void walk()
	{

		if (!agent.pathPending && agent.remainingDistance < 0.5f && (currentWayPoint < wayPoint.Length))//Vector3.Distance(wayPoint[currentWayPoint].transform.position, transform.position) < 1)//accelerate)
		{

			agent.SetDestination(wayPoint[currentWayPoint].transform.position);
			currentWayPoint++;

		}


	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "WayPoint")
			currentWayPoint++;  
	}
}
