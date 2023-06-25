using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossOneAI : MonoBehaviour
{
    public float JumpSpeed;
    public int StartJumpingAt;
    public int JumpDelay;
    public int Health;
    public Slider BossHealth;
    public GameObject BossBullet;
    public float FireDelay;
    public Rigidbody2D Rigidbody2D;
    public SpriteRenderer SpriteRenderer;

    private Vector3 _bulletSpawnPos;
    private bool _canFire;
    private bool _isJumping;


    private void Start()
    {
        _canFire = false;
        _bulletSpawnPos = gameObject.transform.Find("BulletSpawn").transform.position;
        Invoke("Reload", Random.Range(1f, FireDelay));
    }


    private void Update()
    {
        if (_canFire)
        {
            FireBullets();
            _canFire = false;

            if (Health < StartJumpingAt && !_isJumping)
            {
                InvokeRepeating("Jump", 0, JumpDelay);
                _isJumping = true;
            }
        }
    }


    private void Reload()
    {
        _canFire = true;
    }


    private void FireBullets()
    {
        Instantiate(BossBullet, _bulletSpawnPos, Quaternion.identity);
        Invoke("Reload", FireDelay);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (Health == 0)
                GameController.Instance.BulletHitEnemy(gameObject.transform);

            if (Health > 0)
            {
                Health--;
                BossHealth.value = (float)Health;

                SpriteRenderer.color = Color.red;
                Invoke("RestoreColor", 0.1f);
            }
        }
    }


    private void RestoreColor()
    {
        SpriteRenderer.color = Color.white;
    }


    private void Jump()
    {
        Rigidbody2D.AddForce(new Vector2(0, JumpSpeed));
    }
}
