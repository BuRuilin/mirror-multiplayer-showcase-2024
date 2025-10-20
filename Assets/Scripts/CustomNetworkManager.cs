using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
	private GameObject globalConfigs;
	private int idCount = 0;

	/// <summary>
	/// 玩家加入时执行（仅限服务器）
	/// </summary>
	/// <param name="conn"></param>
	public override void OnServerAddPlayer(NetworkConnectionToClient conn)
	{
		base.OnServerAddPlayer(conn);
		NetworkServer.SendToAll<IdMessage>(new(idCount));
		idCount++;
	}

	/// <summary>
	/// 地图改变时执行（仅限服务器）
	/// </summary>
	/// <param name="sceneName"></param>
	public override void OnServerSceneChanged(string sceneName)
	{
		base.OnServerSceneChanged(sceneName);
		if (sceneName == onlineScene)
		{
			GameObject gcPrefab = spawnPrefabs.Find((g) => { if (g.name == "GlobalConfigs") return true; return false; });
			GameObject globalConfigs = Instantiate(gcPrefab);
			NetworkServer.Spawn(globalConfigs);
			this.globalConfigs = globalConfigs;
		}
	}

	/// <summary>
	/// 客户端断开连接时执行（仅限客户端）
	/// </summary>
	public override void OnClientDisconnect()
	{
		GlobalConfigs.idIsSet = false;
	}
	/// <summary>
	/// 客户端停止时执行（仅限客户端）
	/// </summary>
	public override void OnStopClient()
	{
		GlobalConfigs.idIsSet = false;
	}
}
