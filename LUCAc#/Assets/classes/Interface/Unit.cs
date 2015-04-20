using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	private Individual I;
	private Color baseColor;
	public bool selected = false;

	public float floorOffset = 1;
	public float speed = 5;
	public float stopDistanceOffset = 0.5f;

	private bool selectedByClick = false;
	private float angle;
	private Vector3 moveToDest = Vector3.zero;

	void Start () 
	{
		I = gameObject.GetComponentInParent<Individual>();
		baseColor = I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color;
	}

	private void Update ()
	{
		if (GetComponent<Renderer>().isVisible && Input.GetMouseButton(0)) 
		{
			if(!selectedByClick)
			{
				Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
				camPos.y = CameraOperator.InvertMouseY(camPos.y);
				selected = CameraOperator.selection.Contains(camPos);

				if(selected)
				{
					I.isSelectioned = true;
				}
				else
				{
					I.isSelectioned = false;
				}
			}
		}

		if(selected && Input.GetMouseButtonDown(1))		//Si sélectionné et clic droit
		{
			Vector3 dest = CameraOperator.GetDestination();
			if(dest.x >= transform.position.x && dest.z >= transform.position.z)
				angle = Mathf.Tan((dest.x - transform.position.x)/(dest.z - transform.position.z)) * Mathf.Rad2Deg;
			if(dest.x > transform.position.x && dest.z < transform.position.z)
				angle = Mathf.Tan((dest.x - transform.position.x)/(transform.position.z - dest.z)) * Mathf.Rad2Deg;
			if(dest.x <= transform.position.x && dest.z <= transform.position.z)
				angle = Mathf.Tan((transform.position.x - dest.x)/(transform.position.z - dest.z)) * Mathf.Rad2Deg;
			if(dest.x < transform.position.x && dest.z > transform.position.z)
				angle = Mathf.Tan((transform.position.x - dest.x)/(dest.z - transform.position.z)) * Mathf.Rad2Deg;

			transform.Rotate(0, 0, angle);
		}
	}

	private void OnMouseDown()
	{
		if (I.isPlayed)
		{
			I.isSelectioned = !I.isSelectioned;
			if (I.isSelectioned) 
			{
				I.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color (1,1,0,1);
				selected = true;
				selectedByClick = true;
			}
			else
			{
				I.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = baseColor;
				selected = false;
				I.isSelectioned = false;
			}
		}
	}

	private void OnMouseUp()
	{
		if (selectedByClick) 
		{
			selected = true;
			I.isSelectioned = true;
		}
		else 
		{
			selected = false;
			I.isSelectioned = false;
		}
		selectedByClick = false;
	}
}
