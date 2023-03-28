using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public static ContainerCounter Instance { get; private set; }

    public event EventHandler OnPlayerGrabbedObject;
    public event EventHandler OnPlayerGrabbedObjectCarryingPlate;



    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Player is carrying something
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // Player is holding a Plate
                if (plateKitchenObject.TryAddIngredient(kitchenObjectSO))
                {
                    OnPlayerGrabbedObjectCarryingPlate?.Invoke(this, EventArgs.Empty);
                }

            }
        }

    }


}
