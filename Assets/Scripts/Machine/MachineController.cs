using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineController : MonoBehaviour
{
    public string nextScene;

    public int coalCount;
    public int oilCount;

    public List<Machine> machines = new List<Machine>();

    public GameObject failMenu;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        NextScene();
        LevelEnd();
    }

    void NextScene()
    {
        if(machines[0].fillCount == coalCount && machines[1].fillCount == oilCount)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    void LevelEnd()
    {
        if(machines[0].failLevel || machines[1].failLevel)
        {
            failMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
