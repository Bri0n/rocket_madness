using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 3;
    private LevelManager levelManager;
    private PlayerMovement playerMovement;
    private UIHUD uiHUD;

    private void Awake(){
        levelManager = FindObjectOfType<LevelManager>();
        playerMovement = GetComponent<PlayerMovement>();
        uiHUD = FindObjectOfType<UIHUD>();
    }
    
    private float GetHealth(){
        return health;
    }

    public void TakeHit(){
        health--;
        uiHUD.RemoveHeart();
        ProcessDeath();
    }

    private void ProcessDeath(){
        if(health <= 0){
            if(playerMovement != null){
                playerMovement.ProcessPlayerDeath();
            }
            levelManager.ReloadScene();
        }
    }

}
