using System;
using Cinemachine;
using UnityEngine;

public class Npc : InteractableObject {

    [SerializeField] private TriggerEvents visionCone;
    [SerializeField] private GameObject interactionLabel;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GameObject npcCanvas;

    private bool _isInsideVisionCone = false;

    private void Start() {
        //just in case i forget...
        npcCanvas.SetActive(false);
    }

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
                _isInsideVisionCone = true;
                break;
            case TriggerEnum.Exit:
                interactionLabel.SetActive(false);
                npcCanvas.SetActive(false);
                cinemachineVirtualCamera.Priority = 0;
                _isInsideVisionCone = false;
                break;
            }
        }
    }
    
    
    
    //Class common to any  InteractableObject
    public override void InteractWithThis() {
        if (_isInsideVisionCone) {
            interactionLabel.SetActive(false);
            npcCanvas.SetActive(true);
        }
        
    }
}

