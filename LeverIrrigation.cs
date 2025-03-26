using UnityEngine;
using System.Collections.Generic;

public class LeverIrrigation : MonoBehaviour
{
    public Animator leverAnimator;  // 桔槔的动画控制器
    public List<IrrigationControl> irrigationControls = new List<IrrigationControl>(); // 关联的灌溉系统列表
    public bool isAnimating = false; // 是否正在播放动画
    public int Duration = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!isAnimating && IsMouseOverLever(mousePosition))
            {
                StartLeverAnimation();
            }
        }
    }

    private void StartLeverAnimation()
    {
        isAnimating = true;
        leverAnimator.SetTrigger("StartLever"); // 触发动画
    }

    // **动画结束后调用（在动画结束时的Event里触发）**
    public void OnLeverAnimationComplete()
    {
        foreach (var irrigationControl in irrigationControls)
        {
            irrigationControl.Irrigation(); // 开始灌溉
        }
        isAnimating = false; // 允许再次点击
    }

    // 判断鼠标是否点击在桔槔上
    private bool IsMouseOverLever(Vector2 mousePosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        return collider.OverlapPoint(mousePosition);
    }
}
