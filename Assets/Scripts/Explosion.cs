using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject brokenMesh;
    public GameObject[] shards;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    public void TakeDamage()
    {
        if (gameObject.tag == "Bottle")
            gameObject.GetComponent<MeshRenderer>().enabled = false;

        if (gameObject.tag == "Dog")
            gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;

        gameObject.GetComponent<BoxCollider>().enabled = false;
        brokenMesh.SetActive(true);
        GetComponent<MoveNPC>().enabled = false;
        foreach (GameObject shard in shards)
        {
            shard.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
        }
    }
}

