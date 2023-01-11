using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : Agent
{
    void Update()
    {
        var objects = perception.GetGameObjects();
        foreach (var gameObject in objects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }

        if (objects.Length > 0) 
        {
            Vector3 direction = (objects[0].transform.position - transform.position).normalized;
            movement.ApplyForce(direction * 2);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
