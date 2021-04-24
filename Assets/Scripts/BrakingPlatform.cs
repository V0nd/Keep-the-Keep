using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakingPlatform : MonoBehaviour
{
    [Header("Falling Apart")]
    public float fallApartDelay;
    public float spawnBackDelay;

    [Header("Camera Shake")]
    public float amplitude;
    public float shakeTime;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("fallApart", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("fallApart", true);
            CameraShake.Instance.ShakeCamera(amplitude, shakeTime);
            FindObjectOfType<AudioManager>().Play("rockBreak");
            Invoke("FallApart", fallApartDelay);
        }
    }


    private void FallApart()
    {
        this.gameObject.SetActive(false);
        Invoke("SpawnBack", spawnBackDelay);
    }

    private void SpawnBack()
    {
        this.gameObject.SetActive(true);
        anim.SetBool("fallApart", false);
    }
}

