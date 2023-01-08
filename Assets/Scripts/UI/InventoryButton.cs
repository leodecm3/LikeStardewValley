
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class InventoryButton : MonoBehaviour {

    private Image _image;
    [SerializeField] private GameObject qtdBG;
    [SerializeField] private TextMeshProUGUI qtdUI;

    private void Awake() {
        _image = this.GetComponent<Image>();
        qtdBG.SetActive(false);
    }
    

    public void SetInventoryButtonUI(Sprite s, int qtd) {
        
        if (qtd > 0) {
            qtdBG.SetActive(true);
            qtdUI.SetText(qtd.ToString("0"));
        } else {
            qtdBG.SetActive(false);
        }

        _image.sprite = s;

    }

}

