using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestScript : MonoBehaviour {

    public string debugPhrase;
    
    
    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(debugPhrase);
    }



}
