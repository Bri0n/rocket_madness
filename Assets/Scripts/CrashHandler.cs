using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashHandler : MonoBehaviour
{
    
    private PlayerMovement playerMovement;
    private Health playerHealth;
    private LevelManager levelManager;

    private void Awake(){
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<Health>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Finish"){
            playerMovement.StartFinishSequence();
            levelManager.LoadNextLevel();
        } else if(other.gameObject.tag != "Friendly"){
            playerMovement.ProcessCrash(other);
            playerHealth.TakeHit();
        }
        
    }
}
