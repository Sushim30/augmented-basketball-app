using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject basketballPrefab;
    public Transform spawnPoint;
    public float throwForce = 10f;

    private GameObject basketballInstance;
    private Rigidbody rb;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                basketballInstance = Instantiate(basketballPrefab, spawnPoint.position, spawnPoint.rotation);
                rb = basketballInstance.GetComponent<Rigidbody>();
                Vector3 force = spawnPoint.forward * throwForce;
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
