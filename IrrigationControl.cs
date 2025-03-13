using System.Collections;
using UnityEngine;

public class IrrigationControl : MonoBehaviour
{
    public int IrrigationDuration = 5;    // **灌溉后维持的时间**
    
    public IrrigationManager irrigations;

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
        irrigations.Status = true;
        Debug.Log("Water Wheel Irrigated!");

        // **灌溉持续一段时间**
        yield return new WaitForSeconds(IrrigationDuration);

        // **时间到，恢复未灌溉状态**
        irrigations.Status = false;
        Debug.Log("Irrigation ended.");
    }

}
