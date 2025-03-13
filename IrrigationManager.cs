using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrrigationManager : MonoBehaviour
{

    public bool Status = false;

    public List<FarmLand> farmLands;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var farmLand in farmLands)
        {
            // 如果农田状态为“需要灌溉”并且水车已完成灌溉
            if (farmLand.Status == (int)FarmLand.FarmLandStatus.NeedIrrigate && Status)
            {
                // 更新农田状态为“正在生长”
                farmLand.Transition((int)FarmLand.FarmLandStatus.Growing);
                Debug.Log("FarmLand irrigated!");  // 输出灌溉成功的信息
            }
        }
    }
}
