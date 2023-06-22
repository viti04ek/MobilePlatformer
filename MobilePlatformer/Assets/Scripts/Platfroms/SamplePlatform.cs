using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the platform's dropping behavior
/// </summary>
public class SamplePlatform : MonoBehaviour 
{
	public float droppingDelay;

	Rigidbody2D rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();	
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Player"))
		{
			Invoke("StartDropping", droppingDelay);
		}
	}


    void StartDropping()
	{
		rb.isKinematic = false;
	}

}
