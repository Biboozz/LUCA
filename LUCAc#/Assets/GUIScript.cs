using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour 
{
	public GUISkin skin;
	public string text = "this is a label";

	void OnGUI()
	{
		GUI.skin = this.skin;
		GUILayout.BeginArea(new Rect(50,50,400,Screen.width /2f));
		GUILayout.BeginHorizontal ();
		GUILayout.Label (text);
		GUILayout.Button (text);
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
}
