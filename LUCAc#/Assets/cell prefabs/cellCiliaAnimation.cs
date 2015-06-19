using UnityEngine;
using System.Collections;

public class cellCiliaAnimation : MonoBehaviour {

	public Sprite[] sprites;
	public bool shown;
	public int period;

	private int update = 0;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (shown) 
		{
			update++;
			if (update >= period) 
			{
				GetComponent<SpriteRenderer> ().sprite = sprites [Random.Range(0,12)];
				update = 0;
			}
		} 
		else 
		{
			GetComponent<SpriteRenderer> ().sprite = null;
		}
	}
}
