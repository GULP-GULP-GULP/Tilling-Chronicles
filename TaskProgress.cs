using UnityEngine;
using UnityEngine.UI;

public class TaskProgress : MonoBehaviour
{
    public Slider progressBar;  // 进度条组件
    public Target targetComponent;  // 目标数据组件
    private float targetAmount;  // 目标值
    private float currentYield;  // 当前产量

    void Start()
    {
        if (progressBar != null)
        {
            progressBar.value = 0f;  // 初始化进度条
            progressBar.gameObject.SetActive(true);  // 进度条始终可见
        }

        if (targetComponent != null)
        {
            targetAmount = targetComponent.Target0;  // 获取目标值
        }
    }

    void Update()
    {
        if (targetComponent != null)
        {
            currentYield = targetComponent.Yield0;  // 获取当前产量
            float progress = Mathf.Clamp01(currentYield / targetAmount);  // 计算进度
            progressBar.value = progress;  // 更新进度条
        }
    }
}
