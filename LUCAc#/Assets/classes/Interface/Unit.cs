using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	private Individual I;
	private Color baseColor;
	public bool selected = false;

	void Start () 
	{
		I = gameObject.GetComponentInParent<Individual>();
		baseColor = I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color;
	}

	private void Update ()
	{
		if (GetComponent<Renderer>().isVisible && Input.GetMouseButton(0)) 
		{
			Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
			camPos.y = CameraOperator.InvertMouseY(camPos.y);
			selected = CameraOperator.selection.Contains(camPos);
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			I.isSelectioned = true; 
		}
		else
		{
			I.isSelectioned = false;
		}

		if (selected) 
		{
			I.isSelectioned = true;
		} 
		else 
		{
			I.isSelectioned = false;
		}
	}
}
