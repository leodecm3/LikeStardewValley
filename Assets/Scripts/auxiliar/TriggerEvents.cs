
using System;
using UnityEngine;
using UnityEngine.Events;


public enum TriggerEnum {
    Enter,
    Exit,
    Stay
}

public class TriggerEvents : MonoBehaviour {

    
    public UnityEvent<Collider2D,TriggerEnum> onTriggerEnter2DEvent;
    public UnityEvent<Collider2D,TriggerEnum> onTriggerExit2DEvent;
    public UnityEvent<Collider2D,TriggerEnum> onTriggerStay2DEvent;
    
    
    private void OnTriggerEnter2D(Collider2D col) {
        onTriggerEnter2DEvent.Invoke(col,TriggerEnum.Enter);
    }
    
    private void OnTriggerExit2D(Collider2D col) {
        onTriggerExit2DEvent.Invoke(col,TriggerEnum.Exit);
    }

    private void OnTriggerStay2D(Collider2D col) {
        onTriggerStay2DEvent.Invoke(col,TriggerEnum.Stay);
    }
    

}

