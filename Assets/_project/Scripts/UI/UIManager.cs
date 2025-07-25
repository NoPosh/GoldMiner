using MyGame.EventBus;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject sideInventoryPanel;

    private bool _inventoryPanelActive => _inventoryPanel.activeSelf;
    private bool _sideInventoryPanelActive => sideInventoryPanel.activeSelf;
    

    private void OnEnable()
    {
        EventBus.Subscribe<OnInventoryInteract>(InteractInventory);
        EventBus.Subscribe<OnOpenChest>(OpenSideInventory);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryOpen>(InteractInventory);
        EventBus.Unsubscribe<OnOpenChest>(OpenSideInventory);
    }

    private void Start()
    {
        CloseInventory();
        CloseSideInventory();
    }

    private void InteractInventory()
    {
        if (_inventoryPanelActive) CloseInventory();
        else OpenInventory();
    }

    private void OpenInventory()
    {
        Cursor.lockState = CursorLockMode.None;
        _inventoryPanel.SetActive(true);
        EventBus.Raise<OnInventoryOpen>(new OnInventoryOpen());
    }

    private void CloseInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _inventoryPanel.SetActive(false);
        EventBus.Raise<OnInventoryClose>(new OnInventoryClose());

        if (sideInventoryPanel) CloseSideInventory();
    }

    private void OpenSideInventory(OnOpenChest e)
    {
        OpenInventory();
        sideInventoryPanel.SetActive(true);
    }

    private void CloseSideInventory()
    {
        sideInventoryPanel.SetActive(false);
    }
}
