using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class RessourceCircle : MonoBehaviour {

    private POINT center;
    private int radius;
    private List<RessourcePoint> nodes = new List<RessourcePoint>();
    private int totalAmount;

    public int get_radius()
    {
        return radius;
    }

    public POINT get_center()
    {
        return center;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
