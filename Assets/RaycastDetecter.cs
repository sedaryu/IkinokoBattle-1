using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastDetecter : MonoBehaviour
{
    [SerializeField] private TriggerEvent onRayHit = new TriggerEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectForward();
    }

    private void DetectForward()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 25.0f))
        { 
            onRayHit.Invoke(hit.collider);
        }

        Debug.DrawRay(ray.origin, ray.direction * 25.0f, Color.red, 5.0f);
    }

    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {

    }
}
