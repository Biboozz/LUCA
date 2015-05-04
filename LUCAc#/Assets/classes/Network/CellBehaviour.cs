using UnityEngine;
using System.Collections;

public class CellBehaviour : Bolt.EntityBehaviour<ICellState> 
{
	public override void Attached() 
	{
		state.CellTransform.SetTransforms (transform);

		if (entity.isOwner) 
		{
			Color color = new Color(Random.value, Random.value, Random.value);
			transform.FindChild("cytoplasm").gameObject.GetComponent<MeshRenderer>().material.color = color;
		}
		
		state.AddCallback("CellColor", ColorChanged);
	}

	void ColorChanged() 
	{
		GetComponent<Renderer> ().material.color = state.CellColor;
	}

	public override void SimulateOwner() 
	{
		var speed = 100f;
		var movement = Vector3.zero;
		
		if (Input.GetKey(KeyCode.DownArrow)) { movement.z += 1; }
		if (Input.GetKey(KeyCode.UpArrow)) { movement.z -= 1; }
		if (Input.GetKey(KeyCode.RightArrow)) { movement.x -= 1; }
		if (Input.GetKey(KeyCode.LeftArrow)) { movement.x += 1; }
		
		if (movement != Vector3.zero) 
		{
			transform.position = transform.position + (movement.normalized * speed * BoltNetwork.frameDeltaTime);
		}
	}

	void OnGUI() 
	{
		if (entity.isOwner) 
		{
			GUI.color = state.CellColor;
			GUILayout.Label("@@@");
			GUI.color = Color.white;
		}
	}
}
