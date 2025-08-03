using UnityEngine;

[System.Serializable]

public abstract class DialogueAction : ScriptableObject, IDialogueAction
{
    public abstract void Execute(DialogueContext c);
    //Переопределяем с тем, что нам надо
}
