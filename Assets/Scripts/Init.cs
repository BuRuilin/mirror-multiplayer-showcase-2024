using Mirror;
using UnityEngine;

/// <summary>
/// ��Ϸ��ʼ���ű�
/// </summary>
public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //��mirrorע���µ���Ϣ���ͣ����ڽ���id
		NetworkClient.RegisterHandler<IdMessage>(OnIdReceived);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�յ�id��Ϣʱִ��
    void OnIdReceived(IdMessage message)
    {
        Debug.Log("Recv Id");
        if (!GlobalConfigs.idIsSet)
        {
            GlobalConfigs.idIsSet = true;
            GlobalConfigs.localId = message.id;
            Debug.Log(GlobalConfigs.localId);
        }
    }
}
/// <summary>
/// id��Ϣ����
/// </summary>
public struct IdMessage : NetworkMessage
{
    public int id;
    public IdMessage(int id)
    {
        this.id = id;
    }
}