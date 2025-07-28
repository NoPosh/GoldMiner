using UnityEngine;
using MyGame.EventBus;
using MyGame.Inventory;
using System;

public class ItemDropManager : MonoBehaviour
{
    //������� ������� OnItemDrop � ����������� �������, ������� ����
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void DropObject(OnItemDropped e)
    {
        var dropped = Instantiate(e.obj, e.dropPoint.position, Quaternion.identity);

        //dropped.GetComponent<WorldItem>().amount = cell.amount;

        if (dropped.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(e.dropPoint.forward * e.force, ForceMode.Impulse);
        }
    }
}
