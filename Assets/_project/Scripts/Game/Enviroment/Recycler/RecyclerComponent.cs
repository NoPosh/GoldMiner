using UnityEngine;
using MyGame.Core;
using MyGame.EventBus;

public class RecyclerComponent : MonoBehaviour, IInteractable
{
    [SerializeField] private int inputSize = 5;
    [SerializeField] private int outputSize = 5;
    [SerializeField] private RecycleRecipeDatabase recipeDatabase;

    public InventoryComponent InputInventory {  get; private set; }
    public InventoryComponent OutputInventory { get; private set; }
    public Recycler Recycler { get; private set; }

    private void Awake()
    {
        var recyclingServise = new RecyclingService(recipeDatabase);
        Recycler = new Recycler(inputSize, outputSize, recyclingServise, recipeDatabase);

        InputInventory = gameObject.AddComponent<InventoryComponent>();
        OutputInventory = gameObject.AddComponent<InventoryComponent>();

        InputInventory.Initialize(Recycler.InputInventory);
        OutputInventory.Initialize(Recycler.OutputInventory);

        Recycler.OnStartRecycle += () => Debug.Log("������ ��������������");
        Recycler.OnStopRecycle += () => Debug.Log("��������� ��������������");
    }

    private void Update()
    {
        Recycler.Update(Time.deltaTime);
    }

    public void Interact(GameObject interactor)
    {
        EventBus.Raise<OnInteractRecycle>(new OnInteractRecycle(interactor.GetComponent<CharacterComponent>().Inventory, this));        
    }



    //+ ������� UI ������, ������� ������ �����; ���������/������������� �������
    public void StartProcess()
    {
        Debug.Log(OutputInventory.Inventory.GetFirstEmptyCell().index);
        Recycler.StartProcessing();

    }
    public void StopProcess() => Recycler.StopProcessing();
}
