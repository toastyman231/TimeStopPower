using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private GameObject nodesParent;

    [SerializeField] private float speed;

    [SerializeField] private bool shouldFollow;

    private Vector3 _startPosition;

    private Vector3 _currentPosition;

    private Transform[] _nodes;

    private float _timer;

    private int _currentNode;

    // Start is called before the first frame update
    void Start()
    {
        _nodes = nodesParent.GetComponentsInChildren<Transform>();
        _timer = 0f;
        _currentPosition = _nodes[_currentNode].position;
        _startPosition = transform.position;
    }

    private void CheckNode()
    {
        _timer = 0f;
        _currentPosition = _nodes[_currentNode].position;
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldFollow) return;

        _timer += Time.deltaTime * speed;
        if (transform.position != _currentPosition)
        {
            //Debug.Log("Moving to node");
            transform.position = Vector3.Lerp(_startPosition, _currentPosition, _timer);
        }
        else
        {
            if (_currentNode < _nodes.Length - 1)
            {
                //Debug.Log("Checking node");
                _currentNode++;
                CheckNode();
            }
            else
            {
                _currentNode = 0;
                CheckNode();
            }
        }
    }
}
