using UnityEngine;
using System.Collections;

public class CellBehaviour : Bolt.EntityBehaviour<ICellState> 
{
	public GameObject[] Cell_element_objects;

	private Color _color;
	private bool gotDest = false;
	private Vector3 target;
	private float duration = 5f;

	public override void Attached() 
	{
		state.CellTransform.SetTransforms (transform);

		if (entity.isOwner) 
		{
			Color color = new Color(Random.value, Random.value, Random.value);

			transform.FindChild("cytoplasm").gameObject.GetComponent<MeshRenderer>().material.color = color;

			state.Cell_elements[1].Cell_element_Color = color;

			_color = color;
		}
		
		state.AddCallback("CellColor", ColorChanged);
	}

	void ColorChanged() 
	{
		GetComponent<Renderer> ().material.color = _color;
	}

	public override void SimulateOwner() 
	{
		var speed = 100f;
		var movement = Vector3.zero;
		
		if (Input.GetKey(KeyCode.DownArrow) && transform.position.z < 247) { movement.z += 1; }
		if (Input.GetKey(KeyCode.UpArrow) && transform.position.z > 3) { movement.z -= 1; }
		if (Input.GetKey(KeyCode.RightArrow) && transform.position.x > 3) { movement.x -= 1; }
		if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x < 247) { movement.x += 1; }
		
		if (movement != Vector3.zero) 
		{
			transform.position = transform.position + (movement.normalized * speed * BoltNetwork.frameDeltaTime);
		}
	
		if(Input.GetMouseButtonDown(1))		//Si clic droit
		{
			target = transform.FindChild("target").gameObject.transform.position;	//Définition du Vector3 target

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
			Vector3 point = ray.origin + (ray.direction * 4.5f);    
			point.y = 0.2f;

			if(point.x <= 249 && point.x >= 1 && point.z <= 249 && point.z >= 1)
			{
				target = point;		//target prend position du clic
				gotDest = true;		//Objet possède une destination
			}
		}

		if(gotDest)
		{
			if((transform.position.x - target.x >= -2 && transform.position.x - target.x <= 2) && (transform.position.z - target.z >= -2 && transform.position.z - target.z <= 2))	//Gérer pour supprimer dest quand cells dans rayon autour de la target.
			{
				gotDest = false;		//Plus de destination car elle a été atteinte
			}
			if(transform.position.x < 249 && transform.position.x > 1 && transform.position.z < 249 && transform.position.z > 1)
			{
				transform.position = Vector3.Lerp(transform.position, target, 1/(duration*(Vector3.Distance(transform.position, target))));		//Déplacement de la cellule au fur et a mesure !
			}
		}

	}
	
	void OnGUI() 
	{
		if (entity.isOwner) 
		{
			GUI.color = _color;
			GUILayout.Label("Joueur");
			GUI.color = Color.white;
		}
	}
}
