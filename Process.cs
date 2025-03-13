using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process : MonoBehaviour
{
    public Foodtobeprocessed foodSource; // 未加工食物
    public Target processedTarget; // 加工后存储目标

    private int level = 1; // 处理等级（默认 1 级）
    private int processSpeed = 5; // 每秒处理 5 单位，升级后递增
    private float processInterval = 1f; // 处理间隔（秒）
    private float lossRate = 0.1f; // 损耗率（默认 10%）

    private int currentProcessingIndex = 0; // 当前处理的作物索引

    void Start()
    {
        StartCoroutine(ProcessFood());
    }

    IEnumerator ProcessFood()
    {
        while (true)
        {
            if (!HasFoodToProcess())
            {
                yield return null; // 没有可处理的食物，等待下一帧
                continue;
            }

            if (GetYield(currentProcessingIndex) > 0)
            {
                int processNow = Mathf.Min(processSpeed, GetYield(currentProcessingIndex)); // 每次最多处理 processSpeed 单位
                ReduceYield(currentProcessingIndex, processNow);
                IncreaseTarget(currentProcessingIndex, Mathf.FloorToInt(processNow * (1 - lossRate))); // 按当前损耗率计算

                UpdateUI();
            }

            // 处理完当前作物，切换到下一个
            currentProcessingIndex = (currentProcessingIndex + 1) % 4;
            yield return new WaitForSeconds(processInterval); // 按当前升级等级的处理时间
        }
    }

    public void LevelUP()
    {
        if (level >= 10)
        {
            Debug.Log("等级已满！");
            return;
        }

        level++;
        processSpeed = Mathf.Min(14, processSpeed + 1); // 每级+1，最高14
        lossRate = Mathf.Max(0.01f, lossRate - 0.01f); // 损耗减少，最低 1%

        Debug.Log($"升级至 {level} 级！处理速度：{processSpeed}/s，处理间隔：{processInterval}s，损耗率：{lossRate * 100}%");
    }

    bool HasFoodToProcess()
    {
        return foodSource.Yield0 > 0 || foodSource.Yield1 > 0 || foodSource.Yield2 > 0 || foodSource.Yield3 > 0;
    }

    int GetYield(int index)
    {
        switch (index)
        {
            case 0: return foodSource.Yield0;
            case 1: return foodSource.Yield1;
            case 2: return foodSource.Yield2;
            case 3: return foodSource.Yield3;
            default: return 0;
        }
    }

    void ReduceYield(int index, int amount)
    {
        switch (index)
        {
            case 0: foodSource.Yield0 -= amount; break;
            case 1: foodSource.Yield1 -= amount; break;
            case 2: foodSource.Yield2 -= amount; break;
            case 3: foodSource.Yield3 -= amount; break;
        }
    }

    void IncreaseTarget(int index, int amount)
    {
        switch (index)
        {
            case 0: processedTarget.Yield0 += amount; break;
            case 1: processedTarget.Yield1 += amount; break;
            case 2: processedTarget.Yield2 += amount; break;
            case 3: processedTarget.Yield3 += amount; break;
        }
    }

    void UpdateUI()
    {
        Debug.Log($"Processing: Yield0={foodSource.Yield0}, Yield1={foodSource.Yield1}, Yield2={foodSource.Yield2}, Yield3={foodSource.Yield3}");
        Debug.Log($"Processed: Target0={processedTarget.Yield0}, Target1={processedTarget.Yield1}, Target2={processedTarget.Yield2}, Target3={processedTarget.Yield3}");
    }
}