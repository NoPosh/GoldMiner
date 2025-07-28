using UnityEngine;
using MyGame.Core;

public class CharacterComponent : MonoBehaviour
{
    //»м€ и все, что есть в классе Character

    public Character Character { get; private set; }

    private void Awake()
    {
        Character = new Character();   //“ут можно задать все что нужно
    }
}
