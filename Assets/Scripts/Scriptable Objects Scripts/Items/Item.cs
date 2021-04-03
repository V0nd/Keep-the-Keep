using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject itemObject;
    public Machine oven;
    public Machine engine;
    public InventoryObject inventory;
    public GameObject questionMark;

    public int itemCount;
    public bool dropping = false;

    private bool grabAllowed;
    private SpriteRenderer spriteRenderer;
    public AudioManager audioManager;

    private void Start()
    {
        QuestionMarkActiveCheck();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        Drop();

        if(grabAllowed)
        {
            TakeItem();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            grabAllowed = true;
        }

        if (inventory.Container.Count < 1)
        {
            questionMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            grabAllowed = false;
        }

        questionMark.SetActive(false);
    }

    private void TakeItem()
    {
        var item = this.GetComponent<Item>();
        if (item)
        {
            if (Input.GetButtonDown("Grab") && inventory.Container.Count < 1)
            {
                if(itemObject.name == "Oil")
                {
                    audioManager.Play("oilPickUp");
                }
                else if(itemObject.name == "Coal")
                {
                    audioManager.Play("coalPickUp");
                }

                inventory.AddItem(item.itemObject, 1);
                oven.currentItem = this.gameObject;
                engine.currentItem = this.gameObject;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Drop()
    {
        if (Input.GetButtonDown("Drop") && inventory.Container.Count == 1 && oven.currentItem != null)
        {
            Debug.Log("Unset");
            oven.currentItem.gameObject.SetActive(true);
            questionMark.SetActive(false);
            dropping = true;
        }
    }


    private void QuestionMarkActiveCheck()
    {
        if(this.gameObject.activeInHierarchy)
        {
            questionMark.SetActive(false);
        }
    }

}
