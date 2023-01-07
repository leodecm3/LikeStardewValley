using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour {

    private Player _player;

    [SerializeField] private float playersMoney;
    public float GetPlayersMoney() =>  playersMoney;


    [SerializeField] private List<SOobject> inventory = new List<SOobject>();
    
    [SerializeField] private TextMeshProUGUI moneyUI;
    
    
    
    
    //SINGLETON... use like this:   _gm = GameManager.Instance;
    public static GameManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null) 
            Destroy(Instance.gameObject);
        Instance = this;
        _player = FindObjectOfType<Player>();
    }

    private void Start() {
        moneyUI.SetText(playersMoney.ToString("0") + " Coins");
    }



    public float AddPlayersMoney(float amount) {
        playersMoney += amount;
        return playersMoney;
    }
    


    [ContextMenu("TestMoney function")]
    public void TestMoney() {
        playersMoney += 10f;
    }


    public bool BuySomething(SOobject sOobject, float price, int qtd) {
        Debug.Log("BuySomething");
        if (price > playersMoney) {
            return false;
        }
        for (int i = 0; i < qtd; i++) {
            inventory.Add(sOobject.Clone());
            AddPlayersMoney(-price);
        }
        return true;
    }






}
