using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Thrust")]
    [SerializeField] private float thrustSpeed = 1f;
    [SerializeField] private ParticleSystem thrustEffect;
    [SerializeField] private float freezeSpeed = 10f;
    private float defaultDrag;
    private bool isThrusting = false;
    private bool isFreezing = false;
    private Vector2 thrustDirection = new Vector2(0, 1);

    [Header("Direction Indicator")]
    [SerializeField] private GameObject directionIndicator;
    [SerializeField] private Vector2 thrustIndicatorDistance = new Vector2(1f, 1.5f);
    
    [Header("Collision")]
    [SerializeField] private float bounce = 1f;
    
    private Rigidbody2D rigidBody;
    
    private void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        defaultDrag = rigidBody.drag;
    }
    private void Update()
    {
        ProcessFreeze();
        ProcessThrust();
    }

    private void ProcessThrust()
    {
        if (isThrusting){
            Thrust();
        } else {
            StopThrusting();
        }
    }

    private void Thrust(){
        rigidBody.AddRelativeForce(thrustDirection * thrustSpeed * Time.deltaTime);
        if(!thrustEffect.isPlaying){
            thrustEffect.Play();
        }
    }

    private void StopThrusting(){
        if(thrustEffect.isPlaying){
                thrustEffect.Stop();
        }
    }

    private void ProcessFreeze(){
        if(isFreezing){
            rigidBody.drag = freezeSpeed;
        } else {
            rigidBody.drag = defaultDrag;
        }
    }

    private void OnRotate(InputValue value){
        Vector2 newDirection = value.Get<Vector2>();
        if(newDirection != Vector2.zero){
            thrustDirection = newDirection * thrustIndicatorDistance;
            UpdateMovementEffects(newDirection);
        }

    }

    private void UpdateMovementEffects(Vector2 newDirection)
    {
        Transform indicator= directionIndicator.transform;
        indicator.position = (Vector2)transform.position + thrustDirection * thrustIndicatorDistance;
        indicator.rotation = quaternion.identity; // Reiniciar rotación así no se acumula
        indicator.Rotate(0, 0, Vector2.SignedAngle(Vector2.up, newDirection));
        thrustEffect.transform.position = (Vector2)transform.position + thrustDirection * -1;
        thrustEffect.transform.rotation = indicator.rotation;
    }

    private void OnThrust(InputValue value){
        isThrusting = value.isPressed && !isFreezing;
    }

    private void OnFreeze(InputValue value){
        isFreezing = value.isPressed && !isThrusting;
    }

    public void ProcessCrash(Collision2D collision){
        rigidBody.AddRelativeForce(collision.GetContact(0).normal * bounce);
    }

    public void StartFinishSequence(){
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void ProcessPlayerDeath(){
        StopThrusting();
        TogglePlayerMovement();
    }
    
    private void TogglePlayerMovement(){
        enabled = !enabled;
    }
}
