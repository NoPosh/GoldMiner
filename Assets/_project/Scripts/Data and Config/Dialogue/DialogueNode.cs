using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/DialogueNode")]
public class DialogueNode : ScriptableObject
{
    [TextArea(3, 10)]
    public string text;

    public List<DialogueChoice> choices;

    [Tooltip("Если нет выбора - диалог завершается")]
    public List<DialogueAction> onEndAction;
}

[System.Serializable]
public class DialogueChoice
{
    [TextArea(3, 10)]
    public string text;
    public DialogueNode nextNode;

    public List<DialogueAction> onEndAction;
}
