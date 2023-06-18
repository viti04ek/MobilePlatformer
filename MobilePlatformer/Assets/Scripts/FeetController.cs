using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    public Transform DustParticleTransform;
    public bool DustParticleOn;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && DustParticleOn)
            SFXController.Instance.ShowPlayerLanding(DustParticleTransform.position);
    }
}
