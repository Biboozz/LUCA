using UnityEngine;
using System.Collections;

public class CellBehaviour : Bolt.EntityBehaviour<ICellState> 
{
	public GameObject[] Cell_element_objects;

	private Color _color;
	private int percent = 1;

	public override void Attached() 
	{
		state.CellTransform.SetTransforms (transform);

		if (entity.isOwner) 
		{
			Color color = new Color(Random.value, Random.value, Random.value);

			//transform.FindChild("cytoplasm").gameObject.GetComponent<MeshRenderer>().material.color = color;

			//state.Cell_elements[1].Cell_element_Color = color;

			state.CellColor = color;
			_color = color;
		}
		
		state.AddCallback("CellColor", ColorChanged);
	}

	void ColorChanged() 
	{
		GetComponent<Renderer> ().material.color = state.CellColor;
	}

	public override void SimulateOwner() 
	{
		GameObject.Find("Main Camera").transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
		
		var moveSpeed = 40f * percent;
		
		var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		targetPos.z = transform.position.z;

		if (transform.position.x < 1699 && transform.position.x > 301 && transform.position.y < 1699 && transform.position.y > 301)
			transform.position = Vector3.MoveTowards (transform.position, targetPos, moveSpeed * Time.deltaTime);
		else if (transform.position.x >= 1699)
			transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y);
		else if (transform.position.x <= 301)
			transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y);
		else if (transform.position.y >= 1699)
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f);
		else if (transform.position.y <= 301)
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f);
	}

	//detruire une molecule quand on la touche 
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("molecule")) 
		{
			BoltNetwork.Destroy(other.gameObject);
		}
	}
	
	void OnGUI() 
	{
		if (entity.isOwner) 
		{
			string pseudo = pseudo_multi.pseudos;

			GUI.color = _color;
			GUILayout.Label(pseudo);
			GUI.color = Color.white;
		}
	}
}
