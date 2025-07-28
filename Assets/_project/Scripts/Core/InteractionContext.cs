using UnityEngine;

public class InteractionContext
{
    public bool IsTrade {  get; set; } //������� � ������ ��������
    public bool IsDrop { get; set; } //����� �����������
    public bool IsNPC { get; set; } //�������������� � ���������� NPC
    public bool AllowStealing { get; set; } //����� �� ������� ��� �������
    public bool IsCrafting { get; set; } //������������ � ������
    public bool IsRecycler { get; set; }
    public object ExtraData { get; set; } //����� �������������� ������ (�������� ������ �� �����)

}
