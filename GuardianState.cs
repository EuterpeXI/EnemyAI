using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardianState : MonoBehaviour {

    public NavMeshAgent agent;
    public List<Transform> waypoints;
    public List<GameObject> bushes;
    public GameObject player;
    public Canvas gameOverScreen;
    public float attackRange;
    public float maxPursuitDistance;

    private bool isPathing;
    private bool seePlayer = false;
    private bool isHiding = false;
    private bool isClose;
    private bool hidingCheck;
    private float distanceToPlayer;
    private float AIDistance;
    private float speed = 8f;
    private int totalWaypoints;
    private int waypointIndex = 0;
    private bushTrigger bushHiding;
    private HidingLogic logicScript;


    // Use this for initialization
    void Start () {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        totalWaypoints = waypoints.Capacity;
        logicScript = GameObject.Find("Test").GetComponent<HidingLogic>();
    }
	
	// Update is called once per frame
	void Update () {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        seePlayer = checkHiding();

        // If the distance to player is less than 100 and they are not hiding in a bush, NPC can see player. 
        // This also handles when the player exits the bush and their distance is less than 100
        if (distanceToPlayer < 100 && !seePlayer)
        {
            Debug.Log("I see the player!");
            approachPlayer();

        }
        else
        {
            // Return to pathing because player is hiding or out of bounds. 
            Debug.Log("Don't see the player...");
            handlePathing();
        }
	}

    /*
     * This function handles the NPC pre-determined pathing using waypoints. 
     */ 
    private void handlePathing()
    {
        AIDistance = Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position);
        agent.SetDestination(waypoints[waypointIndex].transform.position);
        agent.speed = speed;

        // If the distance to the next waypoint is close, increment to the next destination.
        if (AIDistance < 10)
        {
            waypointIndex++;
        }

        // If the waypoint's list has reached the end, loop back.
        if (waypointIndex == waypoints.Capacity)
        {
            waypointIndex = 0;
        }
    }

    /*
     * This function handles the NPC's movement towards the player. 
     */ 
    private void approachPlayer()
    {
        // Calculate distance from NPC to player
        AIDistance = Vector3.Distance(transform.position, player.transform.position);

        // If the player is out of the pursuit distance, we want to go back to our pathing
        if (AIDistance > maxPursuitDistance)
        {
            seePlayer = false;
        }

        // If the player is hiding in the bushes, NPC will stop following
        isHiding = checkHiding();
        Debug.Log("Player hiding = " + isHiding);
        if (isHiding)
        {
            seePlayer = false;
        }

        // If the distance is within the attack range, we want to attack
        if (AIDistance < attackRange)
        {
            // Attack
            Debug.Log("Attacking Player");
        }
        else
        {
            // Follow the player
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
    }

    /*
     * This function checks to see if the player is hiding in a bush.
     * Returns true if player is hiding. False if not.
     */ 
    private bool checkHiding()
    {
        hidingCheck = logicScript.get_hide();

        return hidingCheck;
    }

    /*
     * If the NPC collides with the player, game over for the player.
     */ 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameOverScreen.enabled = true;
        }
    }
}
