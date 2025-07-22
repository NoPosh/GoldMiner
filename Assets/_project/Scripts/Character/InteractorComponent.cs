using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorComponent : MonoBehaviour
{
    [SerializeField] float interactionDistance = 3.0f;
    [SerializeField] LayerMask interactableLayer; //Слой для предметов
    private CinemachineCamera playerCamera;

    private IInteractable currentInteractable;
    private InputAction interactAction;

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        playerCamera = GetComponentInChildren<CinemachineCamera>();
        
    }

    private void Update()
    {
        FindTarget();

        if (interactAction.IsPressed())
        {
            if (currentInteractable != null) currentInteractable.Interact(gameObject);
        }    
    }

    private void FindTarget()
    {
        currentInteractable = null;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
                //Подсветка, подсказка
                Debug.Log("Навелись на " + hit.collider.gameObject.name);
            }
        }
    }


}
