using UnityEngine;
using UnityEngine.UI;

public class TechTreeUI : MonoBehaviour
{
    public static TechTreeUI Instance; // 单例

    public GameObject confirmPanel;  
    public Text confirmText;
    public Button confirmButton;
    public Button cancelButton;
    public GameObject techTreePanel; 
    public GameObject errorPanel; // 新增错误弹窗
    public Text errorText; // 错误弹窗文字
    public Button errorCloseButton; // 关闭错误弹窗按钮

    private TechNodeButton currentSelectedButton; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        techTreePanel.SetActive(false);
        confirmPanel.SetActive(false);
        errorPanel.SetActive(false); // 初始隐藏错误弹窗

        confirmButton.onClick.AddListener(ConfirmUnlock);
        cancelButton.onClick.AddListener(CancelUnlock);
        errorCloseButton.onClick.AddListener(CloseErrorPanel);
    }

    public void OpenTechTree()
    {
        techTreePanel.SetActive(true);
    }

    public void CloseTechTree()
    {
        techTreePanel.SetActive(false);
    } 

    public void ShowConfirmPanel(TechNodeButton button)
    {
        if (button.isUnlocked) return; // 已解锁就不弹窗

        if (!button.CanUnlock()) // **检查是否符合解锁条件**
        {
            ShowErrorPanel("无法解锁，前置科技未解锁！");
            return;
        }

        currentSelectedButton = button;
        confirmText.text = $"是否解锁科技: {button.techName} ?";
        confirmPanel.SetActive(true);
        confirmPanel.transform.position = button.transform.position + new Vector3(0, -50, 0);
    }

    void ConfirmUnlock()
    {
        if (currentSelectedButton != null)
        {
            currentSelectedButton.Unlock();
            confirmPanel.SetActive(false);
        }
    }

    void CancelUnlock()
    {
        confirmPanel.SetActive(false);
    }

    // **显示错误弹窗**
    public void ShowErrorPanel(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }

    // **关闭错误弹窗**
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }
}
