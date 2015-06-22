using UnityEngine;
using System.Collections;

public class CollisionOptimizer : MonoBehaviour {

	public GameObject parent;

	// Use this for initialization
	void Start () {
		if (GetComponent<SpriteRenderer> ().isVisible) 
		{
			addCollisions();
		}
	}	
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBecameInvisible() 
	{
		removeCollisions ();
	}

	void OnBecameVisible()
	{

		addCollisions ();
	}

	public void addCollisions()
	{
		Rigidbody2D rb2D = parent.GetComponent<Rigidbody2D> ();
		if ( rb2D== null) 
		{
			parent.AddComponent<Rigidbody2D> ().gravityScale = 0;
			parent.AddComponent<CircleCollider2D> ();
		}
	}

	public void removeCollisions()
	{
		Destroy (parent.GetComponent<Rigidbody2D> ());
		Destroy (parent.GetComponent<CircleCollider2D> ());
	}
}
