using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public PlayerController PlayerController;

    
    public void MoveLeft()
    {
        PlayerController.MoveLeft();
    }


    public void MoveRight()
    {
        PlayerController.MoveRight();
    }


    public void MoveStop()
    {
        PlayerController.MoveStop();
    }


    public void Fire()
    {
        PlayerController.Fire();
    }


    public void Jump()
    {
        PlayerController.JumpBtn();
    }
}
