using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            if(player.HasKitchenObject()) {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO())) { //verify if it is a cutable object
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            } else {

            }
        } else {
            if(player.HasKitchenObject()) {
                
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectsSO())) { // has an object and also it can be cut
            // need to cut the object
            KitchenObjectsSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectsSO());
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitcheObjectSO) {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if(cuttingRecipeSO.input == inputKitcheObjectSO) {
                return true;
            }
        }
        return false;
    }


    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO) { // for returing the right so when slicing
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if(cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
