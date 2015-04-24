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
	//private float angle;
	private Vector3 moveToDest = Vector3.zero;

	private GameObject target;
	private Vector3 newPosition;
	private float timeTaken;
	private Vector3 MovingDirection = Vector3.up;

	void Start () 
	{
		I = gameObject.GetComponentInParent<Individual>();
		baseColor = I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color;
		newPosition = transform.position;
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
			target = GameObject.FindGameObjectWithTag("target");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				newPosition = hit.point;
				target.transform.position = newPosition;	//Object target prend position du clic
				I.gotDest = true;		//Objet possède une destination
			}
		}

		if(I.gotDest)
		{
			if(transform.position == target.transform.position)
			{
				I.gotDest = false;		//Plus de destination car elle a été atteinte
			}
			else
			{
				timeTaken += (float) move(transform.position, target);
			}
		}
	}

	/*public bool moveSomething(Vector3 start, GameObject end){ // return bool when finished moving
		
		if(start.transform.position == end.transform.position){
			return false;
		}else{
			timeTaken += (float) move(start, end);
			return true;
		}
	}*/

	public float move(Vector3 start, GameObject end)
	{
		start = Vector3.Lerp(start, end.transform.position, Time.deltaTime*10f);
		return Time.deltaTime*10f;
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
