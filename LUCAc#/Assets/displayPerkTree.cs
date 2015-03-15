using UnityEngine;
using System.Collections;

public class displayPerkTree : MonoBehaviour {

	public Canvas canvas;
	public GameObject GUIhexagon;

	// Use this for initialization
	void Start () {
		canvas = GetComponentInParent<getCanvas> ().canvas;
		GUIhexagon = (GameObject)Instantiate (GUIhexagon, new Vector3 (transform.localPosition.x, transform.localPosition.z, 0), canvas.transform.rotation);
		GUIhexagon.transform.SetParent (canvas.transform, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void display()
	{

	}
}
