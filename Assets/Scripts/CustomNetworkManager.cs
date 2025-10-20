using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
	private GameObject globalConfigs;
	private int idCount = 0;

	/// <summary>
	/// ��Ҽ���ʱִ�У����޷�������
	/// </summary>
	/// <param name="conn"></param>
	public override void OnServerAddPlayer(NetworkConnectionToClient conn)
	{
		base.OnServerAddPlayer(conn);
		NetworkServer.SendToAll<IdMessage>(new(idCount));
		idCount++;
	}

	/// <summary>
	/// ��ͼ�ı�ʱִ�У����޷�������
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
	/// �ͻ��˶Ͽ�����ʱִ�У����޿ͻ��ˣ�
	/// </summary>
	public override void OnClientDisconnect()
	{
		GlobalConfigs.idIsSet = false;
	}
	/// <summary>
	/// �ͻ���ֹͣʱִ�У����޿ͻ��ˣ�
	/// </summary>
	public override void OnStopClient()
	{
		GlobalConfigs.idIsSet = false;
	}
}
