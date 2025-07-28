using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorComponent : MonoBehaviour
{
    [SerializeField] float interactionDistance = 3.0f;
    [SerializeField, Tooltip("Ширина вылетаемого луча")] float interationRadius = 0.2f;
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

        if (interactAction.triggered)
        {
            if (currentInteractable != null) currentInteractable.Interact(gameObject);
        }    
    }

    private void FindTarget()
    {
        currentInteractable = null;

        if (Physics.SphereCast(playerCamera.transform.position, interationRadius, playerCamera.transform.forward, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
                //Подсветка, подсказка
                //Debug.Log("Навелись на " + hit.collider.gameObject.name);
            }
        }
    }


}
