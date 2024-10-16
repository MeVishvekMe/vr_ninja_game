using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    // Enemy Movement
    public float speed = 2f; // Speed of movement
    private Vector3 targetPosition;

    public Transform playerTransform;

    public float minDistance = 50f;
    public float maxDistance = 100f;

    void Start() {
        playerTransform = Camera.main.transform;
        targetPosition = GetRandomTarget();
        StartCoroutine(MoveToTarget());
    }

    private void Update() {
        transform.LookAt(playerTransform);
    }

    private Vector3 GetRandomTarget() {
        float theta = 2f * Mathf.PI * Random.value; // Azimuthal angle
        float phi = Mathf.Acos(Random.value);       // Polar angle

        // Generate random radius between minDistance and maxDistance
        float r = minDistance + (maxDistance - minDistance) * Random.value;

        // Convert to Cartesian coordinates
        float x = r * Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = r * Mathf.Cos(phi);
        float z = r * Mathf.Sin(phi) * Mathf.Sin(theta);

        return new Vector3(x, y, z);
    }

    private IEnumerator MoveToTarget() {
        while (true) {
            // Move towards the target position
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            // Wait a moment at the target position
            yield return new WaitForSeconds(0.1f);

            // Get a new random target
            targetPosition = GetRandomTarget();
        }
    }
}
