
using System;
using UnityEngine;

public class WorldCropObject : InteractableObject {

    [SerializeField] private TriggerEvents visionCone;
    
    [SerializeField] private SpriteRenderer progressBar;
    
    private SpriteRenderer spriteRenderer;
    
    private bool _isInsideVisionCone = false;

    private float timeToFishish = 5f;
    private float timeTotal = 0;


    [SerializeField] private SOcrops _socrops;


    public void InitiateThis(SOcrops s) {
        _socrops = s;

        timeToFishish = _socrops.timeToGrow;
        
        spriteRenderer = this.GetComponent<SpriteRenderer>();

    }
    
    private void Update() {
        if (timeToFishish > timeTotal) {
            timeTotal += Time.deltaTime;
            progressBar.size = new Vector2(timeTotal/timeToFishish, 1f);
           
            var posInt = (int)Mathf.Lerp(0,_socrops.GetGrowingSprite().Length-1, timeTotal/timeToFishish );
            
            Debug.Log(posInt + " and " + _socrops.GetGrowingSprite().Length+ "  == " + timeTotal + "/" + timeToFishish + " = " + timeTotal/timeToFishish);
            
            spriteRenderer.sprite = _socrops.GetGrowingSprite()[posInt];

        }
    }
    
    

    void OnEnable() => 
        visionCone.onTriggerEvent.AddListener(VisionConeColliderTrigger);
    void OnDisable()=> 
        visionCone.onTriggerEvent.RemoveListener(VisionConeColliderTrigger);
    
    
    
    private void VisionConeColliderTrigger(Collider2D col, TriggerEnum triggerEnum) {
        if (col.GetComponent<Player>()) {
            switch (triggerEnum) {
                case TriggerEnum.Enter:
                    _isInsideVisionCone = true;
                    break;
                case TriggerEnum.Exit:
                    _isInsideVisionCone = false;
                    break;
            }
        }
    }
    
    
    
    public override void InteractWithThis() {
        if (_isInsideVisionCone && timeToFishish < timeTotal) {
            Debug.Log("Collecting !");

            GameManager.Instance.BuySomething(_socrops, 0, 2);
            
            Destroy(this.gameObject);
        }
    }
    
    
    
    
    
}

