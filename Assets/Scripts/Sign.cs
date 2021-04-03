using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI textMesh;
    public string dialogText;
    private bool playerInRange;
    public bool reading;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        reading = false;
    }

    // Update is called once per frame
    void Update()
    {
        OpenCloseText();
    }

    private void OpenCloseText()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E) && !pauseMenu.activeInHierarchy)
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                Time.timeScale = 1f;
                reading = false;
            }
            else
            {
                dialogBox.SetActive(true);
                textMesh.text = dialogText;
                Time.timeScale = 0f;
                reading = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
    }
}
