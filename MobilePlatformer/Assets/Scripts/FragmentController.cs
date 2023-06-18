using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour
{
    public Vector3 JumpForce;
    public float DestroyDelay;
    public Rigidbody2D Rigidbody2D;


    private void Start()
    {
        Rigidbody2D.AddForce(JumpForce);
        Destroy(gameObject, DestroyDelay);
    }
}
