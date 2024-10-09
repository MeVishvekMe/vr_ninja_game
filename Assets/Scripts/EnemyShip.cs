using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour {
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public Transform playerTransform;
    public GameObject energyBall;
    Transform[] spawnPoints;

    private float _time = -20f;

    // Enemy Movement
    public float speed = 2f; // Speed of movement
    public Vector3 pointA = new Vector3(20f, 20f, 25f);
    public Vector3 pointB = new Vector3(-20f, -20f, 25f);
    private Vector3 targetPosition;



    private void Start() {
        spawnPoints = new Transform[2];
        spawnPoints[0] = leftSpawnPoint;
        spawnPoints[1] = rightSpawnPoint;


    }

    private void Update() {
        _time += Time.deltaTime;
        if(_time >= 5f) {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject gb = Instantiate(energyBall, spawnPoint.position, Quaternion.identity);
            Vector3 direction = -(spawnPoint.position - playerTransform.position).normalized;
            gb.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
            _time = 0;
        }
    }

    
}
