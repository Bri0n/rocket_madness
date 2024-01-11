using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float thrustSpeed = 1f;
    private float rotationDirection;
    private bool isRotating = false;
    private bool isThrusting = false;
    private Rigidbody2D rigidBody;

    private void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update(){
        if(isRotating){
            Rotate();
        }

        if(isThrusting){
            Thrust();
        }
    }

    private void Rotate(){
        rigidBody.AddTorque(rotationDirection * rotationSpeed * Time.deltaTime);
        Debug.Log("Rotating");
    }

    private void Thrust(){
        rigidBody.AddRelativeForce(Vector2.up * thrustSpeed * Time.deltaTime);
        Debug.Log("Thrusting");
    }

    private void OnRotate(InputValue value){
        rotationDirection = value.Get<Vector2>().x * -1;
        isRotating = rotationDirection != 0;
    }

    private void OnThrust(InputValue input){
        isThrusting = input.isPressed;
    }
}
