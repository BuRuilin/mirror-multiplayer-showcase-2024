using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ÌøÔ¾×´Ì¬
/// </summary>
public class JumpState : PlayerState
{
	PlayerStateMachine StateMachine;
	GameObject gameObject;
	PlayerController playerController;
	Animator animator;
	Rigidbody2D rigidbody2D;

	private WaitTime wait = new WaitTime(0.1f);
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
		animator.SetBool("isJump", true);
		animator.CrossFade("Jump", 0.1f);
		rigidbody2D.gravityScale = playerController.originGravity;
		rigidbody2D.AddForce(Vector2.up * playerController.JumpForce, ForceMode2D.Impulse);
		wait.Start();
	}
	public override void OnStayState()
	{
		wait.FixedTick();
		if (wait.isFinished)
		{
			if (playerController.direction != 0)
			{
				if (playerController.direction > 0)
				{
					gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
				}
				else
				{
					gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
				}
				gameObject.transform.Translate(Vector3.right * playerController.speed * Time.fixedDeltaTime);
			}
			if (playerController.isDie)
			{
				StateMachine.ChangeState("Die");
			}
			else if (playerController.isLand)
			{
				StateMachine.ChangeState("Land");
			}
		}
	}
	public override void OnExitState()
	{
		wait.Reset();
		animator.SetBool("isJump", false);
	}
}