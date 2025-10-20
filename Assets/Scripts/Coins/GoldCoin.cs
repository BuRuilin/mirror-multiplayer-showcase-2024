using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���
/// </summary>
public class GoldCoin : NetworkBehaviour
{
	/// <summary>
	/// ����������ʱִ��
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
