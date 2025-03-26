using UnityEngine;
using DG.Tweening; // 需要引用 DOTween 命名空间

public class SpriteScaleDOTween : MonoBehaviour
{
    public float finalScale = 0.5f; // 控制最终大小

    private void OnEnable()
    {
        transform.localScale = Vector3.zero; // 初始大小设为 0

        Sequence waterSequence = DOTween.Sequence();
        waterSequence.Append(transform.DOScale(new Vector3(finalScale * 1.2f, finalScale * 0.8f, 1f), 0.7f))  // 先横向扩张
                     .Append(transform.DOScale(new Vector3(finalScale * 0.9f, finalScale * 1.0f, 1f), 0.8f))  // 再纵向扩张
                     .Append(transform.DOScale(Vector3.one * finalScale, 0.8f))  // 最后回到目标大小
                     .SetEase(Ease.InOutSine);
    }
}
