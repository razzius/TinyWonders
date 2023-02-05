using UnityEngine;

public class MoveCylinder : MonoBehaviour
{
    

    public float speed = 4f;
    private Quaternion startRotation;
    public GameObject leftMandible2;
    public GameObject rightMandible2;
    public Camera _mainCamera;
    public Rigidbody rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        startRotation = leftMandible2.transform.localRotation;
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
    private void Update()
     {
         Vector3 movDir;
 
         transform.Rotate(0, Input.GetAxis("Horizontal") * 90 * Time.deltaTime, 0);
         movDir = transform.forward * Input.GetAxis("Vertical") * 6;
         rigidBody.velocity = movDir;
         
        if (Input.GetKey(KeyCode.Space)) {
            leftMandible2.transform.localEulerAngles = new Vector3(10.586f, -4.0f, -88.34f);
            rightMandible2.transform.localEulerAngles = new Vector3(0, 30, 0);
        } else {
            leftMandible2.transform.localEulerAngles = new Vector3(10.586f, 14.507f, -88.34f);
            rightMandible2.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        //  float _targetRotation = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg +
        //                           _mainCamera.transform.eulerAngles.y;
         
     }
}
