using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float Speed;
    private float _offsetX;
    public Material Material;
    public GameObject Player;
    public PlayerController PlayerController;


    private void Update()
    {
        if (!PlayerController.isStuck)
        {
            _offsetX += Input.GetAxisRaw("Horizontal") * Speed;
            if (PlayerController._leftPressed)
                _offsetX += -Speed;
            else if (PlayerController._rightPressed)
                _offsetX += Speed;

            Material.SetTextureOffset("_MainTex", new Vector2(_offsetX, 0));
        }
    }
}
