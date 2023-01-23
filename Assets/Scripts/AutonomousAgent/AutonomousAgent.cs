using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : Agent
{
    [SerializeField] private Perception flockPerception;
    public AutonomousAgentData data;
	public ObstacleAvoidance obstacleAvoidance;

	public float wanderAngle { get; set; } = 0;

    void Update()
    {
        var objects = perception.GetGameObjects();
        foreach (var gameObject in objects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }

        if (objects.Length > 0) 
        {
            movement.ApplyForce(Steering.Seek(this, objects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, objects[0]) * data.fleeWeight);
        }

        objects = flockPerception.GetGameObjects();

        foreach (var gameObject in objects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }

        if (objects.Length > 0)
        {
            movement.ApplyForce(Steering.Cohesion(this, objects) * data.cohesionWeight);
            movement.ApplyForce(Steering.Seperation(this, objects, data.separationRadius) * data.separationWeight);
            movement.ApplyForce(Steering.Alignment(this, objects) * data.alignmentWeight);
        }


		if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }
		// obstacle avoidance 
		if (obstacleAvoidance.IsObstacleInFront())
		{
			Vector3 direction = obstacleAvoidance.GetOpenDirection();
			movement.ApplyForce(Steering.CalculateSteering(this, direction) * data.obstacleWeight);
		}
        Vector3 position = transform.position;
        position = Utilities.Wrap(position, new Vector3(-25, -25, -25), new Vector3(25, 25, 25));
        position.y = 0;
        transform.position = position;
    }
}
