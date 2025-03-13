using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RiverLevelUp : MonoBehaviour
{
    public Button Button1; // 存放所有等级的工具按钮
    public Button Button2; // 存放所有等级的工具按钮
    public GameObject River1;
    public GameObject River2;
    public CursorManager CursorManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateToolUI()
    {
        CursorManager.DeactivateToolMode();
        Button1.gameObject.SetActive(false);
        River1.SetActive(false);
        River2.SetActive(true);
        Button2.gameObject.SetActive(false);
    }
}
