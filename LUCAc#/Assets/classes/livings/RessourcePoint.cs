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
        Random rand = new Random();
        center.x = rand.Next(C.get_center().x - C.get_radius(), C.get_center().x + C.getradius);
        center.y = rand.Next(C.get_center().y - C.get_radius(), C.get_center().y + C.getradius);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
