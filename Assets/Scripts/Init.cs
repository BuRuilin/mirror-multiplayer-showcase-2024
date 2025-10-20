using Mirror;
using UnityEngine;

/// <summary>
/// 游戏初始化脚本
/// </summary>
public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //向mirror注册新的消息类型，用于接收id
		NetworkClient.RegisterHandler<IdMessage>(OnIdReceived);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //收到id消息时执行
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
/// id消息主体
/// </summary>
public struct IdMessage : NetworkMessage
{
    public int id;
    public IdMessage(int id)
    {
        this.id = id;
    }
}