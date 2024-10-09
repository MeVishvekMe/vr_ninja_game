using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyEnemy : MonoBehaviour {

    private float _time;
    public GameObject energyBall;
    public Transform spawnPoint;
    public Transform playerTransform;
    public Slider slider;

    // Enemy Movement
    public float speed = 2f; // Speed of movement
    public Vector3 pointA = new Vector3(20f, 20f, 25f);
    public Vector3 pointB = new Vector3(-20f, 5f, 25f);
    private Vector3 targetPosition;

    void Start() {
        _time = -20f;
        playerTransform = Camera.main.transform;

        targetPosition = GetRandomTarget();
        StartCoroutine(MoveToTarget());
    }

    void Update() {
        _time += Time.deltaTime;
        if(_time >= 5f) {
            GameObject gb = Instantiate(energyBall, spawnPoint.position, Quaternion.identity);
            Vector3 direction = -(spawnPoint.position - playerTransform.position).normalized;
            gb.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
            _time = 0;
        }
    }

    private Vector3 GetRandomTarget() {
        float x = Random.Range(pointB.x, pointA.x);
        float y = Random.Range(pointB.y, pointA.y);
        return new Vector3(x, y, pointA.z);
    }

    private IEnumerator MoveToTarget() {
        while (true) {
            // Move towards the target position
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            // Wait a moment at the target position
            yield return new WaitForSeconds(1f);

            // Get a new random target
            targetPosition = GetRandomTarget();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Bullet") {
            slider.value--;
            Destroy(other.gameObject);
        }
    }

}
