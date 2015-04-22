using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class displayPerkTree : MonoBehaviour {

	public Canvas canvas;
	public GameObject GUIhexagon;
	private bool _shown;

	// Use this for initialization
	void Start () {
		GUIhexagon = (GameObject)Instantiate (GUIhexagon, new Vector3 (transform.localPosition.x + 100, transform.localPosition.z + 100, 0), canvas.transform.rotation);
		GUIhexagon.transform.SetParent (canvas.transform, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void display()
	{

	}

	public bool shown
	{
		get
		{
			return _shown;
		}
	}

	public void Initialize(List<skill> skillList)
	{
		foreach (skill S in skillList) 
		{
			S.hex = (GameObject)Instantiate (GUIhexagon, new Vector3 (transform.localPosition.x, transform.localPosition.z + 100, 0), canvas.transform.rotation);
			S.hex.transform.SetParent (canvas.transform, false);
		}
	}

	private void place(skill S)
	{

	}
}
