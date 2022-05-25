using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    public bool galima = false;

    public GameObject pressText;

    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && galima == true)
        {
            pressText.SetActive(false);
            Pickup();

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pressText.SetActive(true);
            galima = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pressText.SetActive(false);
            galima = false;

        }
    }
}
