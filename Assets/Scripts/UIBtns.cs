using Mirror;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// UGUI������غ���
/// </summary>
public class UIBtns : MonoBehaviour
{
    public NetworkManager manager;
    public InputField ip;
    public InputField port;

    public Dropdown quality;
    public Dropdown scene;

    public GameObject pausePanel;

    public InputField msgInput;
    public ChatBox chatBox;
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ������������ť����ʱִ�У���ͬʱ����http��������mirror
    /// </summary>
    public void ServerBtnClick()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
			if (Transport.active is PortTransport portTransport)
			{
                if (ushort.TryParse(port.text, out ushort p))
                {
                    portTransport.Port = p;
                    Debug.Log(p);
                }
			}
            HttpServer.scene = manager.onlineScene;
            HttpServer.Start(ip.text);
            manager.StartHost();
            Debug.Log(manager.networkAddress);
        }
    }

    /// <summary>
    /// ��Ϊ�ͻ��˰�ť����ʱִ�У���������http��������ȡ��ͼ��������mirror
    /// </summary>
    public void ClientBtnClick()
    {
		if (!NetworkClient.isConnected && !NetworkServer.active)
		{
            StartCoroutine(ConnectServer());
		}
	}

    IEnumerator ConnectServer()
    {
		var request = UnityWebRequest.Get($"http://{ip.text}:26666/");
        yield return request.SendWebRequest();
        while (!request.isDone)
        {
            yield return null;
        }
        if (string.IsNullOrEmpty(request.error))
        {
            manager.onlineScene = request.downloadHandler.text;
            Debug.Log(manager.onlineScene);
            if (Transport.active is PortTransport portTransport)
            {
                if (ushort.TryParse(port.text, out ushort p))
                    portTransport.Port = p;
            }
            manager.networkAddress = ip.text;
            manager.StartClient();
        }
	}

    /// <summary>
    /// ����ѡ��ı�ʱִ��
    /// </summary>
    public void OnGraphicSettingChanged()
    {
        Debug.Log(quality.value);
        QualitySettings.SetQualityLevel(quality.value);
    }
    /// <summary>
    /// ������Ϸ��ť����ʱִ��
    /// </summary>
    public void OnContinueBtnClick()
    {
        pausePanel.SetActive(false);
        GameManager.isPause = false;
    }
    /// <summary>
    /// ���Ͱ�ť����ʱִ��
    /// </summary>
    public void OnSendBtnClick()
    {
        if (msgInput.text != "")
        {
            chatBox.SendMsg(msgInput.text, GlobalConfigs.localId);
            msgInput.text = "";
        }
    }
    /// <summary>
    /// ��ͼѡ���ı�ʱִ��
    /// </summary>
    public void OnSceneChanged()
    {
        Debug.Log(GlobalConfigs.scenes.Length);
        Debug.Log(GlobalConfigs.scenes[scene.value]);
        manager.onlineScene = GlobalConfigs.scenes[scene.value];
    }

    /// <summary>
    /// ��Ϣ���ڰ��»س�ʱִ��
    /// </summary>
    public void OnInputSubmit()
    {
		if (msgInput.text != "")
		{
			chatBox.SendMsg(msgInput.text, GlobalConfigs.localId);
			msgInput.text = "";
		}
	}

    /// <summary>
    /// �˳���Ϸ��ť����ʱִ��
    /// </summary>
    public void ExitGameBtnClick()
    {
		
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#endif
		Application.Quit();

	}

    public void BackBtnClick()
    {
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopServer();
    }
}
