using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour {

    private Player _player;
    
    
    //SINGLETON... use like this:   _gm = GameManager.Instance;
    public static GameManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null) 
            Destroy(Instance.gameObject);
        Instance = this;
    }


    private void Start() {
        _player = FindObjectOfType<Player>();
    }






}
