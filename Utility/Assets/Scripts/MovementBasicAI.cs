using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - Daniel Lambert
//suggested use: Attach to a bunch of spheres with sphere colliders and rigid bodies. Have a few game objects placed in the patrol list and the player as the target.
//This simple script causes rigid bodies to patrol points by default. (Patrol)
//If the target is within followDistance they will follow it. (Approach)
//if the target looks at an object running this script it will run away from it.(Flee)
//For testing I ran this on 100 spheres with rigid bodies patroling between four boxes.
//this script doesn't care if you are playing on a plane, or floating in space, it works, just have a bit of drag on the rigid bodies.
public class MovementBasicAI : MonoBehaviour {

	public enum MOVEMENT_STATE {PATROL, FOLLOW, FLEE};
	public GameObject[] patrolPoints = null;
	public GameObject target = null;
	public float fleeDistance = 20.0f;
	public float fleeLookingAtMeAngle = 20.0f;
	public float followDistance = 1.0f;
    public float patrolPointDistThresh = 2.0f;

    public float patrolMovementStrength = 0.005f;
    public float followMovementStrength = 0.02f;
    public float fleeMovementStrength = 0.05f;


	private int currentPatrolPoint = 0;
	private MOVEMENT_STATE currentMovementState = MOVEMENT_STATE.PATROL;
	
	void FixedUpdate () {
        if (target == null)
            return;

        Vector3 vdif = this.transform.position - target.transform.position; //position difference
        float dif = Vector3.Angle(target.transform.forward, vdif); //angle difference between the targets view and our direction, y axis included.
        float dist = vdif.magnitude; //distance
        if (dif < fleeLookingAtMeAngle && dist < fleeDistance)
            currentMovementState = MOVEMENT_STATE.FLEE;
        else if (dist < fleeDistance)
            currentMovementState = MOVEMENT_STATE.FOLLOW;
        else
            currentMovementState = MOVEMENT_STATE.PATROL;

        switch(currentMovementState)
        {
            case (MOVEMENT_STATE.PATROL):
                doPatrol();break;
            case (MOVEMENT_STATE.FOLLOW):
                doFollow();break;
            case (MOVEMENT_STATE.FLEE):
                doFlee();break;
            default:
                doPatrol();break;
        }
	}

    //does the patrol behavior
    public void doPatrol()
    {
        if (patrolPoints == null || patrolPoints.Length <= 0)
            return;

        if ((this.transform.position - patrolPoints[currentPatrolPoint].transform.position).magnitude < patrolPointDistThresh)
            currentPatrolPoint++;

        if (currentPatrolPoint >= patrolPoints.Length)
            currentPatrolPoint = 0;

        Rigidbody rb = this.GetComponent<Rigidbody>();
        if (rb == null)
            return;

        rb.AddForce((patrolPoints[currentPatrolPoint].transform.position - this.transform.position) * patrolMovementStrength, ForceMode.Impulse);
    }

    //does the follow behavior
    public void doFollow()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        if (target==null || rb == null)
            return;
        rb.AddForce((target.transform.position - this.transform.position) * followMovementStrength, ForceMode.Impulse);
    }

    //does the flee behavior
    public void doFlee()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        if (target == null || rb == null)
            return;

        rb.AddForce((this.transform.position - target.transform.position) * fleeMovementStrength, ForceMode.Impulse);
    }
}
