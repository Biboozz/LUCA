using UnityEngine;
using System.Collections;

public class CharacterCustomization : Bolt.IProtocolToken 
{
	public string charac_name;

	public void Write(UdpKit.UdpPacket packet) 
	{
		packet.WriteString (charac_name);
	}
	
	public void Read(UdpKit.UdpPacket packet) 
	{
		charac_name = packet.ReadString();
	}

}
