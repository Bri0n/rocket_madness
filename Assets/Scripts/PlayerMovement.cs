using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float thrustSpeed = 1f;
    [SerializeField] private GameObject directionIndicator;
    [SerializeField] private Vector2 indicatorDistance = new Vector2(1f, 1.5f);
    //private float rotationDirection;
    private Vector2 thrustDirection = new Vector2(0, 1);
    private bool isRotating = false;
    private bool isThrusting = false;
    private Rigidbody2D rigidBody;

    private void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update(){
        /*if(isRotating){
            Rotate();
        }*/
        directionIndicator.transform.position = (Vector2)transform.position + thrustDirection;
        if(isThrusting){
            Thrust();
        }
    }

    /*private void Rotate(){
        
        rigidBody.freezeRotation = true;
        //float impulse = rotationDirection * rotationSpeed * Time.deltaTime;
        //rigidBody.AddTorque(impulse);
        rigidBody.rotation += rotationDirection * rotationSpeed * Time.deltaTime;
        Debug.Log("Rotating");
        rigidBody.freezeRotation = false;
    }*/



    private void Thrust(){
        rigidBody.AddRelativeForce(thrustDirection * thrustSpeed * Time.deltaTime);
        Debug.Log("Thrusting");
    }

    private void OnRotate(InputValue value){
        /*rotationDirection = value.Get<Vector2>().x * -1;
        isRotating = rotationDirection != 0;*/
        Vector2 newDirection = value.Get<Vector2>();
        if(newDirection != Vector2.zero){
            thrustDirection = newDirection * indicatorDistance;
            directionIndicator.transform.rotation = quaternion.identity;
            directionIndicator.transform.Rotate(0, 0, Vector2.SignedAngle(new Vector2(0,1),newDirection));
        }
        
    }

    private void OnThrust(InputValue input){
        isThrusting = input.isPressed;
    }
}
