using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour {
    public GameObject camera;
    public CapsuleCollider capsuleCollider;

    private void Start() {
        
    }

    void Update() {
        //transform.position = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);
        capsuleCollider.height = camera.transform.localPosition.y;
        capsuleCollider.center = new Vector3(transform.position.x, capsuleCollider.height / 2, transform.position.z);
    }
}
