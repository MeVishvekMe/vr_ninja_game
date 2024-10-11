using UnityEngine;

public class SpeedsterEnemy : MonoBehaviour {

    // Main Camera
    public Transform mainCamera;

    // Laser Spawn Points
    public Transform leftLeftPoint;
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform rightRightPoint;

    // Laser Prefab
    public GameObject laser;

    // Time for shooting laser
    private float _time = -20f;

    // Enemy Movement
    public float speed = 2f; // Speed of movement
    public Vector3 pointA = new Vector3(20f, 20f, 25f);
    public Vector3 pointB = new Vector3(-20f, 5f, 25f);
    private Vector3 targetPosition;

    private void Start() {
        mainCamera = Camera.main.transform;
    }

    private void Update() {
        transform.LookAt(mainCamera);
    }

}
