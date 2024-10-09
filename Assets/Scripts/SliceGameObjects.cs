using UnityEngine;
using UnityEngine.InputSystem;
using EzySlice;

public class SliceGameObjects : MonoBehaviour {

    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public LayerMask sliceableLayer;
    public VelocityEstimator velocityEstimator;
    public Material slicedMat;



    public GameObject saber;
    private float saberSize = 0f;
    private float saberSpeed = 3f;

    //public Transform planeDebug;
    //public GameObject cube;

    public bool swordState = false;


    public InputActionReference inputActions;
    private void Start() {
        //Slice(cube);
    }

    private void OnEnable() {
        
        inputActions.action.Enable();
        inputActions.action.performed += ChangeSaberState;
    }

    private void OnDisable() {
        inputActions.action.Disable();
        inputActions.action.performed -= ChangeSaberState;
    }

    private void Update() {
        
        bool hashHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hashHit && swordState) {
            GameObject target = hit.transform.gameObject;
            Debug.Log("Hit " + target.name);
            Slice(target);
        }

        if (swordState) {
            SaberOn();
        }
        else {
            SaberOff();
        }
    }

    public void Slice(GameObject target) {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        //SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);
        Debug.Log("Slicing Start");
        if (hull != null) {
            Debug.Log("Slicing");
            GameObject upperHull = hull.CreateUpperHull();
            upperHull.transform.position = target.transform.position;
            upperHull.transform.localScale = target.transform.localScale;
            SetSlicedObject(upperHull);
            Destroy(upperHull, 2.5f);
            GameObject lowerHull = hull.CreateLowerHull();
            lowerHull.transform.position = target.transform.position;
            lowerHull.transform.localScale = target.transform.localScale;
            SetSlicedObject(lowerHull);
            Destroy(lowerHull, 2.5f);
            Destroy(target);
        }
    }

    private void ChangeSaberState(InputAction.CallbackContext context) {
        swordState = !swordState;
    }

    private void SaberOn() {
        saberSize = Mathf.Lerp(saberSize, 1f, saberSpeed * Time.deltaTime);
        //Debug.Log("Saber On");
        if (saberSize >= 0.98f) {
            saberSize = 1f;
        }
        saber.transform.localScale = new Vector3(saber.transform.localScale.x, saberSize, saber.transform.localScale.z);
    }

    private void SaberOff() {
        saberSize = Mathf.Lerp(saberSize, 0f, saberSpeed * Time.deltaTime);
        //Debug.Log("Saber Off");
        if (saberSize <= 0.03) {
            saberSize = 0f;
        }
        saber.transform.localScale = new Vector3(saber.transform.localScale.x, saberSize, saber.transform.localScale.z);
    }

    private void SetSlicedObject(GameObject obj) {
        MeshCollider mc = obj.AddComponent<MeshCollider>();
        mc.convex = true;
        Material[] materials = new Material[3];
        materials[0] = slicedMat;
        materials[1] = slicedMat;
        materials[2] = slicedMat;
        obj.GetComponent<MeshRenderer>().materials = materials;
        obj.AddComponent<Rigidbody>();
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        obj.GetComponent<Rigidbody>().AddExplosionForce(4000f, obj.transform.position, 1);
        //obj.layer = sliceableLayer;
    }
}
