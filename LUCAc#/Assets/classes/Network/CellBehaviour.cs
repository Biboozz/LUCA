using UnityEngine;
using System.Collections;

public class CellBehaviour : Bolt.EntityBehaviour<ICellState> 
{
	public GameObject[] Cell_element_objects;

	private Color _color;
	private float moveSpeed = 40f;
	private float previousSpeed;
	private bool isPressed = false;
	public System.Random rand = new System.Random ();

	public static string _name;

	public override void Attached() 
	{
		state.CellTransform.SetTransforms (transform);

		if (entity.isOwner) 
		{
			Color color = new Color(Random.value, Random.value, Random.value);
			state.CellColor = color;
			_color = color;

		}
		
		state.AddCallback("CellColor", ColorChanged);
	}

	void ColorChanged() //changement couleur
	{
		GetComponent<Renderer> ().material.color = state.CellColor;
	}

	public override void SimulateOwner() 
	{
		GameObject.Find("Main Camera").transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
		
		Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		targetPos.z = transform.position.z;

		if (transform.position.x < 1699 && transform.position.x > 301 && transform.position.y < 1699 && transform.position.y > 301)
			transform.position = Vector3.MoveTowards (transform.position, targetPos, moveSpeed * Time.deltaTime);
		else if (transform.position.x >= 1699)
			transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y);
		else if (transform.position.x <= 301)
			transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y);
		else if (transform.position.y >= 1699)
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f);
		else if (transform.position.y <= 301)
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f);

		if (Input.GetKeyDown (KeyCode.B) && !isPressed) 
		{
			previousSpeed = moveSpeed;
			moveSpeed = 0f;
			isPressed = true;
		} 
		else if (Input.GetKeyDown (KeyCode.B) && isPressed) 
		{
			isPressed = false;
			moveSpeed = previousSpeed;
		}
	}

	//detruire une molecule quand on la touche 
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("molecule")) 
		{
			BoltNetwork.Destroy(other.gameObject);

//			GetComponent<Quaternion> ().x = transform.localScale + Vector3(0.5f, 0, 0);
//			GetComponent<Quaternion> ().y = transform.localScale + Vector3(0, 0.5f, 0);
			state.CellVector = transform.localScale;
			transform.localScale += new Vector3(0.5f, 0.5f, 0);	//Grossis la sphère
			state.CellVector += new Vector3(0.5f, 0.5f, 0); //Grossir la sphère


			state.AddCallback("CellVector", VectorChanged);


			moveSpeed = ((1 / transform.localScale.x) * moveSpeed * (/*Changer ce coeff*/0.995f * transform.localScale.x));	//Vitesse réduite //Formule Excel : =(1/A3)*B2*(0,995*A3) // A3 : transform.localScale.x & B2 : moveSpeed

			InstanceMolecule();	//Des que une bouffer, une autre apparait
		}
//		if (other.gameObject.CompareTag ("Sphere") && (other.transform.localScale.x - transform.localScale.x > 1))
//		{
//			transform.localScale += new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0);	//Grossis la sphère
//			moveSpeed = ((1 / transform.localScale.x) * moveSpeed * (/*Changer ce coeff*/0.995f * transform.localScale.x));	//Vitesse réduite //Formule Excel : =(1/A3)*B2*(0,995*A3) // A3 : transform.localScale.x & B2 : moveSpeed
//			BoltNetwork.Destroy(gameObject);
//		}
	}

	void VectorChanged()
	{
		transform.localScale = state.CellVector;
		//GetComponent<Quaternion> ().x = transform.localScale + 0.5f;
		//GetComponent<Quaternion> ().y = transform.localScale + 0.5f;
	}
	
	void OnGUI() 
	{
		if (entity.isOwner) 
		{
			string pseudo = pseudo_multi.pseudos;

			GUI.color = _color;
			GUILayout.Label(pseudo);
			GUI.color = Color.white;
		}
	}

	public void InstanceMolecule()
	{
		var mol_pos = new Vector3(rand.Next(301, 1699) , rand.Next(301, 1699), -0.5f);
		BoltNetwork.Instantiate(BoltPrefabs.sphere_mol, mol_pos, Quaternion.Euler(0,0,0));
	}
}
