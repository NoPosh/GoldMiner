using UnityEngine;
using MyGame.EventBus;

public class Recycler : MonoBehaviour, IInteractable
{
    //Два инвентаря?
    //Как в расте
    //Сверху положить в инвентарь, снизу результат
    [SerializeField]private RecyclerInventory inputInventory;    
    [SerializeField]private RecyclerInventory outputInventory;

    public RecyclerInventory InputInventory { get { return inputInventory; } }
    public RecyclerInventory OutputInventory { get { return outputInventory; } }
    //Кнопка старт/стоп переработки


    public void Interact(GameObject interactor)
    {
        //Открывает инвентарь
        Debug.Log("Взаимодействие с переработчиком");
        EventBus.Raise<OnOpenRecycle>(new OnOpenRecycle(this));
    }

    public void StartRecycle()
    {
        //Начинает переработку 
        //Сейчас это: 
        //1. Ищет породу в первом инвентаре
        //2. Обрабатывает ее какое-то время
        //3. Добавляет предмет "золото" в каком-то кол-ве во второй инвентарь
        //4. Если больше нечего перерабатывать, то выключается
    }
}
