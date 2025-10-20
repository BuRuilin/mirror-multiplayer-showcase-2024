using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Εά²½Χ΄Μ¬
/// </summary>
public class RunState : PlayerState
{
	private PlayerStateMachine StateMachine;
	private GameObject gameObject;
	private PlayerController playerController;
	
	private Animator animator;
	private Rigidbody2D rigidbody2D;

	public override void Init(PlayerStateMachine stateMachine)
	{
		StateMachine = stateMachine;
		gameObject = stateMachine.gameObject;
		playerController = gameObject.GetComponent<PlayerController>();
		animator = gameObject.GetComponent<Animator>();
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}
	public override void OnEnterState(string lastState)
	{
		if (lastState != "Run")
		{
			animator.SetBool("isRun", true);
			animator.CrossFade("Run", 0.1f);
		}
	}
	public override void OnStayState()
	{
		if (playerController.isLand)
		{
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = Vector3.zero;
		}
		else
		{
			rigidbody2D.gravityScale = playerController.originGravity;
		}

		if (playerController.isDie)
		{
			StateMachine.ChangeState("Die");
		}
		if (playerController.isJump)
		{
			StateMachine.ChangeState("Jump");
		}
		if (playerController.direction == 0)
		{
			StateMachine.ChangeState("Idle");
		}
		else
		{
			if (playerController.direction > 0)
			{
				gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
				playerController.playerName.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
			}
			else
			{
				gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
				playerController.playerName.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
			}
			gameObject.transform.Translate(Vector3.right * playerController.speed * Time.fixedDeltaTime);
		}
	}
	public override void OnExitState()
	{
		animator.SetBool("isRun", false);
	}
}