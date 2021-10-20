using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemy = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy == null)
        {
            enemy = Instantiate(enemyPrefab, transform);
            enemy.transform.position = transform.position;
        }
        if(enemy.GetComponent<EnemyController>().IsDead == true)
        {
            enemy = null;
        }
    }
}
