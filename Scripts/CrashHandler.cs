using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Crashed!");
    }
}
