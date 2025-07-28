using UnityEngine;
using MyGame.Core;

public class CharacterComponent : MonoBehaviour
{
    //��� � ���, ��� ���� � ������ Character

    public Character Character { get; private set; }

    private void Awake()
    {
        Character = new Character();   //��� ����� ������ ��� ��� �����
    }
}
