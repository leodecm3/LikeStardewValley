
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipInventory : MonoBehaviour {

    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private TextMeshProUGUI textValues;
    
    private GameObject goChild;
    private bool _isActive = false;
    private Vector3 _mousePos;
    private RectTransform _rectTransformTooltip;
    private SOobject _oobject;
    
    
    private void Start() {
        //cache the RectTransform reference
        _rectTransformTooltip = GetComponent<RectTransform>();
        
        //initiate with it hidden
        goChild = transform.GetChild(0).gameObject;
        goChild.SetActive(false);
    }
    
    
    private void Update() {
        //update the position of the tooltip following the mouse (using the new input system)
        if (_isActive) {
            _mousePos = Mouse.current.position.ReadValue();
            _rectTransformTooltip.anchoredPosition = _mousePos / canvasRectTransform.localScale.x;
        }
    }


    public void ActivateThisToolTip(bool b, SOobject o) {
        //only show and update if there is nomething
        if (o == null) {
            return;
        }
        
        _oobject = o;
        goChild.SetActive(b);
        _isActive = b;
        UpdateTextValues();
        void UpdateTextValues() {
            string text = _oobject.name;
            text += "\nBuy price: " + _oobject.baseBuyPrice;
            text += "\nSell price: " + _oobject.baseSellPrice;
            textValues.SetText(text);
        }
    }

    
}

