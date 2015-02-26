using UnityEngine;
using System.Collections;

public class InterfaceDetection : MonoBehaviour {

    private bool press_c = false;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.C))
        {
            RTSCamera(!press_c);
            press_c = !press_c;
        }
	}

    public void RTSCamera(bool press_c)
    {
        if (press_c == false)
        {
            Camera.main.GetComponent<rtsmove>().enabled = true;
        }
        else
        {
            Camera.main.GetComponent<rtsmove>().enabled = false;
        }
    }
}
