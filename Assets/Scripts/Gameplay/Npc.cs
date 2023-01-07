using System;
using Cinemachine;
using UnityEngine;

public class Npc : MonoBehaviour {

    [SerializeField] private TriggerEvents visionCone;
    [SerializeField] private GameObject interactionLabel;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    
    void OnEnable()
    {
        visionCone.onTriggerEvent.AddListener(VisionConeColliderTrigger);
    }
 
    void OnDisable()
    {
        visionCone.onTriggerEvent.RemoveListener(VisionConeColliderTrigger);
    }
    


    private void VisionConeColliderTrigger(Collider2D col, TriggerEnum triggerEnum) {
        if (col.TryGetComponent(out Player player)) {
            switch (triggerEnum) {
            case TriggerEnum.Enter:
                interactionLabel.SetActive(true);
                cinemachineVirtualCamera.Priority = 11;
                
                //tell the player that there is a interactable near
                player.SetCloserInteractable(this.gameObject);
                
                break;
            case TriggerEnum.Exit:
                interactionLabel.SetActive(false);
                cinemachineVirtualCamera.Priority = 0;
                
                player.SetCloserInteractable(null);
                
                break;
            }
        }
    }
    
    


}

