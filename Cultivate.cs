using UnityEngine;
using System.Collections.Generic;

public class Cultivate : MonoBehaviour
{
    public int ClickNeed = 1;  // 各等级所需的点击次数

    private Dictionary<FarmLand, int> cultivateProgress = new Dictionary<FarmLand, int>();  // 存储农田的开垦进度

    public CursorManager cursor;

    // 更新农田的开垦进度，检查是否达到该等级要求的点击次数
    private bool HandleProgressUpdate(FarmLand clickedFarmLand, ref Dictionary<FarmLand, int> progressDictionary, int clickNeed, int targetStatus)
    {
        if (clickedFarmLand == null) return false;

        if (!progressDictionary.ContainsKey(clickedFarmLand))
        {
            progressDictionary[clickedFarmLand] = 0;  // 初始化农田的开垦进度
        }

        progressDictionary[clickedFarmLand]++;  // 增加点击次数
        Debug.Log("clicked");

        // 当进度达到要求，更新农田状态
        if (progressDictionary[clickedFarmLand] >= clickNeed)
        {
            clickedFarmLand.Transition(targetStatus);  // 改变农田的状态
            progressDictionary[clickedFarmLand] = 0;
            return true;  // 开垦完成
        }

        return false;  // 任务未完成
    }


        void Start()
    {
        // 获取当前关卡信息并初始化//
    }

    void Update()
    {
        if (cursor.isToolMode && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 获取鼠标位置
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);  // 使用2D射线检测点击

            // 判断点击的物体是否是FarmLand
            if (hit.collider != null&& hit.transform.CompareTag("FarmLand"))
            {
                FarmLand clickedFarmLand = hit.transform.GetComponent<FarmLand>();
                if (clickedFarmLand != null && clickedFarmLand.Status == (int)FarmLand.FarmLandStatus.NeedCultivate)
                {
                    // 如果状态是“需要开垦”，执行进度更新
                    if (HandleProgressUpdate(clickedFarmLand, ref cultivateProgress, ClickNeed, (int)FarmLand.FarmLandStatus.NeedSow))
                    {
                        Debug.Log("Cultivating completed!");  // 开垦完成
                    }
                }
            }
        }
    }
}
