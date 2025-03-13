using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // 用于随机选择

public class Earthquake : MonoBehaviour
{
    public List<FarmLand> farmLands; // 所有农田
    public int minAffectedLands = 2; // 最小受影响农田数
    public int maxAffectedLands = 5; // 最大受影响农田数
    public float restoreTime = 30f; // 受损农田恢复时间

    public Seismograph seismograph;
    public Protection protection;

    public float timer;
    public float randomTime;

    public void TriggerEarthquake()
    {
        StartCoroutine(EarthquakeRoutine());
    }

    IEnumerator EarthquakeRoutine()
    {
        Debug.Log("Earthquake is coming");
        yield return new WaitForSeconds(2);
        Debug.Log("Earthquake!");
        
        // 确保农田列表不为空
        if (farmLands.Count > 0)
        {
            int affectedCount = Random.Range(minAffectedLands, Mathf.Min(maxAffectedLands, farmLands.Count));
            affectedCount -= protection.GetProtectionLevel();
            List<FarmLand> affectedLands = farmLands.OrderBy(x => Random.value).Take(affectedCount).ToList();
            
            foreach (var farmLand in affectedLands)
            {
                farmLand.SetUnavailable(); // 设置农田不可用
                Debug.Log("FarmLand is now unavailable");
                StartCoroutine(RestoreFarmLand(farmLand)); // 启动恢复机制
            }
        }
    }

    IEnumerator RestoreFarmLand(FarmLand farmLand)
    {
        yield return new WaitForSeconds(restoreTime);
        farmLand.SetAvailable(); // 恢复农田可用
        Debug.Log("FarmLand restored");
    }

    void ResetRandomTime()
    {
        timer = 0f;
        randomTime = Random.Range(90f, 180f);
    }

    void Start()
    {
        ResetRandomTime();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= randomTime)
        {
            Debug.Log("随机地震事件触发！");
            TriggerEarthquake(); 
            ResetRandomTime();
        }
    }
}
