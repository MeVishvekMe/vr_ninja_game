using UnityEngine;
using UnityEngine.UI;

public class EnergyEnemy : MonoBehaviour {

    private float _time;
    public GameObject energyBall;
    public Transform spawnPoint;
    public Transform playerTransform;
    public Slider slider;

    void Start() {
        _time = -20f;
        playerTransform = Camera.main.transform;
    }

    void Update() {
        transform.LookAt(playerTransform);
        _time += Time.deltaTime;
        if(_time >= 5f) {
            GameObject gb = Instantiate(energyBall, spawnPoint.position, Quaternion.identity);
            Vector3 direction = -(spawnPoint.position - playerTransform.position).normalized;
            gb.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
            _time = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Bullet") {
            slider.value--;
            Destroy(other.gameObject);
        }
    }

}
