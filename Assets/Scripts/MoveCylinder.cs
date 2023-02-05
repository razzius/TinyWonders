using UnityEngine;
using System.Collections;

public class MoveCylinder : MonoBehaviour
{
    

    public float speed = 4f;
    private Quaternion startRotation;
    public GameObject leftMandible2;
    public GameObject rightMandible2;
    public Camera _mainCamera;
    public Rigidbody rigidBody;
    public Collider mandibleCollider;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        startRotation = leftMandible2.transform.localRotation;
        mandibleCollider.enabled = false;
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
    private IEnumerator SnipCoroutine() {
        mandibleCollider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        mandibleCollider.enabled = false;
    } 

    private void snip() {
        StartCoroutine(SnipCoroutine());
    }

    private void Update()
     {
         Vector3 movDir;
        float yRotation = (Input.GetAxis("Horizontal") * 90 * Time.deltaTime) + transform.rotation.eulerAngles.y;
         transform.rotation = Quaternion.Euler(0, yRotation, 0);
         movDir = transform.forward * Input.GetAxis("Vertical") * 6;
         rigidBody.velocity = movDir;
                    // Debug.Log("OK1111!");
         if (Input.GetKeyDown(KeyCode.Space)) {
            // Debug.Log("OK!");
            snip();
         }
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
