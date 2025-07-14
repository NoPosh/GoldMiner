using UnityEngine;

public class MovementToggle : MonoBehaviour
{
   public PlayerController controller;

    public void ToggleMovement()
    {
        controller.isMoving = !controller.isMoving;
    }
}
