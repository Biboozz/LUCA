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

		if (selected) 
		{
			I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = new Color (1,1,0,1);
			I.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color (1,1,0,1);
		} 
		else 
		{
			I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = baseColor;
			I.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = baseColor;
		}
	}
}
