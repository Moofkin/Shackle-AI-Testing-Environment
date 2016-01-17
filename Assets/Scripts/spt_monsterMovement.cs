//Created by: Lauren Cunningham

using UnityEngine;
using System.Collections;

public class spt_monsterMovement : MonoBehaviour {

    // Array of waypoints, the graph that holds the waypoints, as well as the script that instantiates the graph itself 
    public Transform[] waypoints;
    private int[][] waypointGraph;
    private spt_createGraphForTestingEnvironment graphScript;

    // The waypoint that the monster is currently travelling toward
    private int currentWaypoint;

    // Used to guide the monster's movement
    private NavMeshAgent agent;

    // Used for motivation calculations
    public Transform[] thingsThatMakeMonsterAngry;
    private float fieldOfViewDegrees = 110f;
    private int visibilityDistance = 40;

    // Use this for initialization
	void Start () {

        //Gets the waypoint graph from another script, then sets the first waypoint to the center of the room.
        graphScript = GameObject.FindObjectOfType(typeof(spt_createGraphForTestingEnvironment)) as spt_createGraphForTestingEnvironment;
        waypointGraph = graphScript.getWaypointGraph();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[26].position);
        currentWaypoint = 26;
	}
	
	// Update is called once per frame
	void Update () {
        if (agent.remainingDistance < 2){
            chooseDestination();
        }
        bool nothingInFieldOfView = true;
        for (int i = 0; i < thingsThatMakeMonsterAngry.Length; i++){
            if (canSeeSomething(thingsThatMakeMonsterAngry[i])){
                nothingInFieldOfView = false;
            }
        }
        if (nothingInFieldOfView)
            print("Cannot see anything");
	}

    void chooseDestination(){
        int numOptions = waypointGraph[currentWaypoint].Length;
        int newDestination = Random.Range(0, numOptions);
        currentWaypoint = waypointGraph[currentWaypoint][newDestination];
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    bool canSeeSomething(Transform item){
        RaycastHit hit;

        // Position of the monster in 3D space
        Vector3 alteredMonsterPosition = transform.position;
        
        // Position of the item that makes the monster angry in 3D space
        Vector3 alteredItemPosition = item.transform.position;
        
        // Ray drawn between the monster and the item
        Vector3 rayDirection = alteredItemPosition - alteredMonsterPosition;

        // Detects if the item is even within the view of the monster
        if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f){
            
            // Detects if the item is blocked by another object in the world
            if (Physics.Raycast(alteredMonsterPosition, rayDirection, out hit, visibilityDistance)){
                print ("Can see " + hit.collider.gameObject.name);
                return (hit.transform.CompareTag("target"));
            }
        }
        return false;
    }

}
