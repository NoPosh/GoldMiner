using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private Transform virtualCamera;

    private InputAction moveAction;
    private InputAction lookAction;

    [Header("��������� ����������")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float turnRotate = 0.1f;
    [SerializeField] private float smoothing = 10f;
    [Header("��������� ����������")]
    [SerializeField] private float gravityForce = -9.81f;
    [SerializeField] private float maxFallSpeed = -20f;

    private float verticalVelocity = 0f;


    private float verticalAngle = 0f;
    private float horizontalAngle = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()   //�������� ����������
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();        

        Vector3 move = (transform.forward * direction.y + transform.right * direction.x).normalized * moveSpeed;

        HandleGravity();

        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);  //����� ����� �� ���� ��� ������ ���� �������
    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f;  // ˸���� "��������" � �����
        }
        else
        {
            Debug.Log("������");
            verticalVelocity += gravityForce * Time.deltaTime;
            verticalVelocity = Mathf.Max(verticalVelocity, maxFallSpeed); // ������������ �������� �������
        }
    }

    void HandleRotation()   //�������� �����������   (����������, � �� ����� ������)         
    {
       Vector2 lookDirection = lookAction.ReadValue<Vector2>();

        horizontalAngle += lookDirection.x * turnRotate;
        verticalAngle -= lookDirection.y * turnRotate;

        verticalAngle = Mathf.Clamp(verticalAngle, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, horizontalAngle, 0f);
        //Quaternion targetRotation = Quaternion.Euler(0f, horizontalAngle, 0f); 
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothing);

        //Quaternion cameraTargtetRotation = Quaternion.Euler(verticalAngle, 0f, 0f);
        //virtualCamera.localRotation = Quaternion.Slerp(virtualCamera.localRotation, cameraTargtetRotation, Time.deltaTime * smoothing);
        virtualCamera.transform.localRotation = Quaternion.Euler(verticalAngle, 0, 0f);
    }
}
