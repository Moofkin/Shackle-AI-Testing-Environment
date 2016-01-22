//Created by: Lauren Cunningham

/** This file is attached to all objects that are used as anger increasers for the monster.
 *  It instantiates a custom-made base anger class variable for each that stores data regarding how the monster is affected by that object.
 *  
 *  In the scope of the world, any game object with this script attatched is an empty game object used to track the actions of the players within the world.
 *  They are not visible in any way within the game (to the players), but they contain a variable that allows the monster to "see" or "not see" them.
 *  There will be one of these for every action that the players can do to anger the monster.
 *  When the players perform that specific action, the game object with this attatched will turn its visibility on and travel to the location
 *      that the action took place in.
 *  These allow for easy interractions between the players' actions and the monster. **/

using UnityEngine;
using System.Collections;

public class spt_angerObject : MonoBehaviour {

    // Base anger class variable used to store the specifics of each action.
    //  This could be an actual base class object, or one of its children.
    private spt_baseAngerClass data;

    // For setting the initial amount that the object angers the monster
    public int angerNum;

    // Handle on the renderer, and visibility interval (for testing purposes).
    private Renderer rend;
    public int visibleToggleInterval;

    //Handle on the monster so its position can be tracked and anger can be updated.
    private spt_monsterMovement monster;

    // Is this action a flashlight-tracker?
    public bool isFlashlight;
    
    // Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = false;

        monster = GameObject.FindObjectOfType<spt_monsterMovement>();

        if (isFlashlight)
            data = new spt_flashlightClass(angerNum, rend);
        else
            data = new spt_baseAngerClass(angerNum, rend);

        // toggles the visibility of objects on an interval (for testing purposes)
        InvokeRepeating("toggleVisibility", visibleToggleInterval, visibleToggleInterval);

        // Performs the "per-tick" calculations needed on actions with a duration (like the flashlight).
        InvokeRepeating("incrementTimer", data.getDuration(), data.getDuration());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Returns the base anger class variable
    public spt_baseAngerClass getData(){
        return data;
    }

    public void toggleVisibility(){
        getData().toggleVisibility();

        //if the object becomes visible...
        if (data.getSeen() == false){

            //if the monster can immediately see the object that just came into existence...
            if (monster.canSeeSomething(this.transform)){

                //Update the monster's anger with an increased field of view boost.
                monster.updateAnger(data.getAnger() + data.getAnger());
            }
            else
                monster.updateAnger(data.getAnger());

            //mark the object as "seen" i.e. the monster has acknowledged its initial presence.
            data.markAsSeen();
        }
    }

    
    // Function that is called "per-tick". Used for actions that have a duration.
    public void incrementTimer(){

        //if the action is visible to the monster and does in fact have a duration...
        if (data.getVisible() && data.getHasDuration()){
            
            // Find out if the monster can see that action
            if (monster.canSeeSomething(transform)) {
                
                // Add additional anger if the monster can observe the action taking place
                monster.updateAnger(data.getAngerIncrease());
            }
            
            // Add base amount of anger for the action happening
            monster.updateAnger(data.getAngerIncrease());
        }
    }

    // Allows you to easily move this object to another position in 3D space.
    public void moveObject(Transform pos){
        this.transform.position = pos.position;
    }
}
