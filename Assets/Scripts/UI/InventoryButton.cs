
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class InventoryButton : MonoBehaviour {

    
    //this may be called on start, so i prefer populate the value internaly
    private Image ThisImage{
        get {
            if (_ThisImage == null) {
                _ThisImage = this.GetComponent<Image>();
            }
            return _ThisImage;
        }  
        set { _ThisImage = value; } 
    }
    private Image _ThisImage;
    
    [SerializeField] private GameObject qtdBG;
    [SerializeField] private TextMeshProUGUI qtdUI;

    private SOobject _oobject;


    private void Start() {
        
        qtdBG.SetActive(false);
        this.GetComponent<Button>().onClick.AddListener(OnClickInventoryButton);
    }


    public void SetInventoryButtonUI() {
        //empty
        qtdBG.SetActive(false);
        ThisImage.sprite = null;
    }
    public void SetInventoryButtonUI(SOobject o, int qtd) {

        _oobject = o;
        
        if (qtd > 0) {
            qtdBG.SetActive(true);
            qtdUI.SetText(qtd.ToString("0"));
        } else {
            qtdBG.SetActive(false);
        }

        ThisImage.sprite = _oobject.sprite;

    }


    private void OnClickInventoryButton() {
        
        //case implement Clothes class interface, i dress the clothes
        if (_oobject is SOclothes) {
            GameManager.Instance.DressClothe((SOclothes)_oobject);
        }
        //TODO else to plant a tree
        
    }
    

}

