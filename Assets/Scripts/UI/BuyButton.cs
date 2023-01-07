using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour {
    [SerializeField] private GameObject xNotEnoughMoney;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Image image;
    [SerializeField] private float totalPrice;
    [SerializeField] private int qtd;
    [SerializeField] private SOobject so;
    
    private GameManager _gm;
    
    
    private void Start() {
        _gm = GameManager.Instance;
        UpdateUI();
    }

    private void OnEnable() {
        UpdateUI();
    }

    private void UpdateUI() {
        price.SetText("$"+ totalPrice + " = " + qtd + "x");
        image.sprite = so.sprite;
        
        //put the X case there is not enough money
        xNotEnoughMoney.SetActive(_gm.GetPlayersMoney() < totalPrice);

    }


    public void OnBuyThis() {

        


        UpdateUI();
    }
    

    
    
    








}
