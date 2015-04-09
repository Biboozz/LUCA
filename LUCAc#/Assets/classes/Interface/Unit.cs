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

		/*if(selected && Input.GetMouseButtonUp(1))
		{
			Vector3 destination = CameraOperator.GetDestination();
			if(destination != Vector3.zero)
			{
				// gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);
				moveToDest = destination;
				moveToDest.y += floorOffset;
			}
		}
		UpdateMove();*/
	}

	/*private void UpdateMove()
	{
		if (moveToDest != Vector3.zero && transform.position != moveToDest) {
			Vector3 direction = (moveToDest - transform.position).normalized;
			direction.y = 0;
			//transform.GetComponent<Rigidbody>().velocity = direction * speed;

			if (Vector3.Distance(transform.position, moveToDest) < stopDistanceOffset) {
				moveToDest = Vector3.zero;
			}
		} 
		else 
		{
			//transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}*/

	private void OnMouseDown()
	{
		if (I.isPlayed)
		{
			I.isSelectioned = !I.isSelectioned;
			if (I.isSelectioned) 
			{
				I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = new Color (1,1,0,1);
				I.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color (1,1,0,1);
				selected = true;
				selectedByClick = true;
			}
			else
			{
				I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = baseColor;
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
