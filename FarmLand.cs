using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLand : MonoBehaviour
{
    public int Status;  // 当前农田状态
    public int Yield;  // 农田产量
    public int SeedKind = -1; // 当前种子种类
    public GameObject highlightOverlay;

    private Collider itemCollider;

    // 状态转换方法
    public void Transition(int AfterTransition)
    {
        Status = AfterTransition;  // 更新农田状态
        Debug.Log($"FarmLand status changed to: {(FarmLand.FarmLandStatus)Status}");
    }

    // 农田状态枚举
    public enum FarmLandStatus
    {
        NeedCultivate = 0,  // 需要开垦
        NeedSow = 1,        // 需要播种
        NeedIrrigate = 2,   // 需要灌溉
        Growing = 3,        // 正在生长
        NeedHarvest = 4     // 需要收获
    }

    public enum SeedKinds
    {
        First = 0,  // 第一种种子
        Second = 1, // 第二种种子
        Third = 2,  // 第三种种子
        Forth = 3   // 第四种种子
    }
    public void SetUnavailable()
    {
        itemCollider.enabled = false;
        Status = 0;
        Yield = 0;
        SeedKind = -1;
    // 可添加视觉效果，比如变灰
    }

    public void PlayPlowAnimation()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
           animator.SetTrigger("Plow"); // ✅ 触发耕地动画
        }
    }

    public void SetAvailable()
    {
        itemCollider.enabled = true; // 设为可用
    // 视觉效果恢复正常
    }

    public void UpdateHighlight(bool isActive)
    {
        highlightOverlay.SetActive(isActive);
    }


    // Start is called before the first frame update
    void Start()
    {
        Status = (int)FarmLandStatus.NeedCultivate;  // 初始化状态为需要开垦
        itemCollider = GetComponent<Collider>();
        highlightOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 可在此添加额外逻辑，例如显示状态更新等
    }
}
