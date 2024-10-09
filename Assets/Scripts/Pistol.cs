using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour {

    public Transform bulletSpawnPoint;
    public GameObject bulletObject;

    private float _bulletCount = 0;
    private bool canShoot = true;
    private float rotationSpeed = 300f;

    public InputActionReference shootRef;

    private void OnEnable() {

        shootRef.action.Enable();
        shootRef.action.performed += Shoot;
    }

    private void OnDisable() {
        shootRef.action.Disable();
        shootRef.action.performed -= Shoot;
    }

    private void Update() {
        
        if (_bulletCount > 5) {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
            canShoot = false;
            if (transform.localRotation.x > 360f) {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                _bulletCount = 0;
                canShoot = true;
            }
            
        }
        
    }

    private void Shoot(InputAction.CallbackContext context) {
        if (canShoot) {
            // Use the gun's forward direction for bullet direction
            Quaternion bulletRotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(90, 0, 0); ;

            // Instantiate the bullet
            GameObject go = Instantiate(bulletObject, bulletSpawnPoint.position, bulletRotation);

            // Set the velocity of the bullet in the forward direction
            go.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
            Destroy(go, 7f);
            _bulletCount++;
        }
    }
}
