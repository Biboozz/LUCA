using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class RessourcePoint : Ressource {

    private POINT center;
    private int amount;

    public RessourcePoint(int id, string name, RessourceCircle C) : base(id, name)
    {
        System.Random rand = new System.Random();
        center.x = rand.Next(C.get_center().x - C.get_radius(), C.get_center().x + C.get_radius());
        center.y = rand.Next(C.get_center().y - C.get_radius(), C.get_center().y + C.get_radius());
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
