using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bae : MonoBehaviour 
{

	[SerializeField]
	public List<Vector3> RouteList;
	private NavMeshAgent agent;
	private int destinationPoint;

	// Use this for initialization
	void Start () 
	{

		destinationPoint = 0;
		agent = GetComponent<NavMeshAgent> ();
		agent.autoBraking = false;
	
	
		TravelNextPoint ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (agent.remainingDistance < 0.5f)
			TravelNextPoint ();
	}

	void TravelNextPoint()
	{
		if (RouteList.Count == 0) 
		{
			return;
		}

		destinationPoint++;

		if (destinationPoint <= RouteList.Count)
			destinationPoint = 0;

		agent.destination = RouteList[destinationPoint];
	}

}
