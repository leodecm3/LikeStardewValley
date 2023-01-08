using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI moneyUI;
    [SerializeField] private List<InventoryButton> inventoryUI= new List<InventoryButton>();
    
    //I normally use ODIN to visualize dictionaries on the editor...  [SerializeField] 
    [SerializeField] private Dictionary<SOobject,int> _dicInv= new Dictionary<SOobject,int>();
    
    
    
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

        //add one to the dictionary
        if (_dicInv.ContainsKey(sOobject) == false) {
            _dicInv.Add(sOobject,0);
        }
        _dicInv[sOobject] = _dicInv[sOobject] + qtd;
        
        AddPlayersMoney(-price);
        
        UpdateUIInventory();
        return true;
    }

    

    private void UpdateUIInventory() {
        
        //TODO add functionality do show the quantity of each object using Dictionary dic_inventoryQtd and System.Linq
        
        int count = 0;
        SOobject oneObject;

        //for each button on the inventory canvas
        for (int i = 0; i < inventoryUI.Count; i++) {

            if (count < _dicInv.Count) {

                oneObject = _dicInv.Keys.ElementAt(i);
                
                inventoryUI[i].SetInventoryButtonUI(oneObject.sprite,_dicInv[oneObject]);
                
                count++;
            } else {
                inventoryUI[i].SetInventoryButtonUI(null,0);
            }
        }
    }
    


}
