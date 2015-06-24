using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallBack : Bolt.GlobalEventListener 
{
	public System.Random rand = new System.Random ();

	public override void SceneLoadLocalDone(string map) 
	{
		// randomize a position
		var pos = new Vector3(rand.Next(301, 1699) , rand.Next(301, 1699), 0);
		
		// instantiate cell
		BoltNetwork.Instantiate(/*BoltPrefabs.cell_network*/ BoltPrefabs.Sphere, pos, Quaternion.Euler(0,0,0));

		// instantiate molecule
		for (int i = 0; i < 200; i++) 
		{
			// random position 
			var mol_pos = new Vector3(rand.Next(301, 1699) , rand.Next(301, 1699), 0);

			BoltNetwork.Instantiate(BoltPrefabs.sphere_mol, mol_pos, Quaternion.Euler(0,0,0));
		}
	}


	List<string> logMessages = new List<string>();

	public override void OnEvent(LogEvent evnt) 
	{
		logMessages.Insert(0, evnt.Message);
	}

	void OnGUI() 
	{
		// only display max the 5 latest log messages
		int maxMessages = Mathf.Min(5, logMessages.Count);
		
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
		
		for (int i = 0; i < maxMessages; ++i) 
		{
			GUILayout.Label(logMessages[i]);
		}
		
		GUILayout.EndArea();
	}
}
