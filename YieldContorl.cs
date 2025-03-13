using System.Collections;
using UnityEngine;

public class YieldControl : MonoBehaviour
{
    public FarmLand farmland; // 绑定农田对象
    private bool isReducing = false; // 是否正在减少产量
    private float decayDuration = 5f; // 产量衰减持续时间
    private float decayRate = 0.04f; // 每秒减少 4%

    void Update()
    {
        if (farmland != null && farmland.Status == 4 && !isReducing)
        {
            StartCoroutine(ReduceYieldOverTime()); // 启动产量衰减
        }
    }

    private IEnumerator ReduceYieldOverTime()
    {
        isReducing = true;
        float elapsedTime = 0f;

        while (elapsedTime < decayDuration && farmland.Yield > 0)
        {
            farmland.Yield = Mathf.Max(0, Mathf.RoundToInt(farmland.Yield * (1 - decayRate)));
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }

        // 5 秒后检查，如果产量归零，变为 `NeedCultivate = 0`
        if (farmland.Yield <= 0)
        {
            farmland.Yield = 0;
            farmland.Status = 0; // 变为需要开垦状态
        }

        isReducing = false;
    }
}
