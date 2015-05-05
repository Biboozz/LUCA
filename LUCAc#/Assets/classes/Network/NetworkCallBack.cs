using UnityEngine;
using System.Collections;

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
}
