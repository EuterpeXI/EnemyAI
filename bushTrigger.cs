using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bushTrigger : MonoBehaviour {

    private HidingLogic logicScript;
    private bool hiding = false;

    void Start()
    {
        logicScript = GameObject.Find("Test").GetComponent<HidingLogic>();
    }

    /*
     * This trigger function sets hiding to true once the player is within a bush and 
     * sends the information to the logicScript
     */
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            hiding = true;
            logicScript.set_hide(hiding);
        }
        Debug.Log("Player has entered the bush... incognito mode on");
    }

    /*
     * This trigger function sets the hiding to false once the player exits the bush
     * and sends the information to the logicScript
     */ 
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hiding = false;
            logicScript.set_hide(hiding);
        }
        Debug.Log("Player has exited the bush... ");
    }
}
