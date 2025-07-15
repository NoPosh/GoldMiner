using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;

    public float turnSpeed = 100f;
    public float maxTurnSpeed = 1f;
    [Tooltip("–ассто€ние при максимальной скорости поворота в пиксел€х")]
    public float maxDistanceForTurn = 200f;

    public bool isMoving = false;

    private int leftTouchFingerId = -1;
    private float previousTochX;
    private float startTochX;
    

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
    }

    //ѕока зажато, нужно свер€ть начальной точкой нажати€,
    //и пока не отпущено не переставать поворачивать,
    //≈сли палец дальше половины экрана, тоже не перестовать
    void HandleRotation()           
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2)
        {
            startTochX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            if (startTochX != 0)
            {
                float deltaX = Input.mousePosition.x - startTochX;
                float deltaRotate = Mathf.Sign(deltaX);
                float distance = Mathf.Abs(deltaX);

                float t = Mathf.Clamp01(distance / maxDistanceForTurn);
                // ѕолучаем плавную скорость поворота
                float smoothRotate = Mathf.Lerp(0, maxTurnSpeed, t);

                transform.Rotate(Vector3.up, deltaRotate * smoothRotate * turnSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTochX = 0;
        }

#else   //ѕеределать управление как на компе
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
            {
                leftTouchFingerId = touch.fingerId;
                previousTochX = touch.position.x;
            }
            else if (touch.fingerId == leftTouchFingerId)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    float deltaX = touch.position.x - previousTochX;
                    transform.Rotate(Vector3.up, deltaX * turnSpeed * Time.deltaTime / 10f);
                    previousTochX = touch.position.x;
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                leftTouchFingerId = -1;
            }
        }
#endif
    }
}
