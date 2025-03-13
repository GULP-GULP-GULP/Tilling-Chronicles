using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int Target0;  // 目标种子0的数量
    public int Target1;  // 目标种子1的数量
    public int Target2;  // 目标种子2的数量
    public int Target3;  // 目标种子3的数量

    public int Yield0 = 0;  // 当前种子0的收获量
    public int Yield1 = 0;  // 当前种子1的收获量
    public int Yield2 = 0;  // 当前种子2的收获量
    public int Yield3 = 0;  // 当前种子3的收获量



    void Start()
    {
        // 获取当前关卡信息并初始化//
    }



    void Update()
    {
        // 检查收获是否达成目标
        if (Yield0 >= Target0 && Yield1 >= Target1 && Yield2 >= Target2 && Yield3 >= Target3)
        {
            Debug.Log("目标完成！");  // 目标完成时打印信息
            // 可以在这里添加其他的通关逻辑，如显示奖励、过关动画等
        }
    }
}
