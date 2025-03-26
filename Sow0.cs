using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sow0 : MonoBehaviour
{
    public int Level = 0;  // 当前播种的等级
    public int SeedKind = 0;  // 种子类型
    public int Yield = 10;  // 产量
    public int GrowTimeNeed = 10;  // 生长所需时间
    public int Cost = 100;  // 播种的花费
    public CursorManager cursor;

    private Dictionary<FarmLand, int> sowProgress = new Dictionary<FarmLand, int>(); // 存储播种进度

    // UI 更新方法，显示种子信息
    private void UpdateUI()
    {
        // 这里可以更新UI，显示种子类型、产量、种植时间等信息
        Debug.Log($"SeedKind: {SeedKind}, Yield: {Yield}, GrowTimeNeed: {GrowTimeNeed}, Level: {Level}");
    }

    // 等级提升方法
    public void LevelUP()
    {
        Level = Mathf.Min(Level + 1, 3);  // 提升等级，最多3级
        Cost = Mathf.Max(Cost - 10, 70);  // 每级降低播种成本，最低为70
        GrowTimeNeed = Mathf.Max(GrowTimeNeed - 1, 7);  // 每级减少生长时间，最小为7
        Yield = Mathf.Min(Yield + 3, 19);  // 每级增加产量，最大为19

        // 更新 UI
        Debug.Log("shengjichenggong");
    }

    // 通用的进度更新方法
    private bool HandleProgressUpdate(FarmLand clickedFarmLand, ref Dictionary<FarmLand, int> progressDictionary, int targetStatus)
    {
        if (clickedFarmLand == null) return false;  // 如果点击的农田为空，返回false

        if (!progressDictionary.ContainsKey(clickedFarmLand))  // 如果没有该农田的进度，初始化为0
        {
            progressDictionary[clickedFarmLand] = 0;
        }

        progressDictionary[clickedFarmLand]++;  // 增加农田的播种进度

        // 当进度完成时，更新农田状态并设置种子类型和产量
        if (progressDictionary[clickedFarmLand] >= 1)  // 播种只需一次
        {
            clickedFarmLand.Transition(targetStatus);  // 转变为下一个状态
            clickedFarmLand.SeedKind = SeedKind;  // 设置农田的种子类型
            clickedFarmLand.Yield = Yield;  // 设置农田的产量
            return true;  // 播种完成
        }

        return false;  // 播种未完成
    }


        void Start()
    {
        // 获取当前关卡信息并初始化//
    }
    void Update()
    {
        if (cursor.isToolMode && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 将鼠标位置转换为世界坐标
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);  // 使用2D射线检测

            if (hit.collider != null)
            {
                FarmLand clickedFarmLand = hit.transform.GetComponent<FarmLand>();  // 获取点击的农田对象
                if ((clickedFarmLand != null) && (clickedFarmLand.Status == (int)FarmLand.FarmLandStatus.NeedSow))
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
