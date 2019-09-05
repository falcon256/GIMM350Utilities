using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Movement Utility Script, modified by Daniel Lambert
/// @author 256
/// </summary>

public class UtilitiesPlayerMovement : MonoBehaviour {

	public enum MoveType { VectorDirect, RotationDirect, Translate, Lerp, RigidBodyArrows, RigidBodyMouse}

    public MoveType moveType;

    public float speed = 10f;
    public float rotSpeed = 100f;
    public float lerpRotationSpeed = 2f;
    public float stoppingDistance = 1f;

    public LayerMask layerMask;
    public Vector3 goal;
    public Quaternion rot;

    Rigidbody rb;
    
	void Start () {
        goal = transform.position;
        rot = transform.rotation;
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        Vector3 position = transform.position;
		switch (moveType)
            {
		    case MoveType.VectorDirect:
			    position.x += Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
			    position.z += Input.GetAxis ("Vertical") * Time.deltaTime * speed;
			    transform.position = position;
                break;

            case MoveType.RotationDirect:
                transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y+(Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed),0);
                position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;
                transform.position = position;
                break;

            case MoveType.Translate:
                getMousePosition();
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                Vector3 direction = goal - transform.position;
                if(direction.magnitude > stoppingDistance)
                {
                    //added my own "Move when facing is correct" stuff.
                    float angleDif = 1.0f + Vector3.Angle(transform.forward, direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
                    transform.Translate((direction.normalized * speed * Time.deltaTime)/angleDif, Space.World);
                }
                break;

            case MoveType.Lerp:
                getMousePosition();
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                if(Vector3.Distance(transform.position, goal) > stoppingDistance)
                {
                    transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime);
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (moveType)
        {
            case MoveType.RigidBodyArrows:
                goal = transform.forward;
                float translation = Input.GetAxis("Vertical") * speed;
                float rotation = Input.GetAxis("Horizontal") * rotSpeed;
                translation *= Time.deltaTime;
                rotation *= Time.deltaTime;
                Quaternion turn = Quaternion.Euler(0f, rb.rotation.eulerAngles.y+rotation, 0f);              
                rb.MovePosition(transform.position + (goal * translation));
                rb.MoveRotation(Quaternion.Lerp(rb.rotation, turn, 0.5f));
                break;
            case MoveType.RigidBodyMouse:
                getMousePosition();
                Quaternion lerpRot = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * lerpRotationSpeed);
                rb.MoveRotation(lerpRot);
                if(Vector3.Distance(transform.position, goal) > stoppingDistance)
                {
                    Vector3 lerpTarget = Vector3.Lerp(transform.position, goal, Time.deltaTime * speed);
                    rb.MovePosition(lerpTarget + transform.forward * speed * Time.deltaTime);
                }
                break;
        }
    }
    public void getMousePosition()
    {
        if (Input.GetMouseButtonDown(0)&&Camera.main!=null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 10000, layerMask, QueryTriggerInteraction.Ignore))
            {
                goal = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Vector3 direction = goal - transform.position;
                rot = Quaternion.LookRotation(direction);
            }
        }
    }
}
