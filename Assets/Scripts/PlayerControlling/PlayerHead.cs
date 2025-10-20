using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ½ÇÉ«µÄÍ·
/// </summary>
public class PlayerHead : MonoBehaviour
{
	public PlayerController controller;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		controller.OnPlayerCollisionEnter2D(collision);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		controller.OnPlayerCollisionStay2D(collision);
	}
}
