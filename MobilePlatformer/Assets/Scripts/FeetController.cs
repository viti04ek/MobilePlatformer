using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    public Transform DustParticleTransform;
    public bool DustParticleOn;

    public GameObject Player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && DustParticleOn)
        {
            SFXController.Instance.ShowPlayerLanding(DustParticleTransform.position);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            if (DustParticleOn)
            {
                SFXController.Instance.ShowPlayerLanding(DustParticleTransform.position);
            }

            Player.transform.parent = collision.gameObject.transform;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Player.transform.parent = null;
        }
    }
}
