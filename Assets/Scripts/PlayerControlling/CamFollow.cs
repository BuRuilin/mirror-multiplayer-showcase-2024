using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
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
    /// ���y��z�᲻�䣬x������ɫ
    /// </summary>
	private void FixedUpdate()
	{
        if (localPlayer!=null)
        {
			transform.position = new Vector3(localPlayer.transform.position.x, transform.position.y, transform.position.z);
		}
	}
}
