using UnityEngine;

public class TechNodeButton : MonoBehaviour
{
    public string techName; // 科技名称
    public bool isUnlocked = false; // 是否已解锁
    public LevelItem linkedItem; // 关联的升级物品
    public TechCategory techCategory; // 科技类别（使用枚举）
    public int techIndex; // 科技序号
    

    public bool CanUnlock()
    {
        // 检查同一类别中，前一个科技是否已解锁
        TechNodeButton[] allButtons = FindObjectsOfType<TechNodeButton>();
        foreach (var button in allButtons)
        {
            if (button.techCategory == techCategory && button.techIndex == techIndex - 1) // 查找前一个
            {
                return button.isUnlocked; // 前一个已解锁，当前才能解锁
            }
        }
        return techIndex == 0; // 如果是当前类别的第一个按钮，直接解锁
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            if (CanUnlock()) // 先检查是否满足解锁条件
            {
                isUnlocked = true;
                Debug.Log("科技解锁：" + techName);

                // 调用 LevelUp 进行升级
                if (linkedItem != null)
                {
                    linkedItem.LevelUp();
                }
                else
                {
                    Debug.LogWarning("未绑定 LevelItem，无法升级！");
                }
            }
            else
            {
                Debug.LogError($"无法解锁 {techName}，前置科技未解锁！");
                TechTreeUI.Instance.ShowErrorPanel("无法解锁，前置科技未解锁！");
            }
        }
        else
        {
            Debug.Log("科技已解锁，无法重复解锁！");
        }
    }
}
