using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;


[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI moneyUI;
    [SerializeField] private List<InventoryButton> inventoryUI= new List<InventoryButton>();
    
    //I normally use ODIN to visualize dictionaries on the editor...  [SerializeField] 
    [SerializeField] private Dictionary<SOobject,int> _dicInv= new Dictionary<SOobject,int>();
    
    
    [SerializeField] private float playersMoney;
    public float GetPlayersMoney() =>  playersMoney;
    
    private Player _player;
    public Player Get_player() => _player;
    
    private Npc _npcNear;
    public void Set_NpcNear(Npc n) => _npcNear = n;
    
    private SOclothes _wearingClothes;
    
    public event EventHandler OnUpdateUI;
    
    
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

    
#region PublicFunctions


    public float AddPlayersMoney(float amount) {
        playersMoney += amount;
        moneyUI.SetText(playersMoney.ToString("0") + " Coins");
        return playersMoney;
    }
    

    //right click on the class to test
    [ContextMenu("Test Buy Something function")]
    public void TestAddMoney() {
        BuySomething(ScriptableObject.CreateInstance<SOobject>(), 10f, 5);
    }


    public bool BuySomething(SOobject sOobject, float price, int qtd) {
        
        //case no money, just return false
        if (price > playersMoney) {
            return false;
        }

        //add one to the dictionary
        if (_dicInv.ContainsKey(sOobject) == false) {
            _dicInv.Add(sOobject,0);
        }
        _dicInv[sOobject] = _dicInv[sOobject] + qtd;
        
        //money
        AddPlayersMoney(-price);
        
        //case implement Clothes class interface, i dress the clothes
        if (sOobject is SOclothes) {
            DressClothe((SOclothes)sOobject);
        }
        
        //UI
        UpdateUIInventory();
        
        //tell the buttons on the screen to update the value
        OnUpdateUI?.Invoke(this, EventArgs.Empty);
        
        return true;
    }
    
    
    public void DressClothe(SOclothes s) {
        _wearingClothes = s;
        _player.DressClothe(s.sprite);
    }


    public void OnClickInventoryButton(SOobject soobject) {
        
        //case implement Clothes class interface, i dress the clothes
        if (soobject is SOclothes) {
            DressClothe((SOclothes)soobject);
            return;
        }

        if (_npcNear != null) {
            //sell the thing to the shop
            
            return;
        }
        
        //Else will use the item in the world
        //todo
        
    }
    
    
    
#endregion PublicFunctions


#region PrivateAuxFunctions
    
    
    private void UpdateUIInventory() {
        
        //TODO add functionality do show the quantity of each object using Dictionary dic_inventoryQtd and System.Linq
        
        int count = 0;
        SOobject oneObject;

        //for each button on the inventory canvas
        for (int i = 0; i < inventoryUI.Count; i++) {

            if (count < _dicInv.Count) {

                oneObject = _dicInv.Keys.ElementAt(i);
                
                inventoryUI[i].SetInventoryButtonUI(oneObject,_dicInv[oneObject]);
                
                count++;
            } else {
                //empty
                inventoryUI[i].SetInventoryButtonUI();
            }
        }
    }


#endregion
    


}
