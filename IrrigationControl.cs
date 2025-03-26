using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class IrrigationControl : MonoBehaviour
{
    public int IrrigationDuration = 10;    // **灌溉后维持的时间**
    
    public GameObject River;  

    public bool Status = false;

    public List<FarmLand> farmLands;

    // **激活灌溉功能**
    public void Irrigation()
    {
        Debug.Log("开始灌溉");
        StartCoroutine(Irrigate());
    }

    // **实现灌溉过程的协程**
    IEnumerator Irrigate()
    {
        // **等待灌溉时间**

        // **更新状态为灌溉**
        Status = true;
        River.SetActive(true);
        Debug.Log("Star Irrigated!");

        // **灌溉持续一段时间**
        yield return new WaitForSeconds(IrrigationDuration);

        // **时间到，恢复未灌溉状态**
        Status = false;
        River.SetActive(false);
        Debug.Log("Irrigation ended.");
    }

    void Update()
    {
        foreach (var farmland in farmLands)
        {
            if(Status==true)
            {
                farmland.NeedIrrigate = false;
            }
            else
            {
                farmland.NeedIrrigate = true;
            }
        }
    }

}
