using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{

    public GameObject victoryText;
    public PlayerMovementCustom player;

    public void Awake()
    {
        player = GetComponent<PlayerMovementCustom>();
    }

    public void Finish()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            victoryText.SetActive(true);
            Finish();
            
        }
    }
}
