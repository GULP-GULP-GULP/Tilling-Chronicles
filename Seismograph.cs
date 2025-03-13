using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seismograph : MonoBehaviour
{
    public Earthquake earthquake; // 需要绑定 Earthquake 组件
    public float warningTime = 5f; // 预警时间
    private bool warningIssued = false;

    private void Start()
    {
        // 启动一个检查地震的定时器
        StartCoroutine(SeismographRoutine());
    }

    IEnumerator SeismographRoutine()
    {
        while (true)
        {
            // 每隔一段时间检查地震
            yield return new WaitForSeconds(earthquake.randomTime - warningTime); // 减去警告时间
            if (!warningIssued)
            {
                IssueWarning(); // 发出预警
            }

            // 等待警告时间后触发地震
            yield return new WaitForSeconds(warningTime);
            earthquake.TriggerEarthquake(); // 触发地震
            warningIssued = false; // 重置警告状态
        }
    }

    void IssueWarning()
    {
        warningIssued = true;
        Debug.Log("Warning: Earthquake is coming in " + warningTime + " seconds!");
        // 可以在这里添加更多的预警效果，例如UI提示、震动等
    }
}
