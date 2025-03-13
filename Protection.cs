using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : MonoBehaviour
{
    public Earthquake earthquake; // 你可以通过这个引用来对接地震类，或者用它来处理与地震相关的逻辑
    public int protectionStrength = 2; // 可保护的农田数
    private bool isActivated = false;

    // 激活保护
    public void ActivateProtection()
    {
        isActivated = true;
        Debug.Log("🛡️ 保护措施已激活！");
        // 你可以在这里添加其他逻辑，比如触发一些防护效果
    }

    // 失效保护
    public void DeactivateProtection()
    {
        isActivated = false;
        Debug.Log("🛡️ 保护措施失效！");
        // 在这里可以添加失效时的反馈效果
    }

    // 获取当前的保护级别
    public int GetProtectionLevel()
    {
        return isActivated ? protectionStrength : 0;
    }

    // 如果这个物体被点击，激活保护
    public void Protecte()
    {
        if (!isActivated)
        {
            ActivateProtection();
        }
        else
        {
            DeactivateProtection();
        }
    }
    void Update()
    {
    }
}
