using MyGame.EventBus;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject sideInventoryPanel;

    public bool _inventoryPanelActive => _inventoryPanel.activeSelf;
    public bool _sideInventoryPanelActive => sideInventoryPanel.activeSelf;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CloseInventory();
    }

    private void InteractInventory()
    {
        if (_inventoryPanelActive) CloseInventory();
        else OpenInventory();
    }

    private void OpenInventory()
    {
        //Cursor.lockState = CursorLockMode.None;
        _inventoryPanel.SetActive(true);
        
    }

    private void CloseInventory()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        _inventoryPanel.SetActive(false);
        EventBus.Raise<OnInventoryClose>(new OnInventoryClose());

        if (sideInventoryPanel) CloseSideInventory();
    }

    private void CloseSideInventory()
    {
        sideInventoryPanel.SetActive(false);
    }
}
