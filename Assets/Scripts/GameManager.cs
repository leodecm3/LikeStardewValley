using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;


[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour {

    

    [SerializeField] private GameObject prefabCrops;
    
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
    
    [SerializeField] private TooltipInventory tooltip;
    public TooltipInventory Get_tooltip() => tooltip;
    
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
        //add zero money just to update the UI
        AddPlayersMoney(0);
        UpdateUIInventory();
    }

    
#region PublicFunctions


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
            AddPlayersMoney(soobject.baseSellPrice);
            _dicInv[soobject]--;
            if (_dicInv[soobject] == 0) {
                _dicInv.Remove(soobject);
            }
            UpdateUIInventory();
            return;
        }
        
        //check if there is room to spawn an item
        RaycastHit2D hit = Physics2D.Raycast(_player.transform.position, _player.FaceDirection, 2f,1 << 3);
        if (hit == true) {
            return;
        }
        
        
        //Else will use the item in the world
        GameObject go = Instantiate(prefabCrops, _player.PositionInFrontOfThePalyer(), Quaternion.identity);
        go.GetComponent<WorldCropObject>().InitiateThis((SOcrops)soobject);
        _dicInv[soobject]--;
        if (_dicInv[soobject] == 0) {
            _dicInv.Remove(soobject);
        }
        UpdateUIInventory();
        
    }
    
    
    
#endregion PublicFunctions


#region PrivateAuxFunctions
    

    private void AddPlayersMoney(float amount) {
        playersMoney += amount;
        moneyUI.SetText(playersMoney.ToString("0") + " Coins");
    }
    
    private void UpdateUIInventory() {
        
        //tell the buttons on the screen to update the value
        OnUpdateUI?.Invoke(this, EventArgs.Empty);
        
        
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
