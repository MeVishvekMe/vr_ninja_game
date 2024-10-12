using System.Collections;
using UnityEngine;

public class SpeedsterEnemy : MonoBehaviour {

    // Laser Spawn Points
    public Transform leftLeftPoint;
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform rightRightPoint;

    // Laser Prefab
    public GameObject laser;

    // Time for shooting laser
    private float _time = 0;

    private void Start() {
        
    }

    private void Update() {
        _time += Time.deltaTime;
        if (_time >= 7f) {
            _time = 0f;
            StartCoroutine(ShootLasers());
        }
    }

    IEnumerator ShootLasers() {
        InstantiateBullets();
        yield return new WaitForSeconds(0.5f);
        InstantiateBullets();
        yield return new WaitForSeconds(0.5f);
        InstantiateBullets();
        yield return new WaitForSeconds(0.5f);
        _time = 0f;
    }

    
    // Spawning bullet on each spawn point
    private void InstantiateBullets() {
        GameObject go1 = Instantiate(laser, leftLeftPoint.position, leftLeftPoint.rotation);
        go1.transform.Rotate(90f, 0f, 0f);
        go1.GetComponent<Rigidbody>().velocity = go1.transform.up * 10f;
        Destroy(go1, 7f);
        GameObject go2 = Instantiate(laser, leftPoint.position, leftPoint.rotation);
        go2.transform.Rotate(90f, 0f, 0f);
        go2.GetComponent<Rigidbody>().velocity = go2.transform.up * 10f;
        Destroy(go2, 7f);
        GameObject go3 = Instantiate(laser, rightPoint.position, rightPoint.rotation);
        go3.transform.Rotate(90f, 0f, 0f);
        go3.GetComponent<Rigidbody>().velocity = go3.transform.up * 10f;
        Destroy(go3, 7f);
        GameObject go4 = Instantiate(laser, rightRightPoint.position, rightRightPoint.rotation);
        go4.transform.Rotate(90f, 0f, 0f);
        go4.GetComponent<Rigidbody>().velocity = go4.transform.up * 10f;
        Destroy(go4, 7f);
    }
}
