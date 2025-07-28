using UnityEngine;
using MyGame.Inventory;

namespace MyGame.Core
{
    public class Character : MonoBehaviour
    {
        public Inventory.Inventory Inventory { get; private set; }
        //Здоровье, лвл, имя

        //Получение урона, смерть и тд
        //Можно наследоваться или добавлять интерфейсы
    }
}