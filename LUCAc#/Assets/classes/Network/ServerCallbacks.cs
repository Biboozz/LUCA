using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Host)]
public class ServerCallbacks : Bolt.GlobalEventListener
{
	public override void Connected(BoltConnection connection) 
	{
		//var customization = (CharacterCustomization)entity.name

		var log = LogEvent.Create();
		log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
		Debug.Log (connection.RemoteEndPoint);
		log.Send();
	}
	
	public override void Disconnected(BoltConnection connection) 
	{
		BoltLauncher.Shutdown ();
		var log = LogEvent.Create();
		log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
		log.Send();
	}
}
