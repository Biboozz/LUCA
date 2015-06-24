using UnityEngine;
using System.Collections;

public class molBehaviour : Bolt.EntityBehaviour<Isphere_mol> 
{
	public override void Attached() 
	{
		state.sphere_molTransform.SetTransforms (transform);
		
		if (entity.isOwner) 
		{
			Color color = new Color(Random.value, Random.value, Random.value);

			state.sphere_molColor = color;
		}
		
		state.AddCallback("sphere_molColor", ColorChanged);
	}
	
	void ColorChanged() 
	{
		GetComponent<Renderer> ().material.color = state.sphere_molColor;
	}

}
