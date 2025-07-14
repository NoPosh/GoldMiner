using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float turnSpeed = 100f;
    public bool isMoving = false;

    private int leftTouchFingerId = -1;
    private float previousTochX;

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
    
    void HandleRotation()           //Пока зажато, нужно сверять начальной точкой нажатия, и пока не отпущено не переставать поворачивать, если палец дальше половины экрана, тоже не перестовать
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2)
        {
            previousTochX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                float deltaX = Input.mousePosition.x - previousTochX;
                transform.Rotate(Vector3.up, deltaX * turnSpeed * Time.deltaTime / 10f);
                previousTochX = Input.mousePosition.x;
            }
        }

#else
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
