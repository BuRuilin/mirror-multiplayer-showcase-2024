using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机跟随
/// </summary>
public class CamFollow : MonoBehaviour
{
    public static GameObject localPlayer = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 相机y、z轴不变，x轴跟随角色
    /// </summary>
	private void FixedUpdate()
	{
        if (localPlayer!=null)
        {
			transform.position = new Vector3(localPlayer.transform.position.x, transform.position.y, transform.position.z);
		}
	}
}
