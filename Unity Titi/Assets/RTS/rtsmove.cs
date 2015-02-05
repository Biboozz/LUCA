using UnityEngine;
using System.Collections;

public class rtsmove : MonoBehaviour {

    public float horizontalSpeed = 40;
    public float verticalSpeed = 40;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * vertical);
        transform.Translate(Vector3.right * horizontal);
    
    }
}
