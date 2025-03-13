using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransplanterControl : MonoBehaviour
{
    public float plowDelay = 1.5f; // 每块农田耕地的间隔
    public CursorManager cursor;

    void Update()
    {
        if (cursor.isToolMode)
        {
            StartCoroutine(AutoPlowFarmlands());
        }
    }

    private IEnumerator AutoPlowFarmlands()
    {
        // 找到所有未耕地的农田
        FarmLand[] allFarmlands = FindObjectsOfType<FarmLand>();
        List<FarmLand> needPlowFarmlands = new List<FarmLand>();

        foreach (FarmLand farmland in allFarmlands)
        {
            if (farmland.Status == (int)FarmLand.FarmLandStatus.NeedCultivate)
            {
                needPlowFarmlands.Add(farmland);
            }
        }

        // 依次耕地
        foreach (FarmLand farmland in needPlowFarmlands)
        {
            if (farmland != null && farmland.Status == (int)FarmLand.FarmLandStatus.NeedCultivate)
            {
                farmland.PlayPlowAnimation(); // 🎬 触发动画
                yield return new WaitForSeconds(plowDelay); // 等待动画完成
                farmland.Transition((int)FarmLand.FarmLandStatus.NeedSow); // 🎯 变为可播种状态
            }
        }

        cursor.isToolMode = false; // 任务完成，释放状态
    }
}
