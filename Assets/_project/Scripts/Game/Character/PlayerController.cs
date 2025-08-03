using MyGame.EventBus;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private Transform virtualCamera;

    private InputAction moveAction;
    private InputAction lookAction;

    [Header("Настройки управления")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float turnRotate = 0.1f;
    [SerializeField] private float smoothing = 10f;
    [Header("Настройки гравитации")]
    [SerializeField] private float gravityForce = -9.81f;
    [SerializeField] private float maxFallSpeed = -20f;

    private float verticalVelocity = 0f;

    private bool canMove = true;

    private float verticalAngle = 0f;
    private float horizontalAngle = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        //EventBus.Subscribe<OnInventoryOpen>(StopMovement);
        //EventBus.Subscribe<OnInventoryClose>(StartMovement);
        EventBus.Subscribe<OnChangeCharacterInput>(ToggleMovement);
    }

    private void OnDisable()
    {
        //EventBus.Unsubscribe<OnInventoryOpen>(StopMovement);
        //EventBus.Unsubscribe<OnInventoryClose>(StartMovement);
        EventBus.Unsubscribe<OnChangeCharacterInput>(ToggleMovement);
    }

    private void Update()
    {

        HandleMovement();   //Тут внутри есть проверка на то, можно ли двигаться
        if (canMove)
            HandleRotation();

    }

    void HandleMovement()   //Добавить гравитацию
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        Vector3 move = Vector3.zero; // = (transform.forward * direction.y + transform.right * direction.x).normalized * moveSpeed;
        if (canMove)
            move = (transform.forward * direction.y + transform.right * direction.x).normalized * moveSpeed;
        HandleGravity();

        move.y = verticalVelocity;
        
        characterController.Move(move * Time.deltaTime);
    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f;  // Лёгкое "прижатие" к земле
        }
        else
        {
            verticalVelocity += gravityForce * Time.deltaTime;
            verticalVelocity = Mathf.Max(verticalVelocity, maxFallSpeed); // Ограничиваем скорость падения
        }
    }

    void HandleRotation()   //Добавить сглаживание   (адекватное, а не будто пьяный)         
    {
       Vector2 lookDirection = lookAction.ReadValue<Vector2>();

        horizontalAngle += lookDirection.x * turnRotate;
        verticalAngle -= lookDirection.y * turnRotate;

        verticalAngle = Mathf.Clamp(verticalAngle, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, horizontalAngle, 0f);
        
        virtualCamera.transform.localRotation = Quaternion.Euler(verticalAngle, 0, 0f);
    }

    void StopMovement()
    {
        canMove = false;
    }

    void StartMovement()
    {
        canMove = true;
    }

    void ToggleMovement(OnChangeCharacterInput e)
    {
        canMove = e.CanMove;
    }

}
