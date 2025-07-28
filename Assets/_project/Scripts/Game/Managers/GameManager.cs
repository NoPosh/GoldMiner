using UnityEngine;

namespace MyGame.Unity.Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Services.Initialize();
        }
    }
}