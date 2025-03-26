using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sow1 : MonoBehaviour
{
    public int Level = 0;
    public int SeedKind = 1;  // 种子类型
    public int Yield = 10;  // 产量
    public int GrowTimeNeed = 10; // 生长时间
    public int Cost = 100;
    public CursorManager cursor;

    private Dictionary<FarmLand, int> sowProgress = new Dictionary<FarmLand, int>(); // 存储播种的进度

    // UI 更新方法
    private void UpdateUI()
    {
        // UI 更新代码（显示种子类型、产量、种植时间等信息）
        Debug.Log($"SeedKind: {SeedKind}, Yield: {Yield}, GrowTimeNeed: {GrowTimeNeed}, Level: {Level}");
    }

    // 等级提升方法
    public void LevelUP()
    {
        Level = Mathf.Min(Level + 1, 3);  // 提升等级，最多3级
        GrowTimeNeed = Mathf.Max(GrowTimeNeed - 1, 7); // 生长时间减少，最小为7
        Yield = Mathf.Min(Yield + 3, 19);  // 最大产量19

        // 更新 UI
        UpdateUI();
    }

    // 通用的进度更新方法
    private bool HandleProgressUpdate(FarmLand clickedFarmLand, ref Dictionary<FarmLand, int> progressDictionary, int targetStatus)
    {
        if (clickedFarmLand == null) return false;

        if (!progressDictionary.ContainsKey(clickedFarmLand))
        {
            progressDictionary[clickedFarmLand] = 0;
        }

        progressDictionary[clickedFarmLand]++;

        // UI 更新的代码
        // 例如：progressText.text = $"{progressDictionary[clickedFarmLand]}/{ClickNeed[Level]}";

        // 当进度完成时，转变为下一个状态
        if (progressDictionary[clickedFarmLand] >= 1) // 这里只需播种一次
        {
            clickedFarmLand.Transition(targetStatus);
            clickedFarmLand.SeedKind = SeedKind;  // 设置种子类型
            clickedFarmLand.Yield = Yield;  // 设置农田的产量
            return true; // 播种完成
        }

        return false; // 播种未完成
    }

    // Update is called once per frame
    
        void Start()
    {
        // 获取当前关卡信息并初始化//
    }
    
    void Update()
    {
        // 按住 1 键并点击鼠标左键进行播种
        if (cursor.isToolMode && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 将鼠标位置转换为世界坐标
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);  // 使用2D射线检测

            // 发射射线检测点击位置
            if (hit.collider != null)
            {
                // 获取点击的农田
                FarmLand clickedFarmLand = hit.transform.GetComponent<FarmLand>();
                if (clickedFarmLand != null && clickedFarmLand.Status == (int)FarmLand.FarmLandStatus.NeedSow)
                {
                    // 播种：直接完成播种，更新状态为 NeedIrrigate
                    if (HandleProgressUpdate(clickedFarmLand, ref sowProgress, (int)FarmLand.FarmLandStatus.Growing))
                    {
                        // 播种后更新UI
                        UpdateUI();
                    }
                }
            }
        }
    }
}
