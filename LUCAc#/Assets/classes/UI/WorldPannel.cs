using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class WorldPannel : MonoBehaviour {

	public GameObject Pannel;
	public GameObject WorldButtonOn;
	private bool isActive = false;

	public void TogglePannelOn()
	{
		if (isActive) 
		{
			Pannel.SetActive (false);
			isActive = false;
		} 
		else 
		{
			Pannel.SetActive (true);
			isActive = true;
		}	
	}

}
