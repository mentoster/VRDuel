using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxController : MonoBehaviour
{
    public EnemyController enemyController;


    private void Update()
    {
    }

    public void Hit()
    {
        enemyController.HitHandler();
        if (enemyController.dead)
        {
            foreach (Collider item in GetComponents<Collider>())
            {
                item.enabled = false;
            }
        } else
        {
            foreach (Collider item in GetComponents<Collider>())
            {
                item.enabled = true;
            }
        }
    }

}
