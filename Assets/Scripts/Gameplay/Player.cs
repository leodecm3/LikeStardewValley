using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    
    [SerializeField] private SpriteRenderer clothesVisual;
    [SerializeField][Range(1f,20f)] private float velocity = 5f;
    private Vector3 _movingDir;
    private Rigidbody2D _rigidbody;

    private Vector2 _faceDirection;
    
    


    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    

    public void OnMove(InputAction.CallbackContext context) {
        _movingDir = (Vector3)context.ReadValue<Vector2>();
        _rigidbody.velocity = _movingDir*velocity;
        _faceDirection = _movingDir;
        //Debug.Log(m_movingDir*velocity);
    }
    
    public void OnInteract(InputAction.CallbackContext context) {
        
        if (context.started) {

            //_movingDir = _movingDir.normalized;//direction is normalized by the raycast, this line is redundant 
            RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, 2f, _movingDir,2f);
            
            if (hit.transform.TryGetComponent(out InteractableObject interactableObject)) {
                interactableObject.InteractWithThis();
            }
            
            Debug.Log("started ");
        }

    }


    public void DressClothe(Sprite s) {
        clothesVisual.sprite = s;
    }




}
