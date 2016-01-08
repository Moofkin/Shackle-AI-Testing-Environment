//Created by: Lauren Cunningham

using UnityEngine;
using System.Collections;

public class spt_monsterMovement : MonoBehaviour {

    public Transform[] waypoints;
    private int[][] waypointGraph;
    private spt_createGraphForTestingEnvironment graphScript;

    // The waypoint that the monster is currently travelling toward
    private int currentWaypoint;

    // Used to guide the monster's movement
    private NavMeshAgent agent;

    // Use this for initialization
	void Start () {
        graphScript = GameObject.FindObjectOfType(typeof(spt_createGraphForTestingEnvironment)) as spt_createGraphForTestingEnvironment;
        waypointGraph = graphScript.getWaypointGraph();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[22].position);
	}
	
	// Update is called once per frame
	void Update () {
        if (agent.remainingDistance < 2){
            chooseDestination();
        }
	}

    void chooseDestination(){
        int numOptions = waypointGraph[currentWaypoint].Length;
        int newDestination = Random.Range(0, numOptions);
        currentWaypoint = waypointGraph[currentWaypoint][newDestination];
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

}
