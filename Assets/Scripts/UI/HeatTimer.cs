using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*public class HeatTimer : MonoBehaviour
{
    [Header("Time")]
    public Image timeBar;
    public float maxTime = 10f;
    public float timeLeft;

    [Header("Oven")]
    public bool coalFill;

    public InventoryObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        timeBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimeManagamenent();

        if(coalFill)
        {
            FillUpWithCoal();
            Debug.Log("Filled");
        }
    }

    private void TimeManagamenent()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / maxTime;
        }
    }

    private void FillUpWithCoal()
    {
        if (Input.GetButtonDown("Grab") && timeLeft < maxTime))
        {
            timeLeft++;
            inventory.Container.Clear();
            Debug.Log("Filling");
        }

    }
}*/
