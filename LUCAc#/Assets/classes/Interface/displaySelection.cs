using UnityEngine;
using System.Collections;

public class displaySelection : MonoBehaviour {

	public Canvas canvas;
	public GameObject selection;
	private Vector3 C1;
	private Vector3 C2;
	private Vector3 focused;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (C1.y >= C2.y && C2.x >= C1.x) {
			C2 = Input.mousePosition;
			selection.transform.position = new Vector3((C1.x + C2.x)/2,(C1.y + C2.y)/2, 0);
			((RectTransform)selection.transform).sizeDelta = new Vector2 (C2.x - C1.x, C1.y - C2.y);
		}

		if (C1.y >= C2.y && C1.x >= C2.x) {
			C2 = Input.mousePosition;
			selection.transform.position = new Vector3((C2.x + C1.x)/2,(C1.y + C2.y)/2, 0);
			((RectTransform)selection.transform).sizeDelta = new Vector2 (C1.x - C2.x, C1.y - C2.y);
		}

		if (C2.y >= C1.y && C2.x >= C1.x) {
			C2 = Input.mousePosition;
			selection.transform.position = new Vector3((C1.x + C2.x)/2,(C2.y + C1.y)/2, 0);
			((RectTransform)selection.transform).sizeDelta = new Vector2 (C2.x - C1.x, C2.y - C1.y);
		}

		if (C2.y >= C1.y && C1.x >= C2.x) {
			C2 = Input.mousePosition;
			selection.transform.position = new Vector3((C2.x + C1.x)/2,(C2.y + C1.y)/2, 0);
			((RectTransform)selection.transform).sizeDelta = new Vector2 (C1.x - C2.x, C2.y - C1.y);
		}
	}

	void OnMouseDown()
	{
		Debug.Log ("down");
		C1 = Input.mousePosition;
		C2 = Input.mousePosition;
		focused = C1;
		selection.SetActive (true);
	}

	void OnMouseUp()
	{
		Debug.Log ("up");
		selection.SetActive (false);
	}
}
