using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour {
    
    [SerializeField] private List<SOobject> inventory = new List<SOobject>();
    
    [SerializeField] private TextMeshProUGUI moneyUI;
    [SerializeField] private List<Image> inventoryImages= new List<Image>();
    
    
    [SerializeField] private float playersMoney;
    public float GetPlayersMoney() =>  playersMoney;
    
    private Player _player;
    
    
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
        UpdateUIInventory();
    }



    public float AddPlayersMoney(float amount) {
        playersMoney += amount;
        moneyUI.SetText(playersMoney.ToString("0") + " Coins");
        return playersMoney;
    }
    

    [ContextMenu("TestAddMoney function")]
    public void TestAddMoney() {
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
        UpdateUIInventory();
        return true;
    }




    public void UpdateUIInventory() {
        int count = 0;
        
        for (int i = 0; i < inventoryImages.Count; i++) {

            if (count < inventory.Count) {
                inventoryImages[i].sprite = inventory[count].sprite;
                count++;
            } else {
                inventoryImages[i].sprite = null;
            }
        }
    }






}
