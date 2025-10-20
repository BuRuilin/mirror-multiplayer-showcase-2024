using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 银币
/// </summary>
public class SilverCoin : NetworkBehaviour
{
	/// <summary>
	/// 玩家碰到银币时执行
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (collision.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			{
				GlobalConfigs.config.GetSilverCoin();
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
