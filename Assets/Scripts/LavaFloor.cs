using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloor : MonoBehaviour
{
    [SerializeField] private Transform respawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided!");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            other.transform.position = respawnLocation.position;
            other.transform.eulerAngles = Vector3.left;
        }
    }
}
