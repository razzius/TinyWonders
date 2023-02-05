using UnityEngine;
using System.Collections;

public class MoveCylinder : MonoBehaviour
{
    

    public float speed = 4f;
    private float cutterActiveSeconds = 0.3f;
    private float cutSmoothTime = 0.01f;
    private Quaternion startRotation;
    public Camera _mainCamera;
    private Rigidbody rigidBody;
    public Cutter leftCutter;
    public Cutter rightCutter;

    private float leftMandibleClosedYRotation = -4f;
    private float rightMandibleClosedYRotation = 7f;
    private float leftMandibleOpenYRotation = 22f;
    private float rightMandibleOpenYRotation = -22f;

    private bool isCutting;
    private float yRotation;
    private float leftCutterCurrentRotationVelocity;
    private float rightCutterCurrentRotationVelocity;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        ActivateCutters(false);
        yRotation = transform.localRotation.eulerAngles.y;
    }
    // Update is called once per frame
    // void Update()
    // {
	//     float xDirection = Input.GetAxis("Horizontal");
	//     float zDirection = Input.GetAxis("Vertical");

    //     if (Input.GetKey(KeyCode.A))
    //     {
    //         transform.RotateAround(gameObject.transform.position, Vector3.down, 20 * Time.deltaTime);
    //     }
	// 	Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

	// 	transform.position += moveDirection * Time.deltaTime * speed;
    // }
    private IEnumerator CutCoroutine()
    {

        float cutStartTime = Time.time;
        isCutting = true;
        leftCutter.isCutting = true;
        rightCutter.isCutting = true;
        
        while(Time.time < cutStartTime + cutterActiveSeconds && isCutting)
        {
            yield return null;
        }

        ActivateCutters(false);
    } 

    private void ActivateCutters(bool activate)
    {
        if (activate)
        {
            if (isCutting)
            {
                return;
            }
            else
            {
                StartCoroutine(CutCoroutine());
            }
        }
        else
        {
            isCutting = false;
            leftCutter.isCutting = false;
            rightCutter.isCutting = false;
        }
    }

    private void Update()
     {
        // Movement
         Vector3 movDir;
         yRotation += Input.GetAxis("Horizontal") * 90 * Time.deltaTime;
         transform.localRotation = Quaternion.Euler(0, yRotation, 0);
         movDir = transform.forward * Input.GetAxis("Vertical") * 6;
         rigidBody.velocity = movDir;

        // Cutter Activation
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateCutters(true);
        }
        else
        {
            ActivateCutters(false);
        }

        // Mandible Rotation
        float leftMandibleTargetYRotation = isCutting ? leftMandibleClosedYRotation : leftMandibleOpenYRotation;
        print($"Current left Y rot: {leftCutter.transform.localRotation.y} - Target Y Rot: {leftMandibleTargetYRotation}");
        float newLeftMandibleYRotation = Mathf.SmoothDampAngle(leftCutter.transform.localEulerAngles.y, leftMandibleTargetYRotation, ref leftCutterCurrentRotationVelocity, cutSmoothTime);
        leftCutter.transform.localRotation = Quaternion.Euler(0, newLeftMandibleYRotation, 0);

        float rightMandibleTargetRotation = isCutting ? rightMandibleClosedYRotation : rightMandibleOpenYRotation;
        float newRightMandibleYRotation = Mathf.SmoothDampAngle(rightCutter.transform.localEulerAngles.y, rightMandibleTargetRotation, ref rightCutterCurrentRotationVelocity, cutSmoothTime);
        rightCutter.transform.localRotation = Quaternion.Euler(0, newRightMandibleYRotation, 0);

     }
}
