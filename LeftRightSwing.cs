using UnityEngine;
using DG.Tweening;

// 假设草有根部和顶部（你可以手动将草的根部和顶部分为两个子物体）
public class GrassSwing : MonoBehaviour
{
    public GameObject topPart;  // 草的顶部部分
    public GameObject bottomPart;  // 草的根部部分

    public float maxSwingAmount = 0.5f;  // 摇摆的最大幅度
    public float swingDuration = 1f;  // 每次摇摆的持续时间
    public float swingFrequency = 2f;  // 摇摆频率，值越大越快

    private void OnEnable()
    {
        // 确保根部不动，只控制顶部部分
        AnimateSwing();
    }

    private void AnimateSwing()
    {
        // 只让顶部部分摇摆
        Sequence swingSequence = DOTween.Sequence();
        swingSequence.Append(topPart.transform.DOLocalMoveX(bottomPart.transform.localPosition.x + Random.Range(-maxSwingAmount, maxSwingAmount), swingDuration).SetEase(Ease.InOutSine))
                     .SetLoops(-1, LoopType.Yoyo)  // 设置循环，左右摆动
                     .SetDelay(Random.Range(0, swingFrequency));  // 添加随机延迟
    }
}

