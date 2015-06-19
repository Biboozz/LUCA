using UnityEngine;
using System.Collections;

public class CellFlagellaAnimation : MonoBehaviour {

	public Sprite[] sprites;
	public bool shown = true;
	public int period;

	private int update = 0;
	private int n = 0;

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
				update = 0;
				GetComponent<SpriteRenderer> ().sprite = sprites [n];
				n = (n + 1) % 8;
			}
		}
		else 
		{
			GetComponent<SpriteRenderer> ().sprite = null;
		}

	}
}
