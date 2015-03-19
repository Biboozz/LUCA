﻿using UnityEngine;
using System.Collections;

public class CameraOperator : MonoBehaviour {

	public Texture2D selectionHighlight = null;
	public static Rect selection = new Rect(0, 0, 0, 0);
	private Vector3 startclick = -Vector3.one;

	private void Update () 
	{
		CheckCamera ();
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

	private void OnGui()	//Problème ici de l'affichage de la box de sélection
	{
		if (startclick != -Vector3.one) 
		{
			GUI.DragWindow(selection);
		}
	}

	public static float InvertMouseY (float y)
	{
		return Screen.height - y;
	}
}
