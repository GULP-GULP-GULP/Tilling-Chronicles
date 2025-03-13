using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public int current;  // 当前经济数量

    // 增加经济值
    public void Increase(int num)
    {
        current += num;  // 简单的增加操作
    }

    // 减少经济值
    public bool Decrease(int num)
    {
        if (current >= num)
        {
            current -= num;  // 足够时减少经济值
            return true;  // 成功减少
        }
        else
        {
            return false;  // 余额不足
        }
    }

    void Start()
    {
        // 获取当前关卡信息并初始化//
    }


    void Update()
    {
        // 你可以在这里更新UI显示当前经济数量，例如：
        // uiText.text = "经济：" + current;
    }
}
