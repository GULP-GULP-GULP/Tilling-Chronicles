using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlowButtonControl : MonoBehaviour
{
    public CursorManager cursor;
    private List<FarmLand> selectedFarmlands = new List<FarmLand>(); // 记录选中的农田
    public float plowDelay = 1.5f; // 每块农田耕地间隔时间

    void Update()
    {
        bool shouldHighlight = cursor.isToolMode; // 是否处于耕地模式
        FarmLand[] allFarmlands = FindObjectsOfType<FarmLand>(); // 获取所有农田对象

        foreach (FarmLand farmland in allFarmlands)
        {
            if (farmland.Status == (int)FarmLand.FarmLandStatus.NeedCultivate)
            {
                farmland.UpdateHighlight(shouldHighlight);
            }
        }

        // 选择农田
        if (cursor.isToolMode && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.transform.CompareTag("FarmLand"))
            {
                FarmLand clickedFarmLand = hit.transform.GetComponent<FarmLand>();
                if (clickedFarmLand != null && clickedFarmLand.Status == (int)FarmLand.FarmLandStatus.NeedCultivate)
                {
                    if (!selectedFarmlands.Contains(clickedFarmLand))
                    {
                        selectedFarmlands.Add(clickedFarmLand); // 添加到耕地队列
                    }
                }
            }
        }
    }

    public void OnPlowButtonClick()
    {
        if (cursor.isToolMode && selectedFarmlands.Count > 0)
        {
            StartCoroutine(PlowSelectedLands());
        }
    }

    private IEnumerator PlowSelectedLands()
    {
        foreach (FarmLand farmland in selectedFarmlands)
        {
            if (farmland != null && farmland.Status == (int)FarmLand.FarmLandStatus.NeedCultivate)
            {
                // 🎬 在此处触发曲辕犁动画
            farmland.PlayPlowAnimation(); // ✅ 调用农田的动画播放方法
            
            yield return new WaitForSeconds(plowDelay); // 等待动画完成

            // 🎯 动画完成后，改变土地状态
            farmland.Transition((int)FarmLand.FarmLandStatus.NeedSow);
            }
        }

        selectedFarmlands.Clear(); // 耕地结束后清空列表
    }
}
