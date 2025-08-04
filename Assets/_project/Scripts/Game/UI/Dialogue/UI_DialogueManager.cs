using MyGame.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DialogueManager : MonoBehaviour
{
    //Когда начинается диалог - выводим UI, отключаем инпут
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text NpcNameText;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private Transform answerParent;
    [SerializeField] private Button answerPrefab;

    private void OnEnable()
    {
        EventBus.Subscribe<OnDialogieStart>(StartDialogue);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<OnDialogieStart>(StartDialogue);
    }

    private void StartDialogue(OnDialogieStart e)
    {
        if (!e.context.IsCustomer)
        {
            NextNode(e.dialogue.startNode, e.context);            
        }
        else
        {
            NextNode(Services.CreatingNpcService.GetRandomDialogue().startNode, e.context);
        }
        dialoguePanel.SetActive(true);
    }

    private void NextNode(DialogueNode node, DialogueContext context)
    {
        foreach (Transform t in answerParent)
        {
            t.GetComponent<Button>()?.onClick.RemoveAllListeners();
            Destroy(t.gameObject);
        }

        DialogueText.text = node.text;

        foreach (var choice in node.choices)
        {
            Button answerButton = Instantiate(answerPrefab, answerParent);
            answerButton.GetComponentInChildren<TMP_Text>().text = choice.text;
            if (choice.onEndAction != null)
            foreach (var action in choice.onEndAction)
            {
                answerButton.onClick.AddListener(() => action.Execute(context));
            }
            if (choice.nextNode != null)
            {
                answerButton.onClick.AddListener(() => NextNode(choice.nextNode, context));
            }
            else
            {
                answerButton.onClick.AddListener(EndDialogue);
                foreach (var action in node.onEndAction)
                {
                    answerButton.onClick.AddListener(() => action.Execute(context));
                }
            }
        }
        if (node.choices.Count == 0)
        {
            //Добавляем кнопку, завершающую диалог
            Button answerButton = Instantiate(answerPrefab, answerParent);
            answerButton.GetComponentInChildren<TMP_Text>().text = "Бывай";
            answerButton.onClick.AddListener(EndDialogue);
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        EventBus.Raise<OnDialogueEnd>(new OnDialogueEnd());

    }
}
