using UnityEngine;
using System.Collections;

public class NetworkCallBack : Bolt.GlobalEventListener 
{
	public override void SceneLoadLocalDone(string map) 
	{
		// randomize a position
		var pos = new Vector3(Random.Range(-16, 16), 0, Random.Range(-16, 16));
		
		// instantiate cube
		BoltNetwork.Instantiate(BoltPrefabs.cell_network, pos, Quaternion.identity);	
	}
}
