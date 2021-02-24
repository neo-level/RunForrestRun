using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    [SerializeField] private GameObject[] boxes;
    List<Rigidbody> _boxRigidbodies = new List<Rigidbody>();
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        
        // Fill up list with the boxes their colliders that form the wall.
        foreach (var box in boxes)
        {
            _boxRigidbodies.Add(box.GetComponent<Rigidbody>());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("grenade"))
        {
            _collider.enabled = false;

            foreach (var boxRigidbody in _boxRigidbodies)
            {
                boxRigidbody.isKinematic = false;
            }
        }
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }
}
