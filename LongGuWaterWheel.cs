using UnityEngine;

public class LongGus : MonoBehaviour
{
    private bool isDragging = false; // 是否正在拖动水车
    private Vector2 lastMousePosition; // 上次鼠标位置
    private float totalRotation = 0f; // 累计旋转角度
    private float rotationThreshold = 360f; // 一圈的阈值（360度）
    private float irrigationDuration = 3f; // 完成一圈后的灌溉持续时间（3秒）
    private float irrigationTimer = 0f; // 用于计时灌溉持续时间
    private bool isWatering = false; // 当前是否在灌溉
    public Animator longGuWaterWheelAnimator; // 水车的动画控制器
    public IrrigationControl IrrigationControl; // 水车对象

    void Update()
    {
        // 鼠标按下开始拖动水车
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverWaterWheel(mousePosition)) // 判断是否点击在水车上
            {
                isDragging = true;
                lastMousePosition = mousePosition; // 记录鼠标初始位置
            }
        }

        // 鼠标按住并拖动水车
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = CalculateRotationAngle(mousePosition); // 计算鼠标拖动的角度
            totalRotation += angle; // 累加旋转角度

            // 旋转水车
            transform.Rotate(Vector3.forward, angle);

            // 检查是否旋转了一圈
            if (totalRotation >= rotationThreshold)
            {
                StartWatering(); // 开始灌溉
                totalRotation = 0f; // 重置累计角度
            }

            lastMousePosition = mousePosition; // 更新鼠标位置
        }

        // 如果当前正在灌溉，则更新灌溉计时器
        if (isWatering)
        {
            irrigationTimer += Time.deltaTime;

            // 如果灌溉时间已过，停止灌溉
            if (irrigationTimer >= irrigationDuration)
            {
                StopWatering();
            }
        }
    }

    // 判断鼠标是否点击在水车上
    private bool IsMouseOverWaterWheel(Vector2 mousePosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        return collider.OverlapPoint(mousePosition);
    }

    // 计算鼠标拖动的角度（基于水车的中心点）
    private float CalculateRotationAngle(Vector2 mousePosition)
    {
        // 计算鼠标当前位置与上次位置的方向
        Vector2 direction = mousePosition - lastMousePosition;
        // 计算出鼠标移动的角度（相对）
        float angle = Vector2.SignedAngle(lastMousePosition, mousePosition);
        return angle;
    }

    // 开始灌溉
    private void StartWatering()
    {
        // 播放水车灌溉动画
        if (longGuWaterWheelAnimator != null)
        {
            longGuWaterWheelAnimator.SetTrigger("StartWatering");
        }

        // 开始灌溉的逻辑
        IrrigationControl.Irrigation();

        // 设置灌溉状态
        isWatering = true;
        irrigationTimer = 0f; // 重置计时器
    }

    // 停止灌溉
    private void StopWatering()
    {
        // 播放水车停止灌溉的动画
        if (longGuWaterWheelAnimator != null)
        {
            longGuWaterWheelAnimator.SetTrigger("StopWatering");
        }

        // 设置灌溉状态为停止
        isWatering = false;
    }

    void Awaken()
    {
        IrrigationControl.IrrigationDuration = 3;
    }
}
