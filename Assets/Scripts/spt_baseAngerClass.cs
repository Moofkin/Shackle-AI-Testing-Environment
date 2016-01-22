//Created by: Lauren Cunningham

/** This is the base anger class.
 * It is the top of an inheritance heirarchy that is used to hold data regarding how the monster is affected by the players' actions. **/

using UnityEngine;
using System.Collections;

public class spt_baseAngerClass{

    // Default anger increase for any action. This does not take into account extra anger caused by proximity.
    protected int anger = 5;

    // By default, actions cannot be seen by the monster.
    protected bool isVisible = false;

    // Used to mark if the monster has acknowledged an object becoming visible.
    protected bool hasBeenSeen = true;

    // For actions that cause light. Anger Increase is the amount of additional anger the monster gets per tick of time.
    //  Duration is the amount of time between these ticks, with a default of zero for non-light producing actions or toggles (used mainly for the flashlight).
    protected bool hasDuration = false;
    protected float duration = 0;
    protected int angerIncrease = 0;

    // For toggling the visibility of standins (for testing purposes).
    protected Renderer rend;

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

    // Toggles the visibility of an object. For all intents and purposes, this is called whenever the players being or stop performing an action
    //  that angers the monster.
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

    // Used to mark if the object has been acknowledged by the monster
    public void markAsSeen(){
        hasBeenSeen = true;
    }

    // Used to find out if the object has been acknowledged by the monster
    public bool getSeen(){
        return hasBeenSeen;
    }

    // Used to find if this particular action has a duration attatched (does it have persistence like a flashlight shine?).
    public bool getHasDuration(){
        return hasDuration;
    }

    // Gets the time between ticks if there is a duration.
    //  Deafault is zero if there is no duration.
    public float getDuration(){
        return duration;
    }

    // Gets the per-tick anger increase amount (for actions with durations only).
    //  Default is zero if there is no duration.
    public int getAngerIncrease(){
        return angerIncrease;
    }
}
