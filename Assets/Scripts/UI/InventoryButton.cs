using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class InventoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    
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
    private GameManager _gm;

    private void Start() {
        //cache the game manager eference
        _gm = GameManager.Instance;

        //Button functionality
        this.GetComponent<Button>().onClick.AddListener(OnClickInventoryButton);
    }
    
    private void OnDestroy() => this.GetComponent<Button>().onClick.RemoveListener(OnClickInventoryButton);
    

    public void SetInventoryButtonUI() {
        //case nothing on the button, i set it null
        qtdBG.SetActive(false);
        ThisImage.sprite = null;
    }
    public void SetInventoryButtonUI(SOobject o, int qtd) {
        //case there is something on the button i change cache the obj , include quantity if >0 and set the sprite
        
        _oobject = o;
        
        if (qtd > 1) {
            qtdBG.SetActive(true);
            qtdUI.SetText(qtd.ToString("0"));
        } else {
            qtdBG.SetActive(false);
        }

        ThisImage.sprite = _oobject.sprite;

    }


    private void OnClickInventoryButton() {
        if (_oobject == null) {
            return;
        }
        
        _gm.OnClickInventoryButton(_oobject);

    }

    


    //show the tooltip when the player is with the mouse over
    public void OnPointerEnter(PointerEventData eventData) {
        _gm.Get_tooltip().ActivateThisToolTip(true,_oobject);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _gm.Get_tooltip().ActivateThisToolTip(false,_oobject);
    }
    

}

