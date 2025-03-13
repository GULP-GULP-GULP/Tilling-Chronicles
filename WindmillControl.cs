using UnityEngine;
using System.Collections;

public class WindmillControl : MonoBehaviour
{
    public float minWaitTime = 3f;  // 最小等待时间
    public float maxWaitTime = 8f;  // 最大等待时间
    public float rotationDuration = 5f;  // 旋转持续时间
    public float rotationSpeed = 200f; // 风车旋转速度
    public Animator windmillAnimator;  // 风车动画
    public IrrigationControl IrrigationControl;  // 关联的灌溉系统
    public IrrigationManager irrigations;


    private bool isRotating = false; // 旋转状态

    void Start()
    {
        StartCoroutine(WindmillLoop()); // 启动风车控制循环
    }

    private IEnumerator WindmillLoop()
    {
        while (true)
        {
            // 等待随机时间
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // 开始旋转
            StartRotation();

            // 旋转持续一段时间
            yield return new WaitForSeconds(rotationDuration);

            // 停止旋转
            StopRotation();
        }
    }

    private void StartRotation()
    {
        isRotating = true;
        if (windmillAnimator != null)
        {
            windmillAnimator.SetBool("IsRotating", true);
        }
        IrrigationControl.Irrigation(); // 开始灌溉
        irrigations.Status = true;
    }

    private void StopRotation()
    {
        isRotating = false;
        if (windmillAnimator != null)
        {
            windmillAnimator.SetBool("IsRotating", false);
            irrigations.Status = false;
        }
    }

    void Update()
    {
        if (isRotating)
        {
            // 持续旋转风车
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
