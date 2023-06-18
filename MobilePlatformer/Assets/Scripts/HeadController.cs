using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))
        {
            SFXController.Instance.HandleBoxBreaking(collision.gameObject.transform.parent.transform.position);
            gameObject.transform.parent.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }
}
