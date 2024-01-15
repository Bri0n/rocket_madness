using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHUD : MonoBehaviour
{
    [SerializeField] GameObject healthDisplay;

    public void RemoveHeart(){
        int currentHearts = healthDisplay.transform.childCount;
        if(currentHearts > 0){
            GameObject targetHeart = healthDisplay.transform.GetChild(currentHearts -1 ).gameObject;
            Destroy(targetHeart);
        }
    }
}
