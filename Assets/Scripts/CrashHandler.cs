using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashHandler : MonoBehaviour
{
    
    private PlayerMovement playerMovement;

    private void Awake(){
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        playerMovement.ProcessCrash(other);
        Debug.Log("Crashed!");
        Debug.Log("Normal: " + other.GetContact(0).normal);
    }
}
