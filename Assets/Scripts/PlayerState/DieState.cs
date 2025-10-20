using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ËÀÍö×´Ì¬
/// </summary>
public class DieState : PlayerState
{
	private PlayerStateMachine StateMachine;
	private GameObject gameObject;
	private PlayerController playerController;
	private Animator animator;
	private Rigidbody2D rigidbody2D;

	private WaitTime wait1 = new WaitTime(3);
	private WaitTime wait2 = new WaitTime(2);

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
		animator.SetBool("isDie", true);
		animator.CrossFade("Die", 0.1f);
		rigidbody2D.gravityScale = 0;
		rigidbody2D.velocity = Vector3.zero;
		wait1.Start();
	}
	public override void OnStayState()
	{
		wait1.FixedTick();
		wait2.FixedTick();
		if (wait1.isFinished)
		{
			wait1.Reset();
			wait2.Start();
			gameObject.transform.position = GlobalConfigs.config.reborn_Point;
		}
		if (wait2.isFinished)
		{
			wait2.Reset();
			animator.SetBool("isDie", false);
			animator.CrossFade("Idle", 0.1f);
			StateMachine.ChangeState("Idle");
		}
	}
	public override void OnExitState()
	{
		playerController.isDie = false;
		wait1.Reset();
		wait2.Reset();
	}
}