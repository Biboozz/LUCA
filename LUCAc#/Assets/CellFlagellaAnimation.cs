using UnityEngine;
using System.Collections;

public class CellFlagellaAnimation : MonoBehaviour {

	public Sprite[] sprites;
	public bool shown = true;
	public int period;

	private int update = 0;
	private int increment = 1;
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
				int i = n + increment;
				if (i == -1 || i == 6) 
				{
					increment *= -1;
					n += increment;
				} 
				else 
				{
					n = i;
				}
			}
		}
		else 
		{
			GetComponent<SpriteRenderer> ().sprite = null;
		}

	}
}
