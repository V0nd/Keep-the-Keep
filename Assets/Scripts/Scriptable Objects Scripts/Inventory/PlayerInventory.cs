using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    private void Update()
    {
        Drop();
    }

    private void Drop()
    {
        if (Input.GetButtonDown("Drop") && inventory.Container.Count == 1)
        {
            inventory.Container.Clear();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
