//Created by: Lauren Cunningham

/** This file is attached to all objects that are used as anger increasers for the monster.
 *  It instantiates a custom-made base anger class variable for each that stores data regarding how the monster is affected by that object.**/

using UnityEngine;
using System.Collections;

public class spt_angerObject : MonoBehaviour {

    private spt_baseAngerClass data;

    // For setting the initial amount that the object angers the monster
    public int angerNum;

    private Renderer rend;
    public int visibleToggleInterval;
    
    // Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        
        // Creates a base anger class variable for each object
        data = new spt_baseAngerClass(angerNum, rend);

        InvokeRepeating("toggleVisibility", visibleToggleInterval, visibleToggleInterval);
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
    }
}
