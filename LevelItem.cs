using UnityEngine;

public class LevelItem : MonoBehaviour
{
    public GameObject[] itemLevels; // 不同等级的物品
    private int currentLevel = 0; // 当前等级

    void Start()
    {
        if (itemLevels == null || itemLevels.Length == 0)
        {
            Debug.LogError(name + " 没有设置 itemLevels！");
            return;
        }

        // 只显示当前等级的物品
        for (int i = 0; i < itemLevels.Length; i++)
        {
            itemLevels[i].SetActive(i == currentLevel);
        }
    }

    public void LevelUp()
    {
        if (currentLevel < itemLevels.Length - 1)
        {
            itemLevels[currentLevel].SetActive(false); // 关闭当前物品
            currentLevel++;
            itemLevels[currentLevel].SetActive(true);  // 激活下一级物品
            Debug.Log(name + " 升级到 " + currentLevel);
        }
        else
        {
            Debug.Log(name + " 已经是最高级，无法继续升级！");
        }
    }
}
