using MyGame.EventBus;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool IsInventoryOpen;
    private bool IsDialogOpen;

    private void Awake()
    {
        Instance = this;
        if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
        Services.Initialize();

        EventBus.Subscribe<OnInventoryOpen>(InventoryOpen);
        EventBus.Subscribe<OnInventoryClose>(InventoryClose);
        EventBus.Subscribe<OnDialogieStart>(DialogueStart);
        EventBus.Subscribe<OnDialogueEnd>(DialogueClose);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryOpen>(InventoryOpen);
        EventBus.Unsubscribe<OnInventoryClose>(InventoryClose);
        EventBus.Unsubscribe<OnDialogieStart>(DialogueStart);
        EventBus.Unsubscribe<OnDialogueEnd>(DialogueClose);
    }

    //Управляет тем, работает ввод персонажа или нет (тогда ввод лучше делать событиями?)

    private void InventoryOpen()
    {
        IsInventoryOpen = true;
        Cursor.lockState = CursorLockMode.None;
        EventBus.Raise<OnChangeCharacterInput>(new OnChangeCharacterInput(false));
    }
    private void InventoryClose()
    {
        IsInventoryOpen = false;
        if (!IsDialogOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            EventBus.Raise<OnChangeCharacterInput>(new OnChangeCharacterInput(true));
        }
        
    }

    private void DialogueStart()
    {
        IsDialogOpen = true;
        Cursor.lockState = CursorLockMode.None;
        EventBus.Raise<OnChangeCharacterInput>(new OnChangeCharacterInput(false));
    }
    private void DialogueClose()
    {
        IsDialogOpen = false;
        if (!IsInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            EventBus.Raise<OnChangeCharacterInput>(new OnChangeCharacterInput(true));
        }
    }
}
