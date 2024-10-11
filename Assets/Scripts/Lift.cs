using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lift : MonoBehaviour {
    public InputActionReference liftMove;

    private bool liftShouldMove = false;
    private Rigidbody _rb;

    public GameObject objectToDestroy;

    private AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() {

        liftMove.action.Enable();
        liftMove.action.performed += MoveLift;
    }

    private void OnDisable() {
        liftMove.action.Disable();
        liftMove.action.performed -= MoveLift;
    }

    private void Update() {
        if (liftShouldMove) {
            if (transform.localPosition.y >= 6.125f) {
                _rb.velocity = new Vector3(0f, 0f, 0f);
                liftShouldMove = false;
                liftMove.action.performed -= MoveLift;
                Destroy(objectToDestroy);
                
            }
            else {
                _rb.velocity = new Vector3(_rb.velocity.x, 20f * Time.deltaTime, _rb.velocity.z);

                _rb.isKinematic = false;
            }
            
        }
        else {
            _rb.isKinematic = true;
        }
    }

    private void MoveLift(InputAction.CallbackContext context) {
        liftShouldMove = true;
        
    }
}
