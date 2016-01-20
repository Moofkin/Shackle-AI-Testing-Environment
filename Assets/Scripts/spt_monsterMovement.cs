//Created by: Lauren Cunningham

/** This file is the one that ultimately governs the monster's behaviors. **/

using UnityEngine;
using UnityEngine.UI;
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

    private int angerLevel;
    
    // Used for motivation calculations
    public GameObject[] thingsThatMakeMonsterAngry;
    private float fieldOfViewDegrees = 110f;
    private int visibilityDistance = 20;

    private Text gui;

    // Use this for initialization
	void Start () {

        //Gets the waypoint graph from another script, then sets the first waypoint to the center of the room.
        graphScript = GameObject.FindObjectOfType(typeof(spt_createGraphForTestingEnvironment)) as spt_createGraphForTestingEnvironment;
        waypointGraph = graphScript.getWaypointGraph();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[26].position);
        currentWaypoint = 26;
        angerLevel = 0;
        gui = GameObject.FindWithTag("gui").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        // Chooses a new destination if the monster is within a certain distance of its current one.
        if (agent.remainingDistance < 2 && currentWaypoint != 999){
            chooseDestination();
        }

        // Goes through the list of objects that make the monster angry in order to determine if the players have done things it doesn't like.
        bool nothingInFieldOfView = true;
        for (int i = 0; i < thingsThatMakeMonsterAngry.Length; i++){
            if (canSeeSomething(thingsThatMakeMonsterAngry[i].transform)){
                nothingInFieldOfView = false;
            }
        }
        if (nothingInFieldOfView)
            print("Cannot see anything");

        gui.text = ("Anger level: " + angerLevel + "%");

	}

    // Used to choose a new destination for the monster based on its current destination (used for wandering).
    //  This is dependant on the graph of waypoints which is created in  the spt_createGraphFor... scripts.
    void chooseDestination(){
        int numOptions = waypointGraph[currentWaypoint].Length;
        int newDestination = Random.Range(0, numOptions);
        currentWaypoint = waypointGraph[currentWaypoint][newDestination];
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    //Determines if the monster can see an object from its current position.
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
                
                // Makes sure the object being looked at is something that angers the monster. MUST HAVE A "target" TAG
                if (hit.transform.CompareTag("target")){

                    // Makes sure the object being looked at is visible to the monster
                    if (hit.collider.gameObject.GetComponent<spt_angerObject>().getData().getVisible()){
                        print("Can see " + hit.collider.gameObject.name);
                        if (hit.collider.gameObject.GetComponent<spt_angerObject>().getData().getSeen() == false){
                            hit.collider.gameObject.GetComponent<spt_angerObject>().getData().markAsSeen();
                            angerLevel = angerLevel + hit.collider.gameObject.GetComponent<spt_angerObject>().getData().getAnger();
                            if (angerLevel >= 100)
                                attack();
                            return (true);
                        }
                    }
                }
            }
        }
        return false;
    }

    private void attack(){
        int attackOrNot = Random.Range(0, 2);
        if (attackOrNot == 1){
            currentWaypoint = 999;
            agent.SetDestination(GameObject.FindWithTag("Player").transform.position);
        }
    }
}
