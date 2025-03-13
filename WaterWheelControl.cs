using UnityEngine;

public class WaterWheelControl : MonoBehaviour
{
    private bool isDragging = false; // 是否正在拖动水车
    private Vector2 lastMousePosition; // 上次鼠标位置
    private float totalRotation = 0f; // 累计旋转角度，用于判断是否完成两圈
    private float rotationThreshold = 720f; // 旋转两圈的阈值（720度）
    public float rotationSpeed = 200f; // 水车旋转的速度
    public Animator waterWheelAnimator; // 水车的动画控制器
    public IrrigationControl IrrigationControl;

    private bool isWatering = false; // 标记灌溉是否开始
    private float irrigationTimer = 0f; // 用于控制灌溉的计时器
    public float irrigationDuration = 5f; // 持续灌溉的时间

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
            transform.Rotate(Vector3.forward, angle * rotationSpeed);

            // 检查是否旋转了两圈（720度）
            if (totalRotation >= rotationThreshold && !isWatering)
            {
                StartWatering(); // 开始灌溉
                isDragging = false; // 停止拖动
            }

            lastMousePosition = mousePosition; // 更新鼠标位置
        }

        // 如果正在灌溉，控制灌溉持续时间
        if (isWatering)
        {
            irrigationTimer += Time.deltaTime; // 累加灌溉时间
            if (irrigationTimer >= irrigationDuration)
            {
                StopWatering(); // 时间到，停止灌溉
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
        if (waterWheelAnimator != null)
        {
            waterWheelAnimator.SetTrigger("StartWatering");
        }

        // 开始灌溉的逻辑
        IrrigationControl.Irrigation();

        // 标记灌溉开始
        isWatering = true;
        irrigationTimer = 0f; // 重置计时器
    }

    // 停止灌溉
    private void StopWatering()
    {
        // 停止灌溉的动画（减速动画可以在Animator中设置）
        if (waterWheelAnimator != null)
        {
            waterWheelAnimator.SetTrigger("StopWatering");
        }

        // 重置灌溉状态
        isWatering = false;
    }

    void Awaken()
    {
        IrrigationControl.IrrigationDuration = 5;
    }
}
