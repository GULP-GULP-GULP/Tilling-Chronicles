using UnityEngine;

public class LeverIrrigation : MonoBehaviour
{
    public Animator leverAnimator;  // 桔槔的动画控制器
    public IrrigationControl IrrigationControl;   // 关联的灌溉系统
    private bool isAnimating = false; // 是否正在播放动画

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键点击
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!isAnimating && IsMouseOverLever(mousePosition)) // 只在未播放动画时触发
            {
                StartLeverAnimation();
            }
        }
    }

    private void StartLeverAnimation()
    {
        isAnimating = true;
        if (leverAnimator != null)
        {
            leverAnimator.SetTrigger("StartLever"); // 触发动画
        }
    }

    // **动画结束后调用（在动画结束时的Event里触发）**
    public void OnLeverAnimationComplete()
    {
        IrrigationControl.Irrigation(); // 开始灌溉
        isAnimating = false; // 允许再次点击
    }

    // 判断鼠标是否点击在桔槔上
    private bool IsMouseOverLever(Vector2 mousePosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        return collider.OverlapPoint(mousePosition);
    }

    void Awaken()
    {
        IrrigationControl.IrrigationDuration = 2;
    }
}
