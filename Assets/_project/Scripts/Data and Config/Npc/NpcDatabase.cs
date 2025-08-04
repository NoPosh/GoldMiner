using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcDatabase", menuName = "Npc/NpcDatabase")]
public class NpcDatabase : ScriptableObject
{
    [Header("���")]
    public List<NpcData> allNpcs;

    public List<NpcData> customersNpc;

    [Header("������ ����")]
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

    //����� ���-�� ���� 
    public Dialogue GenerateRandomDialog()  //TODO: ��� ����� ������� ����� ��������� ��������� ������� � ��
    {
        Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();

        // ��������� ����
        DialogueNode startNode = ScriptableObject.CreateInstance<DialogueNode>();
        startNode.text = greetingsPhrases[Random.Range(0, greetingsPhrases.Count)];
        startNode.choices = new List<DialogueChoice>();
        dialogue.startNode = startNode;

        // ���� "������ �����������"
        DialogueNode assortimentNode = ScriptableObject.CreateInstance<DialogueNode>();
        assortimentNode.text = learnAssortimentPhrases[Random.Range(0, learnAssortimentPhrases.Count)];
        assortimentNode.choices = new List<DialogueChoice>();

        //���� "�������"
        DialogueNode interestNode = ScriptableObject.CreateInstance<DialogueNode>();
        interestNode.text = interestPhrases[Random.Range(0, interestPhrases.Count)];
        interestNode.choices = new List<DialogueChoice>();

        //���� "�������������"
        DialogueNode thanksNode = ScriptableObject.CreateInstance<DialogueNode>();
        thanksNode.text = thanksPhrases[Random.Range(0, thanksPhrases.Count)];
        thanksNode.choices = new List<DialogueChoice>();

        //���� "��������"
        DialogueNode bargainNode = ScriptableObject.CreateInstance<DialogueNode>();
        bargainNode.text = bargainPhrases[Random.Range(0, bargainPhrases.Count)];
        bargainNode.choices = new List<DialogueChoice>();

        // ���� "��������"
        DialogueNode goodbyeNode = ScriptableObject.CreateInstance<DialogueNode>();
        goodbyeNode.text = goodbuyPhrases[Random.Range(0, goodbuyPhrases.Count)];
        goodbyeNode.choices = new List<DialogueChoice>();

        DialogueChoice startNodeChoice1 = new DialogueChoice
        {
            text = "������",
            nextNode = assortimentNode
        };
        DialogueChoice startNodeChoice2 = new DialogueChoice
        {
            text = "�� �������",
            nextNode = goodbyeNode
        };

        startNode.choices.Add(startNodeChoice1);
        startNode.choices.Add(startNodeChoice2);

        return dialogue;
    }
}
