using UnityEngine;
using System.Collections;

public class target : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))    //if right click
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
			Vector3 click_position  = ray.origin + (ray.direction * 4.5f);    //Coords of Right Click
			Vector3 tempPos = transform.position;
			tempPos.x = click_position.x;
			tempPos.z = click_position.z;
			Debug.Log(click_position);
			transform.position = tempPos;
		}
	}
}
