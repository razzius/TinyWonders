using UnityEngine;

public class MoveCylinder : MonoBehaviour
{
    public float speed = 4f;
    private Quaternion startRotation;
    public GameObject leftMandible2;
    public GameObject rightMandible2;
    void Start()
    {
        startRotation = leftMandible2.transform.localRotation;
    }
    // Update is called once per frame
    void Update()
    {
	    float xDirection = Input.GetAxis("Horizontal");
	    float zDirection = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space)) {
            leftMandible2.transform.localEulerAngles = new Vector3(10.586f, -4.0f, -88.34f);
            rightMandible2.transform.localEulerAngles = new Vector3(0, 30, 0);
        } else {
            leftMandible2.transform.localEulerAngles = new Vector3(10.586f, 14.507f, -88.34f);
            rightMandible2.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
		Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

		transform.position += moveDirection * Time.deltaTime * speed;
    }
}
