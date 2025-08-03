using UnityEngine;
[CreateAssetMenu(fileName = "GiveItemAction", menuName = "Dialogue/Actions/GiveItem")]
public class GiveItemAction : DialogueAction
{
    public string itemId;
    public int amount;

    public override void Execute(DialogueContext context)
    {
        //��� AddItem � ��
        Debug.Log($"���� ������ ������� {itemId}");
    }
}
