using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 金币
/// </summary>
public class GoldCoin : NetworkBehaviour
{
	/// <summary>
	/// 玩家碰到金币时执行
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (collision.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			{
				GlobalConfigs.config.GetGoldCoin();
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
