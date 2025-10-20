using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 硬币数量UI显示相关
/// </summary>
public class UICoinCount : MonoBehaviour
{
    // Start is called before the first frame update
    public Text brozen;
    public Text silver;
    public Text gold;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalConfigs.config != null)
        {
            brozen.text = GlobalConfigs.config.bronzeCoinCount.ToString();
            silver.text = GlobalConfigs.config.silverCoinCount.ToString();
            gold.text = GlobalConfigs.config.goldCoinsCount.ToString();
        }
    }
}
