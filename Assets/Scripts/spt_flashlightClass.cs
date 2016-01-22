//Created by: Lauren Cunningham

/** This is the flashlight class.
 * This is a class that inerits from the base anger class, and is used to track the use of the players' flashlights. **/

using UnityEngine;
using System.Collections;

public class spt_flashlightClass : spt_baseAngerClass{
    
    // The constructor sets a duration and per-tick anger increase value
    public spt_flashlightClass(int a, Renderer r) :base(a, r){
        hasDuration = true;
        duration = 0.5f;
        angerIncrease = 1;
    }
}
