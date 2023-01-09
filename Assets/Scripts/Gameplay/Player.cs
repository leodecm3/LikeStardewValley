using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    
    [HideInInspector]
    public Vector3 FaceDirection  { get; private set; }
    
    [SerializeField] private SpriteRenderer clothesVisual;
    [SerializeField][Range(1f,20f)] private float velocity = 5f;
    private Vector3 _movingDir;
    private Rigidbody2D _rigidbody;
    
    
    
    
    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    

    public void OnMove(InputAction.CallbackContext context) {
        _movingDir = (Vector3)context.ReadValue<Vector2>();
        _rigidbody.velocity = _movingDir*velocity;

        //record the last face direction
        if (_movingDir.magnitude > 0) {
            FaceDirection = _movingDir;
        }

    }
    
    public void OnInteract(InputAction.CallbackContext context) {
        
        if (context.started) {

            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, FaceDirection, 2f,1 << 3);

            //check if hit something
            if (hit == false) {
                return;
            }
                
            if (hit.transform.TryGetComponent(out InteractableObject interactableObject)) {
                interactableObject.InteractWithThis();
            }
            
            Debug.DrawRay(this.transform.position, FaceDirection.normalized * 2f, Color.green, 100);
            Debug.Log("OnInteract, Hit = " + hit.transform.name);
        }

    }

    public Vector3 PositionInFrontOfThePalyer() {
        return this.transform.position + FaceDirection.normalized * 1f;
    }


    public void DressClothe(Sprite s) {
        clothesVisual.sprite = s;
    }





}
