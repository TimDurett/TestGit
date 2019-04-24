using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAction : MonoBehaviour
{
    public float multiplier = 2f;
    public float duration = 4f;
    public GameObject pickupEffect; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            StartCoroutine  (Pickup(other));
        }
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

    }

    IEnumerator Pickup(Collider player)
    {
        //Debug.Log("Power Up!!!");
        Instantiate(pickupEffect, transform.position, transform.rotation);

        PlayerController FireRate = player.GetComponent<PlayerController>();
        FireRate.fireRate *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        FireRate.fireRate /= multiplier;

        Destroy(gameObject);
    }
}
