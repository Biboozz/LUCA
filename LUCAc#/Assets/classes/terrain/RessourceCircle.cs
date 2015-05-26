using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class RessourceCircle
{
    private molecule Molecule;
	private POINT Center;
	private List<RessourceNode> Nodes = new List<RessourceNode>();
	private int Radius;
	private GameObject _circleObject;

	public GameObject circleObject
	{
		get
		{
			return _circleObject;
		}
		set
		{
			_circleObject = value;
			_circleObject.transform.position = new Vector3(Center.x, UnityEngine.Random.Range(0f,3f), Center.y);
			_circleObject.transform.localScale = new Vector3(Radius * 2,Radius * 2,1);
			_circleObject.GetComponent<MeshRenderer>().material.color = new Color(1/(float)Molecule.ID, 0 , 1/(float)Molecule.ID , 1);
		}
	}

    public RessourceCircle(POINT center, int radius, molecule molecule, Terrain T)
    {
        Center = center;
        Radius = radius;
        Molecule = molecule;


        POINT p = new POINT();

        int boucle = UnityEngine.Random.Range(200,301);

        for (int i = 0; i < boucle; i++)
        {
            do
            {
                p.x = UnityEngine.Random.Range(Center.x - Radius, Center.x + Radius);
                p.y = UnityEngine.Random.Range(Center.y - Radius, Center.y + Radius);

            } while ((p.distance(Center) > Radius) & (p.x < T.get_Width()) & (p.y < T.get_Heigth()) & (p.x >= 0) & (p.y >= 0));

            Nodes.Add(new RessourceNode(p,molecule));
        }
    }
	
	public molecule Get_mol()
	{
		return Molecule;
	}
	
	public int Get_radius()
	{
		return Radius;
	}
	
	public POINT Get_Center()
	{
		return Center;
	}

}
