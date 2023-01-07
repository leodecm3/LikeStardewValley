using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    private void Awake() {
        _gm = GameManager.Instance;
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

        if (_gm.BuySomething(so,totalPrice,qtd) == false) {
            
            //case cant buy, i do an simple animation using dotween
            xNotEnoughMoney.transform.localScale = new Vector2(2f, 2f);
            xNotEnoughMoney.transform.DOKill(false);
            xNotEnoughMoney.transform.DOScale(1f,0.5f);
            
        }
        
        UpdateUI();
    }
    

    
    
    








}
