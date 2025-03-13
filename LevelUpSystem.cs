using System.Collections.Generic; // 添加这一行
using UnityEngine;
using UnityEngine.UI;


public class LevelUpSystem : MonoBehaviour
{
    public List<Button> toolButtons; // 存放所有等级的工具按钮
    private int currentLevel = 0; // 当前等级

    void Start()
    {
        UpdateToolUI();
    }

    public void UpgradeTool()
{
    if (currentLevel < toolButtons.Count - 1)
    {
        // 获取当前按钮的 CursorManager
        CursorManager currentCursorManager = toolButtons[currentLevel].GetComponent<CursorManager>();
        
        // 关闭当前工具的 ToolMode（如果需要）
        if (currentCursorManager != null)
        {
            currentCursorManager.DeactivateToolMode();
        }

        // 隐藏旧按钮
        toolButtons[currentLevel].gameObject.SetActive(false);
        
        // 升级
        currentLevel++;

        // 显示新按钮
        toolButtons[currentLevel].gameObject.SetActive(true);
    }
}

    void UpdateToolUI()
    {
        // 确保只有当前等级的按钮可见
        for (int i = 0; i < toolButtons.Count; i++)
        {
            toolButtons[i].gameObject.SetActive(i == currentLevel);
        }
    }
}

