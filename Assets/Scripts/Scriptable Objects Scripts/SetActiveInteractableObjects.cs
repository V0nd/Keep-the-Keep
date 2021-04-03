using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveInteractableObjects : MonoBehaviour
{
    public GameObject interactableObject;
    public InventoryObject inventory;
    public GameObject questionMark;

    // Update is called once per frame
    private void Start()
    {
        interactableObject = null;
    }

    void Update()
    {
        if(interactableObject != null || questionMark != null || interactableObject != null && questionMark != null)
        {
            SetActiveTrue();
        }
    }

    private void SetActiveTrue()
    {
        if (Input.GetButtonDown("Drop") && inventory.Container.Count >= 1)
        {
            Debug.Log("Respawned");
            interactableObject.gameObject.SetActive(true);
            questionMark.gameObject.SetActive(false);
        }
    }

}
