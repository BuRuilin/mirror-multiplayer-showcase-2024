using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局参数
/// </summary>
public class GlobalConfigs : NetworkBehaviour
{
	public static GlobalConfigs config = null;

	public static bool idIsSet = false;
	public static int localId = 0;

	public static string[] scenes = 
	{ 
		"Assets/Scenes/Scene1.unity", 
		"Assets/Scenes/Scene2.unity", 
		"Assets/Scenes/Scene3.unity" 
	};

	public static Color[] colors =
	{
		Color.white,//白
        Color.cyan,//蓝
		new Color32(170, 255, 180, 255),//绿
        new Color32(255, 190, 200, 255),//粉
        new Color32(255, 255, 170, 255)//黄
    };

	[SyncVar]
	public Vector3 reborn_Point = new(-5, 0, 0);

	[SyncVar]
	public int priority = 0;

	[SyncVar]
	public int bronzeCoinCount = 0;

	[SyncVar]
	public int silverCoinCount = 0;

	[SyncVar]
	public int goldCoinsCount = 0;

	[Command(requiresAuthority = false)]
	public void SetRebornPoint(Vector3 point,int priority)
	{
		reborn_Point = point;
		this.priority = priority;
	}

	[Command(requiresAuthority = false)]
	public void GetBronzeCoin()
	{
		bronzeCoinCount++;
	}

	[Command(requiresAuthority = false)]
	public void GetSilverCoin()
	{
		silverCoinCount++;
	}

	[Command(requiresAuthority = false)]
	public void GetGoldCoin()
	{
		goldCoinsCount++;
	}
}