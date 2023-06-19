using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuck : MonoBehaviour
{
    public GameObject Player;
    public PlayerController PlayerController;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.isStuck = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController.isStuck = false;
    }
}
