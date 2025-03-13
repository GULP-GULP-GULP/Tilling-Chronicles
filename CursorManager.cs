using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D hoeCursor;      // **锄头鼠标贴图**
    public Texture2D defaultCursor;  // **默认鼠标贴图**

    public bool isToolMode = false; // **是否处于工具模式**

    void Start()
    {
        SetDefaultCursor();
    }

    /// <summary>
    /// 切换到锄头模式，并关闭其他 CursorManager 的工具模式
    /// </summary>
    public void ActivateToolMode()
    {
        DisableOtherToolModes(); // **先禁用其他 CursorManager 的 ToolMode**
        isToolMode = true;
        Cursor.SetCursor(hoeCursor, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// 退出工具模式，恢复默认鼠标
    /// </summary>
    public void DeactivateToolMode()
    {
        isToolMode = false;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// 设置默认鼠标
    /// </summary>
    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// 切换工具模式（开关）
    /// </summary>
    public void ToolMode()
    {
        if (!isToolMode)
        {
            ActivateToolMode();
        }
        else
        {
            DeactivateToolMode();
        }
    }

    /// <summary>
    /// 关闭场景中所有其他 CursorManager 的工具模式
    /// </summary>
    private void DisableOtherToolModes()
    {
        CursorManager[] allManagers = FindObjectsOfType<CursorManager>(); // 获取场景中的所有 CursorManager

        foreach (CursorManager manager in allManagers)
        {
            if (manager != this) // 排除自己
            {
                manager.DeactivateToolMode();
            }
        }
    }
}
