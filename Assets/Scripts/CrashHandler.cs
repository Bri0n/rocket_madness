using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashHandler : MonoBehaviour
{
    
    private PlayerMovement playerMovement;
    private Health playerHealth;

    private void Awake(){
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<Health>();
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        playerMovement.ProcessCrash(other);
        playerHealth.TakeHit();
    }
}
