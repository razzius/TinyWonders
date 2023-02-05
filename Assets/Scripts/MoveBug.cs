using System;
using UnityEngine;

public class MoveBug : MonoBehaviour
{
    private readonly float maxSpeed = 12f;
    private readonly float acceleration = 5f;
    private readonly float maxSpinSpeed = 60f;

    private Rigidbody bugRigibody;
    private Transform bugTransform;
    private float speed = 0f;
    private float spinSpeed = 0f;

    void Start()
    {
	    bugRigibody = gameObject.GetComponent<Rigidbody>();
	    bugTransform = gameObject.GetComponent<Transform>();
    }
    
    void Update()
    {
		if (Input.GetAxis("Vertical") > 0f)
		{
			speed = Mathf.Clamp(speed + acceleration * Time.deltaTime, 0f, maxSpeed);
		}
		else
		{
			speed = Mathf.Clamp(speed - 2f * acceleration * Time.deltaTime, 0f, maxSpeed);
		}

		if (Input.GetAxis("Horizontal") > 0f)
		{
			spinSpeed = maxSpinSpeed;
		}
		else if (Input.GetAxis("Horizontal") < 0f)
		{
			spinSpeed = -maxSpinSpeed;
		}
		else
		{
			spinSpeed = 0;
		}
    }

    void FixedUpdate()
    {
		bugRigibody.MoveRotation(bugTransform.rotation * Quaternion.Euler(0f, spinSpeed * Time.deltaTime, 0f));
		bugRigibody.MovePosition(bugTransform.position + bugTransform.rotation * Vector3.back * (speed * Time.deltaTime));
    }
}
