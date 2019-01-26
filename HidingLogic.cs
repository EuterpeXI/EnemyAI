using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script plays the middle man between the AI logic and the bush triggers while
 * keeping the bush triggers private.
 */ 
public class HidingLogic : MonoBehaviour {

    private bool playerIsHiding;

    /*
     * This function returns a boolean for if the player is hiding or not. 
     */ 
    public bool get_hide()
    {
        return playerIsHiding;
    }

    /*
     * This function is used by the script "bushTrigger" to set the bool value of 
     * hiding.
     */ 
    public void set_hide(bool newHide)
    {
        playerIsHiding = newHide;
    }
}
