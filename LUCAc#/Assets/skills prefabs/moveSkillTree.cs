using UnityEngine;
using System.Collections;

public class moveSkillTree : MonoBehaviour {

	private bool isMoving = false;
	private Vector3 lastMousePos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isMoving) 
		{
			transform.position = new Vector3(transform.position.x + Input.mousePosition.x - lastMousePos.x, transform.position.y + Input.mousePosition.y - lastMousePos.y, 0);
		}
		if (Input.GetMouseButtonDown (1)) 
		{
			isMoving = false;
		}
		lastMousePos = Input.mousePosition;
	}

	public void click()
	{

		isMoving = true;
	}
}
