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

    private void OnEnable()
    {
        EventBus.Subscribe<OnInventoryInteract>(InteractInventory);
        EventBus.Subscribe<OnOpenChest>(OpenSideInventory);
        EventBus.Subscribe<OnOpenRecycle>(OpenSideInventory);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryOpen>(InteractInventory);
        EventBus.Unsubscribe<OnOpenChest>(OpenSideInventory);
        EventBus.Unsubscribe<OnOpenRecycle>(OpenSideInventory);
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
    private void OpenSideInventory(OnOpenRecycle e)
    {
        OpenInventory();
        //Панелька перераба открывается в другом месте. Почему?
    }

    private void CloseSideInventory()
    {
        sideInventoryPanel.SetActive(false);
    }
}
