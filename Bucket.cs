using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    private bool hasWater = false; // 是否装满水

    public CursorManager cursor;

    void Update()
    {
        if (cursor.isToolMode && Input.GetMouseButtonDown(0)) // 鼠标左键点击
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("River"))
                {
                    CollectWater();
                }
                else if (hit.collider.CompareTag("FarmLand"))
                {
                    WaterFarm(hit.collider.gameObject);
                }
            }
        }
    }

    void CollectWater()
    {
        hasWater = true;
        Debug.Log("水桶装满水！");
    }

    void WaterFarm(GameObject farmLandObj)
    {
        FarmLand farmLand = farmLandObj.GetComponent<FarmLand>();
        if (farmLand != null && hasWater)
        {
            if (farmLand.Status == (int)FarmLand.FarmLandStatus.NeedIrrigate)
            {
                farmLand.Transition((int)FarmLand.FarmLandStatus.Growing); // 变为可种植状态
                hasWater = false;
                Debug.Log("成功灌溉农田！");
            }
            else
            {
                Debug.Log("这个农田不需要水！");
            }
        }
    }
}
