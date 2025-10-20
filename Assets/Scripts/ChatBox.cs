using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ChatBox : NetworkBehaviour
{
    public GameObject msgPrefab;
    public GameObject msgListContent;
    public ScrollRect scrollRect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        
	}

    //告知服务器发消息了，让服务器把消息分发给各个客户端
	[Command(requiresAuthority = false)]
    public void SendMsg(string msg,int Id)
    {
        ReceiveMsg(msg, Id);
    }

    //客户端接收到消息
    [ClientRpc]
    public void ReceiveMsg(string msg,int Id)
    {
        GameObject msgObject = GameObject.Instantiate(msgPrefab);
        msgObject.GetComponent<Text>().text = $"[P{Id + 1}]:{msg}";
        msgObject.GetComponent<Text>().color = GlobalConfigs.colors[Id % GlobalConfigs.colors.Length];
        msgObject.transform.SetParent(msgListContent.transform);
        msgObject.transform.localScale = Vector3.one;
        StartCoroutine(ScrollToBottom());
    }

    //消息列表滑动到底部，展示最新消息
    IEnumerator ScrollToBottom()
    {
        yield return null;//等待一帧，ui更新了再滑动
		scrollRect.verticalNormalizedPosition = 0;
	}
}
