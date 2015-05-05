using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallBack : Bolt.GlobalEventListener 
{
	public override void SceneLoadLocalDone(string map) 
	{
		// randomize a position
		var pos = new Vector3(Random.Range(0, 250), 0, Random.Range(0, 250));
		
		// instantiate cube
		BoltNetwork.Instantiate(BoltPrefabs.cell_network, pos, Quaternion.Euler(90,0,0));
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
		
		for (int i = 0; i < maxMessages; ++i) {
			GUILayout.Label(logMessages[i]);
		}
		
		GUILayout.EndArea();
	}
}
