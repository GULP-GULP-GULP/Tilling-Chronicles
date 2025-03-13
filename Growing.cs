using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growing : MonoBehaviour
{
    private FarmLand farmland;  // 获取农田组件
    public Sow0 sow0;  // 种子组件 0
    public Sow1 sow1;  // 种子组件 1
    public Sow2 sow2;  // 种子组件 2
    public Sow3 sow3;  // 种子组件 3

    private float growProgress = 0f;  // 当前生长进度

    // 初始化，获取农田组件和种子组件
    void Start()
{
    farmland = GetComponent<FarmLand>();
    if (farmland == null)
    {
        Debug.Log("FarmLand component not found on " + gameObject.name);
    }
}

    void Update()
    {

        int currentStatus = farmland.Status;  // 获取当前农田的状态
        // 如果农田状态是“正在生长”，则开始处理生长进度
        if (currentStatus == (int)FarmLand.FarmLandStatus.Growing)
        {
            int SeedKind = farmland.SeedKind;  // 获取当前种子的类型

            int growTime = 0;  // 默认的生长时间

            // 根据不同的种子种类选择对应的生长时间
            switch (SeedKind)
            {
                case 0:
                    growTime = sow0.GrowTimeNeed;  // 获取生长时间
                    break;

                case 1:
                    growTime = sow1.GrowTimeNeed;  // 获取生长时间
                    break;

                case 2:
                    growTime = sow2.GrowTimeNeed;  // 获取生长时间
                    break;

                case 3:
                    growTime = sow3.GrowTimeNeed;  // 获取生长时间
                    break;
            }

            // 增加生长进度
            growProgress += Time.deltaTime;

            // 如果生长进度达到所需时间，农田生长完成
            if (growProgress >= growTime)
            {
                farmland.Transition((int)FarmLand.FarmLandStatus.NeedHarvest);  // 更新农田状态为“需要收获”
                growProgress = 0f;  // 重置生长进度，准备下一轮生长
                Debug.Log("Growing completed!");
            }
        }
    }
}
