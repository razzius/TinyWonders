using System;
using UnityEngine;

public class MoveBug : MonoBehaviour
{
    private readonly float maxSpeed = 12f;
    private readonly float acceleration = 5f;
    private readonly float spinSpeed = 30f;

    private Transform bugTransform;
    private float speed = 0f;

    void Start()
    {
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

		bugTransform.position += bugTransform.rotation * Vector3.back * (speed * Time.deltaTime);

		if (Input.GetAxis("Horizontal") > 0f)
		{
			bugTransform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
		}
		else if (Input.GetAxis("Horizontal") < 0f)
		{
			bugTransform.Rotate(0f, -spinSpeed * Time.deltaTime, 0f);
		}
    }
}
