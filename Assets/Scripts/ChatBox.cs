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

    //��֪����������Ϣ�ˣ��÷���������Ϣ�ַ��������ͻ���
	[Command(requiresAuthority = false)]
    public void SendMsg(string msg,int Id)
    {
        ReceiveMsg(msg, Id);
    }

    //�ͻ��˽��յ���Ϣ
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

    //��Ϣ�б������ײ���չʾ������Ϣ
    IEnumerator ScrollToBottom()
    {
        yield return null;//�ȴ�һ֡��ui�������ٻ���
		scrollRect.verticalNormalizedPosition = 0;
	}
}
