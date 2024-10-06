using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceGameObjects : MonoBehaviour {

    public GameObject saber;
    private float saberSize = 0f;
    private float saberSpeed = 2f;
    
    
    private void Start() {
        
    }

    private void Update() {
        SaberOn();
    }

    public void SaberOn() {
        saberSize = Mathf.Lerp(saberSize, 1f, saberSpeed * Time.deltaTime);
        saberSize = Mathf.Clamp(saberSize, 0f, 1f);
        saber.transform.localScale = new Vector3(saber.transform.localScale.x, saberSize, saber.transform.localScale.z);
    }
}
