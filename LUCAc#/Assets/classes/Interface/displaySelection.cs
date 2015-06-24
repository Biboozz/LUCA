using UnityEngine;
using System.Collections;

public class displaySelection : MonoBehaviour {

	public Canvas canvas;
	public GameObject selection;
	private Vector3 C1;
	private Vector3 C2;

	public GameObject miniWin1;
	public GameObject miniWin2;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frameS
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			bool b = true;
			Vector2 pos = Input.mousePosition;
			float RX = pos.x / Screen.width;
			float RY = pos.y / Screen.height;

			if (miniWin2.activeSelf)
			{
				if(RX < 0.19 && RY > 0.52)
				{
					b = false;
				}
			}

			if (miniWin1.activeSelf)
			{
				if (RX < 0.22 && RY < 0.47)
				{
					b = false;
				}
			}

			if (RX > 0.82 && RY > 0.58)
			{
				b = false;
			}


			if (b) 
			{
				if (Time.timeScale >= 1f) {
					C1 = Input.mousePosition;
					C2 = Input.mousePosition;
					selection.SetActive (true);
				}
			}
		} 
		if (Input.GetMouseButtonUp(0))
		{
			if (Time.timeScale >= 1f) 
			{
				selection.SetActive (false);
			}
		}

		if (Time.timeScale >= 1f) 
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
	}

	void OnMouseDown()
	{
		if (Time.timeScale >= 1f) 
		{
			C1 = Input.mousePosition;
			C2 = Input.mousePosition;
			selection.SetActive (true);
		}
	}

	void OnMouseUp()
	{
		if (Time.timeScale >= 1f) {
			selection.SetActive (false);
		}
	}

}
