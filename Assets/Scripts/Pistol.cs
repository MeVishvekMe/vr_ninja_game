using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour {

    public Transform bulletSpawnPoint;
    public GameObject bulletObject;

    public InputActionReference shootRef;

    private void OnEnable() {

        shootRef.action.Enable();
        shootRef.action.performed += Shoot;
    }

    private void OnDisable() {
        shootRef.action.Disable();
        shootRef.action.performed -= Shoot;
    }

    private void Shoot(InputAction.CallbackContext context) {
        // Use the gun's forward direction for bullet direction
        Quaternion bulletRotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(90, 0, 0); ;

        // Instantiate the bullet
        GameObject go = Instantiate(bulletObject, bulletSpawnPoint.position, bulletRotation);

        // Set the velocity of the bullet in the forward direction
        go.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
    }
}
