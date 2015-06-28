using UnityEngine;
using System.Collections;

public class listboxDisplayer : MonoBehaviour {

	private ListBox molecules;
	//private int _lastMoleculeSelected;

	// Use this for initialization
	void Start () 
	{
		Transform t = transform;
		Rect listBoxRect = ((RectTransform)t).rect;
		if (gameObject.name == "taskListBox") 
		{
			listBoxRect.position = new Vector2 (Screen.width * 0.77f, Screen.height * 0.43f);
		} 
		else 
		{
			listBoxRect.position = new Vector2 (Screen.width * 0.63f, Screen.height * 0.74f);
		}

		molecules = new ListBox (listBoxRect, new Rect (0, 0, listBoxRect.width - 20, 150), false, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		drawListBox ();
		GUI.skin.button.fontSize = 11;
		GUI.skin.label.fontSize = 11;
		GUI.skin.button.alignment = TextAnchor.LowerLeft;
	}

	void drawListBox()
	{
		//Click Test
		if (molecules.ReDraw())
		{
			//_lastMoleculeSelected = molecules.GetSelectedID();
		}
		//----------
	}

	public void toggleShow()
	{
		gameObject.SetActive (!gameObject.activeSelf);
	}
}
