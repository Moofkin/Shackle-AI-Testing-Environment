//Created by: Lauren Cunningham

/** This file is the one that ultimately governs the monster's behaviors. **/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spt_monsterMovement : MonoBehaviour {

    // Handle on the players (so the monster always knows where they are).
    public GameObject players;
    
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
    private float fieldOfViewDegrees = 110f;
    private int visibilityDistance = 20;

    private Text gui;

    // A boolean used to track if the monster has warned the players that it is unhappy
    private bool hasGivenWarning;

    private int upperThreshold = 150;
    private int lowerThreshold = 100;

    // Use this for initialization
	void Start () {

        //Gets the waypoint graph from another script, then sets the first waypoint to the center of the room.
        graphScript = GameObject.FindObjectOfType(typeof(spt_createGraphForTestingEnvironment)) as spt_createGraphForTestingEnvironment;
        waypointGraph = graphScript.getWaypointGraph();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[26].position);
        currentWaypoint = 26;
        
        // Sets the initial anger level of the monster to zero.
        angerLevel = 0;
        
        // Handle on the gui (used for testing)
        gui = GameObject.FindWithTag("gui").GetComponent<Text>();

        //Begins the gradual anger depreciation over time.
        InvokeRepeating("angerDepreciation", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {

        // Chooses a new destination if the monster is within a certain distance of its current one.
        if (agent.remainingDistance < 2 && currentWaypoint != 999){
            chooseDestination();
        }

        //If the monster is past a certain amount of anger, initiate an attack.
        if (angerLevel >= lowerThreshold)
            attack();

        // Update the GUI with the current anger level of the monster.
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
    public bool canSeeSomething(Transform item){
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
                        return (true);
                    }
                }
            }
        }
        return false;
    }

    
    // Attack function for the monster.
    private void attack(){

        // Monster can decide to attack or not
        int attackOrNot = Random.Range(0, 3);
        
        // the monster will attack if the random number allows it, or if its anger has exceeded the top bound of its threshold.
        if (attackOrNot == 0 || (angerLevel >= upperThreshold)){
            
            // If the monster has not already warned the players of its anger, it does. 
            if (hasGivenWarning == false){
                // Play warning noise.
                hasGivenWarning = true;
            }
            
            // If the players have already been warned, it will attack
            else{
                currentWaypoint = 999;
                agent.SetDestination(players.transform.position);
            }
        }
    }

    // Increases the anger by an integer amount.
    public void updateAnger(int i){
        angerLevel = angerLevel + i;
    }

    // Called evey second. Allows anger to depreciate over time.
    private void angerDepreciation(){
        if (angerLevel != 0)
            angerLevel = angerLevel - 1;
        
        // If the level of anger dips a bit below the bottom threshold, the monster will need to warn the players again if it gets angry.
        //  (This happens before it is allowed to actually attack).
        if (angerLevel <= (lowerThreshold - 10))
            hasGivenWarning = false;
    }
}
