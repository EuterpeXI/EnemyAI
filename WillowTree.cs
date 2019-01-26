using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WillowTree : MonoBehaviour {

    public Canvas winScreen;

    /*
     * If player clicks on the tree, they win the game
     */
    void OnMouseDown()
    {
        winScreen.enabled = true;
    }
}
