using UnityEngine;
[CreateAssetMenu(fileName = "GiveItemAction", menuName = "Dialogue/Actions/GiveItem")]
public class GiveItemAction : DialogueAction
{
    public string itemId;
    public int amount;

    public override void Execute(DialogueContext context)
    {
        //Тут AddItem и тд
        Debug.Log($"Типо выдали предмет {itemId}");
    }
}
