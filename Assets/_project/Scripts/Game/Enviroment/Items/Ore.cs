using UnityEngine;
using MyGame.EventBus;

public class Ore : WorldItem
{    
    public int Potential {  get; private set; }
    public void Init(OreData data, int potential)
    {
        item = data;
        Potential = potential;
    }

    public override void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent(out InventoryComponent inventory))
        {
            EventBus.Raise(new ItemPickupAttemptEvent()
            {
                picker = interactor,
                item = this.item,
                amount = this.amount,
                onResult = (success) =>
                {
                    if (success)
                    {
                        EventBus.Raise<OnOreCollectedGloabal>(new OnOreCollectedGloabal(this));
                        Destroy(gameObject);
                    }
                    else
                        Debug.Log("Не получилось взять этот предмет");
                }
            });
        }
    }
}
