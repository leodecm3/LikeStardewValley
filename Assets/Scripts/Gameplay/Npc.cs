using System;
using Cinemachine;
using UnityEngine;

public class Npc : MonoBehaviour {

    [SerializeField] private TriggerEvents visionCone;
    [SerializeField] private GameObject interactionLabel;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    
    void OnEnable()
    {
        visionCone.onTriggerEnter2DEvent.AddListener(VisionConeColliderTrigger);
        visionCone.onTriggerExit2DEvent.AddListener(VisionConeColliderTrigger);
    }
 
    void OnDisable()
    {
        visionCone.onTriggerEnter2DEvent.RemoveListener(VisionConeColliderTrigger);
        visionCone.onTriggerExit2DEvent.RemoveListener(VisionConeColliderTrigger);
    }
    


    private void VisionConeColliderTrigger(Collider2D col, TriggerEnum triggerEnum) {
        switch (triggerEnum) {
            case TriggerEnum.Enter:
                interactionLabel.SetActive(true);
                cinemachineVirtualCamera.Priority = 11;
                break;
            case TriggerEnum.Exit:
                interactionLabel.SetActive(false);
                cinemachineVirtualCamera.Priority = 0;
                break;
            }
    }
    
    
    
    









}

