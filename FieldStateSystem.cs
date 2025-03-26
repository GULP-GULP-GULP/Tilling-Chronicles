using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldStateSystem : MonoBehaviour
{
    public FarmLand farmLand;
    public Sprite DefaultSprite;
public Sprite CultivatedSprite;

private SpriteRenderer spriteRenderer;

void Start()
{
    spriteRenderer = GetComponent<SpriteRenderer>();
    UpdateFieldAppearance();
}

    void Update()
    {
        UpdateFieldAppearance(); // 每帧检查状态并更新
    }

void UpdateFieldAppearance()
{
    if (farmLand == null || spriteRenderer == null) return;

    switch (farmLand.Status)
    {
        case (int)FarmLand.FarmLandStatus.NeedCultivate:
            spriteRenderer.sprite = DefaultSprite;
            break;
        case (int)FarmLand.FarmLandStatus.NeedSow:
            spriteRenderer.sprite = CultivatedSprite;
            break;
        case (int)FarmLand.FarmLandStatus.Growing:
            spriteRenderer.sprite = CultivatedSprite;
            break;
        case (int)FarmLand.FarmLandStatus.NeedHarvest:
            spriteRenderer.sprite = CultivatedSprite;
            break;
        default:
            spriteRenderer.sprite = DefaultSprite;
            break;
    }
}

}
