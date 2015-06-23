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
	public GameObject WorldButtonOff;

	public void TogglePannelOn()
	{
		Pannel.SetActive(true);
		WorldButtonOn.SetActive (false);
		WorldButtonOff.SetActive (true);

	}

	public void TogglePannelOff()
	{
		Pannel.SetActive (false);
		WorldButtonOff.SetActive (false);
		WorldButtonOn.SetActive (true);
	}

}
