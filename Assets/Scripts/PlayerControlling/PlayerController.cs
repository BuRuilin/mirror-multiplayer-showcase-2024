using System.Collections;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 角色控制器，用于获取键盘输入，启动状态机
/// </summary>
public class PlayerController : NetworkBehaviour
{
    private PlayerStateMachine stateMachine;

    public Text playerName;

	public float speed = 2;
    public float JumpForce = 6;

    public float direction { get; private set; } = 0;


    public float originGravity;
    public bool isLand = false;
    public bool isJump = false;
    public bool isDie = false;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;



    [SyncVar]
    private int Id = GlobalConfigs.localId;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		originGravity = rb.gravityScale;

        if (isLocalPlayer)
        {
            CamFollow.localPlayer = gameObject;
        }

        //如果是本地角色才启动状态机，用于控制角色
        if (isLocalPlayer)
        {
            stateMachine = GetComponent<PlayerStateMachine>();
            stateMachine.AddState("Idle", new IdleState());
            stateMachine.AddState("Run", new RunState());
            stateMachine.AddState("Jump", new JumpState());
            stateMachine.AddState("Land", new LandState());
            stateMachine.AddState("Die", new DieState());
            stateMachine.Init("Idle");
        }
    }

    //用于获取全局数据
    void GetGlobalConfig()
    {
        if (GlobalConfigs.config == null)
        {
            GameObject[] g = GameObject.FindGameObjectsWithTag("GlobalConfigs");
            if (g.Length > 0)
            {
                GlobalConfigs.config= g[0].GetComponent<GlobalConfigs>();
				Debug.Log("Got Config");
			}
        }
	}

    void Update()
    {
        if (isLocalPlayer)
        {
            Id = GlobalConfigs.localId;
		}
        spriteRenderer.color = GlobalConfigs.colors[Id % GlobalConfigs.colors.Length];
        if (Id == 0)
        {
            playerName.color = Color.black;
		}
        else
        {
            playerName.color = GlobalConfigs.colors[Id % GlobalConfigs.colors.Length];
		}
        playerName.text = $"P{Id + 1}";
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (GlobalConfigs.config == null)
            {
                GetGlobalConfig();//获取全局数据
            }
		}
		playerName.rectTransform.eulerAngles = Vector3.zero;
    }

    /// <summary>
    /// InputSystem OnMove
    /// </summary>
    /// <param name="value"></param>
    public void OnMove(InputValue value)
    {
        if (isLocalPlayer)
        {
			if (!GameManager.isPause && !GameManager.isMsgFocused)
			{
				direction = value.Get<float>();
			}
            else
            {
				direction = 0;
			}
        }
    }

    /// <summary>
    /// InputSystem OnJump
    /// </summary>
    public void OnJump()
    {
        if (isLocalPlayer)
        {
            if (!GameManager.isPause && !GameManager.isMsgFocused)
            {
                if (!isDie)
                {
                    if (!isJump)
                    {
                        isJump = true;
                    }
                }
            }
        }
    }


	private void OnCollisionEnter2D(Collision2D collision)
	{
        OnPlayerCollisionEnter2D(collision);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
        OnPlayerCollisionStay2D(collision);
	}

	public void OnPlayerCollisionEnter2D(Collision2D collision)
    {
        if (isLocalPlayer)
        {
            if (collision.collider.tag == "Spike")
            {
                if (!isDie)
                {
                    isDie = true;
                }
            }
        }
    }

    public void OnPlayerCollisionStay2D(Collision2D collision)
    {
		if (isLocalPlayer)
		{
			if (collision.collider.tag == "Spike")
			{
				if (!isDie)
				{
					isDie = true;
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (isLocalPlayer)
        {
            if (collision.tag == "RebornPoint")
            {
                RebornPoint rp = collision.gameObject.GetComponent<RebornPoint>();

                if (rp.priority >= GlobalConfigs.config.priority)
                    GlobalConfigs.config.SetRebornPoint(rp.gameObject.transform.position, rp.priority);
            }
        }
	}

	public void OnFeetTriggerEnter2D(Collider2D collision)
    {
		if (isLocalPlayer)
		{
            if (collision.tag != "Coin")
            {
                isLand = true;
            }
            else if (collision.tag == "Spike")
            {
                isDie = true;
            }
		}
	}

	public void OnFeetTriggerStay2D(Collider2D collision)
	{
		if (isLocalPlayer)
		{
			if (collision.tag != "Coin")
			{
				isLand = true;
			}
			else if(collision.tag == "Spike")
			{
				isDie = true;
			}
		}
	}

	public void OnFeetTriggerExit2D(Collider2D collision)
	{
		if (isLocalPlayer)
		{
			if (collision.tag != "Coin")
			{
				isLand = false;
			}
		}
	}
}
