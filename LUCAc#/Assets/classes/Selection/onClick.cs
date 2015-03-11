using UnityEngine;
using System.Collections;

public class onClick : MonoBehaviour {

	private Individual I;
	private Color baseColor;

	// Use this for initialization
	void Start () 
	{
		I = gameObject.GetComponentInParent<Individual>();
		baseColor = I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		if (I.isPlayed) 
		{
			I.isSelectioned = !I.isSelectioned;
			Debug.Log("is selectioned " + I.isSelectioned);
			if (I.isSelectioned) 
			{
				I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = new Color (1,1,0,1);
				I.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color (1,1,0,1);

			}
			else
			{
				I.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = baseColor;
				I.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = baseColor;

			}
		}
	}
}
