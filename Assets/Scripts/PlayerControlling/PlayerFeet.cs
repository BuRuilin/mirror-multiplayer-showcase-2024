using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ½ÇÉ«µÄ½Å
/// </summary>
public class PlayerFeet : MonoBehaviour
{
	public PlayerController controller;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(controller != null) controller.OnFeetTriggerEnter2D(collision);
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (controller != null) controller.OnFeetTriggerStay2D(collision);
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (controller != null) controller.OnFeetTriggerExit2D(collision);
	}
}
