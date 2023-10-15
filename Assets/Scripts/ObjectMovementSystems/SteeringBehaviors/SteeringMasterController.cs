using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteeringMasterController : MonoBehaviour
{
    public IBoid host;
    public Vector3 steering;
    public CharacterMovement characterMovementController;
    public List<ISteeringBehavior> behaviours;

    //Use this to ignore the first couple of frame because it is a random Velocity.
    private bool movementAllowed;


    private void Start()
    {
        movementAllowed = false;
        if (host == null)
        {
            host = GetComponent<IBoid>();
        }

        if(characterMovementController == null)
        {
            characterMovementController = GetComponent<CharacterMovement>();
        }
        StartCoroutine(HandleMovementOffset());
    }

    IEnumerator HandleMovementOffset()
    {
        yield return new WaitForSeconds(0.02f);
        movementAllowed = true;
    }

    public void AddBehavior(Behaviors behavior, Transform target)
    {
        behaviours.Add(new ISteeringBehavior(behavior, target));
    }

    public void RemoveBehavior(Behaviors behavior, Transform target)
    {
        behaviours.RemoveAll(element => element.behaviorType == behavior && element.target == target);
    }

    public void Update()
    {
        if (movementAllowed)
        {
            steering = new Vector3(0, 0, 0);
            foreach (ISteeringBehavior behavior in behaviours)
            {
                switch (behavior.behaviorType)
                {
                    case Behaviors.Seek:
                        if (behavior.target != null)
                        {
                            Seek(behavior.target.position, host.slowingDistance);
                        }
                        break;
                    default:
                        print("TODO implement Steering Behavior " + behavior.behaviorType.ToString());
                        break;
                }
            }
            UpdateMovement();
        }
    }

    //Call at the end of update to push changed velocity to character mover.

    public void UpdateMovement()
    {
        /*
        //Vector3 position = host.GetPosition();
        velocity = velocity.normalized;
        velocity.Scale(host.GetMaxVelocity() * Vector3.one);
        velocity.Scale((1 / host.GetMass()) * Vector3.one);
        Vector3.ClampMagnitude(velocity, host.GetMaxVelocity());
        //host.position = position.add(velocity);
        */
        Vector3 calculatedVector = host.GetVelocity() + steering;
        calculatedVector = Vector3.ClampMagnitude(calculatedVector, host.maxVelocity);
        if(calculatedVector.magnitude >= 0.05)
        {
            characterMovementController.SetMovement(calculatedVector);
        }
        else
        {
            characterMovementController.SetMovement(Vector3.zero);
        }
    }

    // The public API (one method for each behavior)
    public void Seek(Vector3 target, float slowingRadius = 10) { //slowingRadius is from Boid
        steering += DoSeek(target, slowingRadius);
    }

    public void Flee(Vector3 target) { 
        
    }

	public void Wander(){

    }

	public void Evade(IBoid target) {

    }

	public void Pursuit(IBoid target) {
    
    }

    // Reset the internal steering force. 
	public void ResetForces() {

    }
	// The internal API 
    private Vector3 DoSeek(Vector3 target, float slowingRadius) {
        Vector3 newSteering;
        // Calculate the desired velocity 
        Vector3 desired_velocity = target - host.GetPosition();
        float distance = desired_velocity.magnitude;
        if(distance < 0.5)
        {
            distance = 0;
        }
        // Check the distance to detect whether the character 
        // is inside the slowing area 
        if (distance < slowingRadius)
        {
            // Inside the slowing area 

            desired_velocity = desired_velocity.normalized * host.GetMovementSpeed() * (distance / slowingRadius);
        }
        else
        {
            // Outside the slowing area. 
            desired_velocity = desired_velocity.normalized * host.GetMovementSpeed();
        }
        // Set the steering based on this 
        newSteering = desired_velocity;// - host.GetVelocity();
        return newSteering;
    } 

	private Vector3 DoFlee(Vector3 target) {
        return Vector3.zero; //TODO
    }
	private Vector3 DoWander() {
        return Vector3.zero; //TODO
    }
	private Vector3 DoEvade(IBoid target) {
        return Vector3.zero; //TODO
    }
	private Vector3 DoPursuit(IBoid target) {
        return Vector3.zero; //TODO
    }
}
