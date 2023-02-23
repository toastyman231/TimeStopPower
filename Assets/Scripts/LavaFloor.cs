using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class LavaFloor : MonoBehaviour
{
    [SerializeField] private Transform respawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided!");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collided with player");
            other.GetComponent<FirstPersonController>().Warp(respawnLocation.position, -respawnLocation.right);
        }
    }
}
