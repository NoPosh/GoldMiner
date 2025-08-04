using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcDatabase", menuName = "Npc/NpcDatabase")]
public class NpcDatabase : ScriptableObject
{
    [Header("НПС")]
    public List<NpcData> allNpcs;

    public List<NpcData> customersNpc;

    [Header("Наборы фраз")]
    [TextArea(3,5)]public List<string> greetingsPhrases;
    [TextArea(3,5)]public List<string> goodbuyPhrases;
    [TextArea(3,5)]public List<string> learnAssortimentPhrases;
    [TextArea(3,5)]public List<string> interestPhrases;
    [TextArea(3,5)]public List<string> thanksPhrases;
    [TextArea(3,5)]public List<string> bargainPhrases;

    public NpcData GetRandomCustomer()
    {
        return customersNpc[Random.Range(0, customersNpc.Count)];
    }

    //Нужно что-то типо 
    public Dialogue GenerateRandomDialog()  //TODO: тут нужно сделать более адкватную обработку ответов и тд
    {
        Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();

        // Стартовый узел
        DialogueNode startNode = ScriptableObject.CreateInstance<DialogueNode>();
        startNode.text = greetingsPhrases[Random.Range(0, greetingsPhrases.Count)];
        startNode.choices = new List<DialogueChoice>();
        dialogue.startNode = startNode;

        // Узел "узнать ассортимент"
        DialogueNode assortimentNode = ScriptableObject.CreateInstance<DialogueNode>();
        assortimentNode.text = learnAssortimentPhrases[Random.Range(0, learnAssortimentPhrases.Count)];
        assortimentNode.choices = new List<DialogueChoice>();

        //Узел "Интерес"
        DialogueNode interestNode = ScriptableObject.CreateInstance<DialogueNode>();
        interestNode.text = interestPhrases[Random.Range(0, interestPhrases.Count)];
        interestNode.choices = new List<DialogueChoice>();

        //Узел "Благодарность"
        DialogueNode thanksNode = ScriptableObject.CreateInstance<DialogueNode>();
        thanksNode.text = thanksPhrases[Random.Range(0, thanksPhrases.Count)];
        thanksNode.choices = new List<DialogueChoice>();

        //Узел "Торговля"
        DialogueNode bargainNode = ScriptableObject.CreateInstance<DialogueNode>();
        bargainNode.text = bargainPhrases[Random.Range(0, bargainPhrases.Count)];
        bargainNode.choices = new List<DialogueChoice>();

        // Узел "прощание"
        DialogueNode goodbyeNode = ScriptableObject.CreateInstance<DialogueNode>();
        goodbyeNode.text = goodbuyPhrases[Random.Range(0, goodbuyPhrases.Count)];
        goodbyeNode.choices = new List<DialogueChoice>();

        DialogueChoice startNodeChoice1 = new DialogueChoice
        {
            text = "Привет",
            nextNode = assortimentNode
        };
        DialogueChoice startNodeChoice2 = new DialogueChoice
        {
            text = "Мы закрыты",
            nextNode = goodbyeNode
        };

        startNode.choices.Add(startNodeChoice1);
        startNode.choices.Add(startNodeChoice2);

        return dialogue;
    }
}
