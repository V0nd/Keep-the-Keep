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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        NextScene();
    }

    void NextScene()
    {
        if(machines[0].fillCount == coalCount && machines[1].fillCount == oilCount)
        {
            Debug.Log("NextScene");
            SceneManager.LoadScene(nextScene);
        }
    }
}
