using UnityEngine;
using System.Collections;

public class CellBehaviour : Bolt.EntityBehaviour<ICellState> 
{
	public GameObject[] Cell_element_objects;

	private Color _color;
	private bool gotDest = false;
	private Vector3 target;
	private float duration = 5f;
	private int percent = 1;

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
		GameObject.Find("Main Camera").transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
		
		var moveSpeed = 40f * percent;
		
		var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		targetPos.z = transform.position.z;
		transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
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
