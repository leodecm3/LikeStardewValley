using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour {

    private Player _player;

    [SerializeField] private float playersMoney;
    public float GetPlayersMoney() =>  playersMoney;
    
    
    
    
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



    
    public float AddPlayersMoney(float amount) {
        playersMoney += amount;
        return playersMoney;
    }
    


    [ContextMenu("TestMoney function")]
    public void TestMoney() {
        playersMoney += 10f;
    }
    






}
