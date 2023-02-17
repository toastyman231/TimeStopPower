using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillableObject : MonoBehaviour
{
    [SerializeField] private float dissolveTime;

    private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _material = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.Enter].wasPressedThisFrame)
        {
            //Debug.Log("Pressed enter!");
            StartCoroutine(DissolveAnimation());
        }

        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            _material.SetFloat("_Value", 0);
        }
    }

    private IEnumerator DissolveAnimation()
    {
        float normalizedTime = 0f;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / dissolveTime;
            _material.SetFloat("_Value", normalizedTime);
            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);
        //Destroy(gameObject);
    }
}
