using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Machine : MachineController
{
    public string machineName;
    public DisplayInventory inv;

    [Header("Time Management")]
    public GameObject linearTimer;
    public Image timeBar;
    public float maxTime = 10f;
    private float timeLeft;
    public float plusItemTime;

    public string nameOfNeededItem;
    private bool fill;
    [SerializeField] public int fillCount = 0;
    public int itemCount = 1;

    [Header("References")]
    public AudioManager audioManager;
    public InventoryObject inventory;
    public GameObject inventoryUI;
    public GameObject currentItem;



    // Start is called before the first frame update
    void Start()
    {
        fillCount = 0;
        timeBar = linearTimer.GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimeManagamenent();

        if(fill)
        {
            FillUpWithResource();
        }
    }

    private void TimeManagamenent()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / maxTime;
        }
        //else if(timeLeft <= 0)
        //{
        //    LevelFailed();
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fill = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fill = false;
        }
    }

    private void FillUpWithResource()
    {
        if (Input.GetButtonDown("Grab") && timeLeft < maxTime && inventory.Container.Count >= 1 && inventory.Container[0].item.name == nameOfNeededItem)
        {
            //if (inventory.Container[0].item.name == "Coal")
            //{
            //    coalCount++;
            //}
            //else if (inventory.Container[0].item.name == "Oil")
            //{
            //    oilCount++;
            //}

            fillCount++;
            timeLeft += plusItemTime;

            CheckIfTimeIsOverflowing();

            CleanInventoryAfterFilling();
        }

    }

    private void CheckIfTimeIsOverflowing()
    {
        if(timeLeft > maxTime)
        {
            timeLeft = maxTime;
        }
    }

    private void CleanInventoryAfterFilling()
    {
        inventory.Container.Clear();
        Destroy(inventoryUI.transform.GetChild(0).gameObject);

        if(currentItem != null)
        {
            Destroy(currentItem.gameObject);
            currentItem = null;
        }
    }

    private void LevelFailed()
    {
        SceneManager.LoadScene("Start Menu");
    }

    /*private void LoopSounds()
    {
        if(machineName == "Engine")
        {
            audioManager.Play("engine");
        }
        else if(machineName == "Oven")
        {

        }

                    if(inventory.Container[0].item.name == "Coal")
                {
                    coalCount++;
                }
                else if(inventory.Container[0].item.name == "Oil")
                {
                    oilCount++;
                }

                if(inventory.Container[0].item.name == nameOfNeededItem)
            {
                fillCount++;
                timeLeft += plusItemTime;
                CleanInventoryAfterFilling();
                Debug.Log("Filling");

            }
    }*/
}

