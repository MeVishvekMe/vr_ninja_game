using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lift : MonoBehaviour {
    public InputActionReference liftMove;

    private bool liftShouldMove = false;
    private Rigidbody _rb;

    private void Start() {
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
            if (transform.localPosition.y >= 5.5f) {
                _rb.velocity = new Vector3(0f, 0f, 0f);
                liftShouldMove = false;
                liftMove.action.performed -= MoveLift;
            }
            else {
                _rb.velocity = new Vector3(_rb.velocity.x, 20f * Time.deltaTime, _rb.velocity.z);
            }
            
        }
    }

    private void MoveLift(InputAction.CallbackContext context) {
        liftShouldMove = true;
    }
}
