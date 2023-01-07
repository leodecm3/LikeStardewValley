using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField][Range(1f,20f)] private float velocity = 5f;
    private Vector3 _movingDir;
    private Rigidbody2D _rigidbody;

    private GameObject closerInteractable = null;
    public void SetCloserInteractable(GameObject g) => closerInteractable = g;

    
    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    


    public void OnMove(InputAction.CallbackContext context) {
        _movingDir = (Vector3)context.ReadValue<Vector2>();
        _rigidbody.velocity = _movingDir*velocity;
        //Debug.Log(m_movingDir*velocity);
    }



    public void OnInteract(InputAction.CallbackContext context) {
        
        if (context.started) {

            if (closerInteractable != null) {
                //closerInteractable.
            }
            
            Debug.Log("started ");
            
        }

    }




}
