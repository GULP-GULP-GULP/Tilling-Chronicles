using UnityEngine;
using System.Collections.Generic;

public class Harvest : MonoBehaviour
{
    public int ClickNeed =  1;

    private Dictionary<FarmLand, int> harvestProgress = new Dictionary<FarmLand, int>(); // 存储每块农田的收获进度

    // 目标类，存储每种作物的收获量
    public Foodtobeprocessed Foodtobeprocessed;

    public CursorManager cursor;


    // 通用的进度更新方法
    private bool HandleProgressUpdate(FarmLand clickedFarmLand, ref Dictionary<FarmLand, int> progressDictionary, int clickNeed, int targetStatus)
    {
        if (clickedFarmLand == null) return false;  // 如果点击的农田为空，返回false

        if (!progressDictionary.ContainsKey(clickedFarmLand))  // 如果进度字典中没有该农田，初始化为0
        {
            progressDictionary[clickedFarmLand] = 0;
        }

        progressDictionary[clickedFarmLand]++;  // 增加农田的进度
        Debug.Log("clicked");

        // 这里可以更新UI，显示当前进度
        // 例如：progressText.text = $"{progressDictionary[clickedFarmLand]}/{ClickNeed[Level]}";

        // 如果进度达到所需点击次数，更新农田状态并返回true表示任务完成
        if (progressDictionary[clickedFarmLand] >= clickNeed)
        {
            clickedFarmLand.Transition(targetStatus);
            progressDictionary[clickedFarmLand] = 0;  // 转变农田状态为需要的状态
            return true;  // 完成任务
        }

        return false;  // 任务未完成
    }

    // 每帧更新




     void Awake()
{
    Foodtobeprocessed = FindObjectOfType<Foodtobeprocessed>();

    if (Foodtobeprocessed == null)
    {
        Debug.LogWarning("Foodtobeprocessed 组件未找到！请检查场景中是否有它。");
    }
}



    void Update()
    {
        // 检测玩家是否按下W键并点击鼠标左键
        if (cursor.isToolMode && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 将鼠标位置转换为世界坐标
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);  // 使用2D射线检测

            if (hit.collider != null && hit.transform.CompareTag("FarmLand"))  // 如果点击的对象是标签为“FarmLand”的物体
            {
                FarmLand clickedFarmLand = hit.transform.GetComponent<FarmLand>();  // 获取点击的农田对象
                if (clickedFarmLand != null && clickedFarmLand.Status == (int)FarmLand.FarmLandStatus.NeedHarvest)
                {
                    if (HandleProgressUpdate(clickedFarmLand, ref harvestProgress, ClickNeed, (int)FarmLand.FarmLandStatus.NeedCultivate))
                    {
                        // 根据收获的种类更新目标的收获量
                        switch (clickedFarmLand.SeedKind)
                        {
                            case 0:
                                Foodtobeprocessed.Yield0 += clickedFarmLand.Yield;  // 种子0的产量
                                break;
                            case 1:
                                Foodtobeprocessed.Yield1 += clickedFarmLand.Yield;  // 种子1的产量
                                break;
                            case 2:
                                Foodtobeprocessed.Yield2 += clickedFarmLand.Yield;  // 种子2的产量
                                break;
                            case 3:
                                Foodtobeprocessed.Yield3 += clickedFarmLand.Yield;  // 种子3的产量
                                break;
                        }

                        clickedFarmLand.SeedKind = -1;  // 将农田的种子类型重置为-1，表示未播种
                        Debug.Log("收割完成");
                    }
                }
            }
        }
    }
}
