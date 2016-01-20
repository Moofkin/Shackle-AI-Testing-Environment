//Created by: Lauren Cunningham

/** This is the base anger class.
 * It is the top of an inheritance heirarchy that is used to hold data regarding how the monster is affected by the players' actions. **/

using UnityEngine;
using System.Collections;

public class spt_baseAngerClass{

    // Default anger increase for any action.
    private int anger = 5;

    // By default, actions cannot be seen by the monster.
    private bool isVisible = false;

    private bool hasBeenSeen = true;

    private Renderer rend;

    // Default constructor for this class. Private so that it cannot be used (forces the use of the other constructor).
    private spt_baseAngerClass() { }

    // Other constructor. Requires the user to pass an integer, which will be used for the anger value.
    public spt_baseAngerClass(int a, Renderer r){
        anger = a;
        rend = r;
    }

    // Sets the visibility
    public void setVisible(bool i){
        isVisible = i;
    }

    // Gets the visibility
    public bool getVisible(){
        return isVisible;
    }

    // Gets the anger cooefficient for the action.
    public int getAnger(){
        return anger;
    }

    public void toggleVisibility(){
        if (isVisible){
            setVisible(false);
            rend.enabled = false;
        }
        else{
            setVisible(true);
            rend.enabled = true;
            hasBeenSeen = false;
        }
    }

    public void markAsSeen(){
        hasBeenSeen = true;
    }

    public bool getSeen(){
        return hasBeenSeen;
    }
}
