using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class RotateArms : MonoBehaviour
{
    private FirstPersonController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponentInParent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Camera.main.transform.eulerAngles;
    }

    public void Punch()
    {
        _controller.Punch();
    }
}
