using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
        DestroyItemUI();
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < inventory.Container.Count; i++)
        {
            if (!itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity, transform);
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public void DestroyItemUI()
    {
        if (Input.GetButtonDown("Drop") && inventory.Container.Count == 1 && transform.childCount == 1)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void CreateDisplay()
    {
        for(int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity, transform);
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }

}
