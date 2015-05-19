using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	private Individual I;
	private Color baseColor;
	public bool selected = false;
	public float floorOffset = 1;
	public float stopDistanceOffset = 0.5f;

	private bool selectedByClick = false;

	private float angle;
	private Vector3 newPosition;
	private float timeTaken;
	private ConsoleInitializer CI;

	void Start () 
	{
		I = gameObject.GetComponentInParent<Individual>();
		newPosition = transform.position; // position de la cell
		CI = I.place.CI;
	}

	private void Update ()
	{
		if (transform.FindChild ("membrane").gameObject.GetComponent<SpriteRenderer> ().isVisible) 
		{
			//gamObject.GetComponent<RigidBody>().isKinematic = false;
		}

		if (Time.timeScale >= 1f) 
		{

			if (Input.GetMouseButton(0) && transform.FindChild("membrane").gameObject.GetComponent<SpriteRenderer>().isVisible) // test si clique sur cell (cell jouée)
			{
				if(!selectedByClick) // si pas deja selectionnée
				{
					Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position); // selection drag and drop
					camPos.y = CameraOperator.InvertMouseY(camPos.y);
					selected = CameraOperator.selection.Contains(camPos);
					
					if(selected && I.isPlayed)
					{
						I.isSelectioned = true;
					}
					else
					{
						I.isSelectioned = false;
					}
				}
			}
			else if(Input.GetMouseButtonDown(0))
			{
				foreach(Individual D in CI.cellsplayed)
				{
					selected = false;
					selectedByClick = false;
					D.isSelectioned = false;
				}
			}


			
			if(I.isSelectioned && I.isPlayed && Input.GetMouseButtonDown(1))		//Si sélectionné et clic droit
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					newPosition = hit.point;
					newPosition.z = 1;
					I.target = newPosition;
					I.gotDest = true;		//Objet possède une destination

				}
			}

			if(I.gotDest && (Time.timeScale >= 1f))
			{
				if((transform.position.x - I.target.x >= -2 && transform.position.x - I.target.x <= 2) && (transform.position.y - I.target.y >= -2 && transform.position.y - I.target.y <= 2))	//Gérer pour supprimer dest quand cells dans rayon autour de la target.
				{
					I.gotDest = false;		//Plus de destination car elle a été atteinte
				}
				transform.position = Vector3.Lerp(transform.position, I.target, 1/(I.duration*(Vector3.Distance(transform.position, I.target))));		//Déplacement de la cellule au fur et a mesure !
			}
			else
			{

			}
		}
	}
	
	private void OnMouseDown()
	{
		if (Time.timeScale >= 1f) 
		{
			I.isSelectioned = !I.isSelectioned;
			if (I.isSelectioned) 
			{
				//sdfsdfsdf
				selected = true;
				selectedByClick = true;
			}
			else
			{
				selected = false;
				I.isSelectioned = false;
			}
		}
	}
	
	private void OnMouseUp()
	{
		if (Time.timeScale >= 1f) 
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
}
