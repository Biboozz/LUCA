using UnityEngine;
using System.Collections;

public class hideGroupZone : MonoBehaviour {

	private bool shown = false;

	private bool animateShow = false;

	private bool animateHide = false;

	public environment E;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 p = Input.mousePosition;
		//Debug.Log (p);
		if (p.x > (float)Screen.width * 0.80f && p.y > 0.60f * (float)Screen.height) 
		{
			if (!shown) 
			{
				animateShow = true;
			}
		} 
		else
		{
			if (shown && (E.selectedButton == -1))
			{
				animateHide = true;
			}
		}

		Quaternion R = transform.rotation;
		if (animateShow) 
		{
			R.z = R.z - 0.2f;
			transform.rotation = R;
			if (R.z <= 0f) 
			{
				R.z = 0f;
				transform.rotation = R;
				animateShow = false;
				shown = true;
			}
		} 
		else 
		{
			if (animateHide) 
			{
				R.z = R.z + 0.2f;
				transform.rotation = R;
				if (R.z >= 1f) 
				{
					R.z = 1f;
					transform.rotation = R;
					animateHide = false;
					shown = false;
				}
			} 
		}
	}
}
