﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraOperator : MonoBehaviour {

	public Texture2D selectionHighlight = null;
	public static Rect selection = new Rect(0, 0, 0, 0);
	private Vector3 startclick = -Vector3.one;

	private static Vector3 moveToDestination = Vector3.zero;
	private static List<string> passables = new List<string>() { "Terrain" };

	private void Update () 
	{
		CheckCamera ();
		Cleanup ();
	}

	private void CheckCamera()
	{
		if(Input.GetMouseButtonDown(0))
		{
			startclick = Input.mousePosition;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			startclick = -Vector3.one;
		}
		if(Input.GetMouseButton(0))
		{
			selection = new Rect(startclick.x, InvertMouseY(startclick.y), Input.mousePosition.x - startclick.x, InvertMouseY(Input.mousePosition.y) - InvertMouseY(startclick.y));
			if(selection.width < 0)
			{
				selection.x += selection.width;
				selection.width = -selection.width;
			}
			if(selection.height < 0)
			{
				selection.y += selection.height;
				selection.height = -selection.height;
			}
		}
	}

	public static float InvertMouseY (float y)
	{
		return Screen.height - y;
	}

	private void Cleanup()
	{
		if (!Input.GetMouseButtonUp(1)) 
		{
			moveToDestination = Vector3.zero;
		}
	}

	public static Vector3 GetDestination()
	{
		if (moveToDestination == Vector3.zero) 
		{
			RaycastHit hit;
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(r, out hit))
			{
				while(!passables.Contains(hit.transform.gameObject.name))
				{
					if(!Physics.Raycast(hit.point + r.direction * 0.1f, r.direction, out hit))
					{
						break;
					}
				}
			}
			if(hit.transform != null)
				moveToDestination = hit.point;
		}

		return moveToDestination;
	}
}
