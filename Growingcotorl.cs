using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growingcotorl : MonoBehaviour
{
    public Growing grow;
    public GameObject plant1;
    public GameObject plant2;
    public GameObject plant3;
    public Sow0 sow;
    public FarmLand farmland;

    private float stage1;
    private float stage2;

    void Start()
    {
        if (sow != null)
        {
            float totalTime = sow.GrowTimeNeed - 1;
            stage1 = totalTime / 3;
            stage2 = 2 * totalTime / 3;
        }
        SetActivePlant(null); // 初始化时关闭所有植物
    }

    void Update()
    {
        int currentStatus = farmland.Status;  // 获取当前农田状态

        if (currentStatus == (int)FarmLand.FarmLandStatus.Growing)
        {
            UpdatePlantStage(); // 根据生长进度更新植物
        }
        else if (currentStatus == (int)FarmLand.FarmLandStatus.NeedHarvest)
        {
            SetActivePlant(plant3); // 保持 plant3 可见
        }
        else
        {
            SetActivePlant(null); // 其他状态（如 Empty）时隐藏植物
        }
    }

    void UpdatePlantStage()
    {
        if (grow.growProgress < stage1)
        {
            SetActivePlant(plant1);
        }
        else if (grow.growProgress < stage2)
        {
            SetActivePlant(plant2);
        }
        else
        {
            SetActivePlant(plant3);
        }
    }

    void SetActivePlant(GameObject activePlant)
    {
        plant1.SetActive(activePlant == plant1);
        plant2.SetActive(activePlant == plant2);
        plant3.SetActive(activePlant == plant3);
    }
}
