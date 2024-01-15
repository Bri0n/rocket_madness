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
    [SerializeField] private Vector2 thrustIndicatorDistance = new Vector2(1f, 1.5f);
    [SerializeField] private ParticleSystem thrustEffect;
    //private float rotationDirection;
    private Vector2 thrustDirection = new Vector2(0, 1);
    private bool isRotating = false;
    private bool isThrusting = false;
    private Rigidbody2D rigidBody;

    private void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update(){
        if(isThrusting){
            Thrust();
            if(!thrustEffect.isPlaying){
                thrustEffect.Play();
            }
            Debug.Log("Thrusting");
        } else {
            if(thrustEffect.isPlaying){
                thrustEffect.Stop();
            }
        }
    }

    private void Thrust(){
        rigidBody.AddRelativeForce(thrustDirection * thrustSpeed * Time.deltaTime);
    }

    private void OnRotate(InputValue value){
        Vector2 newDirection = value.Get<Vector2>();
        if(newDirection != Vector2.zero)
        {
            thrustDirection = newDirection * thrustIndicatorDistance;
            UpdateMovementEffects(newDirection);
        }

    }

    private void UpdateMovementEffects(Vector2 newDirection)
    {
        Transform indicatorTransform= directionIndicator.transform;
        indicatorTransform.position = (Vector2)transform.position + thrustDirection * thrustIndicatorDistance;
        indicatorTransform.rotation = quaternion.identity; // Reiniciar rotación así no se acumula
        indicatorTransform.Rotate(0, 0, Vector2.SignedAngle(Vector2.up, newDirection));
        thrustEffect.transform.position = (Vector2)transform.position + thrustDirection * -1;
        thrustEffect.transform.rotation = directionIndicator.transform.rotation;
    }

    private void OnThrust(InputValue input){
        isThrusting = input.isPressed;
    }
}
