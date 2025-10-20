using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable enable
/// <summary>
/// 游戏管理脚本
/// </summary>
public class GameManager : MonoBehaviour
{
	public static bool isMsgFocused = false;
	public static bool isPause = false;
	public GameObject pausePanel;
	public InputField msgInput;
	public GameObject taskFinishUI;
	public int coinTotal = 0;
	private void Start()
	{
	}
	private void FixedUpdate()
	{
		isMsgFocused = msgInput.isFocused;
		GlobalConfigs? configs = GlobalConfigs.config;
		if (configs == null) return;
		if (configs.bronzeCoinCount + configs.silverCoinCount + configs.goldCoinsCount >= coinTotal)
		{
			taskFinishUI.SetActive(true);
		}
	}
	public void OnPause()
	{
		isPause = true;
		pausePanel.SetActive(true);
	}
}
