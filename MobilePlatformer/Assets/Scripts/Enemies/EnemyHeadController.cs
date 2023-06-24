using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadController : MonoBehaviour
{
    public GameObject Enemy;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerFeet"))
        {
            GameController.Instance.PlayerStompsEnemy(Enemy);
            SFXController.Instance.ShowEnemyPoof(Enemy.transform.position);
        }
    }
}
