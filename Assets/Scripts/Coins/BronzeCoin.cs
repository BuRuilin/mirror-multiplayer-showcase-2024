using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Í­±Ò
/// </summary>
public class BronzeCoin : NetworkBehaviour
{

	/// <summary>
	/// Íæ¼ÒÅöµ½Í­±ÒÊ±Ö´ÐÐ
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (collision.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			{
				GlobalConfigs.config.GetBronzeCoin();
				DestroyCoin();
			}
		}
	}
	[Command(requiresAuthority = false)]
	private void DestroyCoin()
	{
		Destroy(gameObject);
	}
}
