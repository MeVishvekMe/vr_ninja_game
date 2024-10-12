using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour {

    public Transform bulletSpawnPoint;
    public GameObject bulletObject;

    public TextMeshProUGUI bulletText;

    private int _bulletCount = 0;
    private bool _canShoot = true;

    public Animator animator;

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
        bulletText.text = (6 - _bulletCount).ToString();
    }

    private void Shoot(InputAction.CallbackContext context) {
        if (_canShoot) {
            // Use the gun's forward direction for bullet direction
            var bulletRotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(90, 0, 0); ;

            // Instantiate the bullet
            GameObject go = Instantiate(bulletObject, bulletSpawnPoint.position, bulletRotation);

            // Set the velocity of the bullet in the forward direction
            go.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
            Destroy(go, 7f);
            _bulletCount++;
        }
        if (_bulletCount == 6) {
            StartCoroutine(GunReloadCou());
        }
    }

    IEnumerator GunReloadCou() {
        bulletText.enabled = false;
        _bulletCount++;
        _canShoot = false;
        animator.Play("GunReloadAnimation");
        yield return new WaitForSecondsRealtime(1f);
        _canShoot = true;
        _bulletCount = 0;
        bulletText.enabled = true;
    }
}
