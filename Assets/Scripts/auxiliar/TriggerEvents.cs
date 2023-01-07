
using System;
using UnityEngine;
using UnityEngine.Events;


public enum TriggerEnum {
    Enter,
    Exit,
    Stay
}

public class TriggerEvents : MonoBehaviour {

    
    public UnityEvent<Collider2D,TriggerEnum> onTriggerEvent;
    
    
    private void OnTriggerEnter2D(Collider2D col) {
        onTriggerEvent.Invoke(col,TriggerEnum.Enter);
    }
    
    private void OnTriggerExit2D(Collider2D col) {
        onTriggerEvent.Invoke(col,TriggerEnum.Exit);
    }

    private void OnTriggerStay2D(Collider2D col) {
        onTriggerEvent.Invoke(col,TriggerEnum.Stay);
    }
    

}

