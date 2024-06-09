using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
    public float ThrowForce = 100f;
    public float ThrowDirectionX = 0.17f;
    public float ThrowDirectionY = 0.67f;

    public AudioSource bounceSound;

    public Vector3 BallCameraOffset = new Vector3(0f, -0.4f, 2f);

    private Vector3 startPosition;
    private Vector3 direction;
    private float startTime;
    private float endTime;
    private float duration;
    private bool directionChosen = false;
    private bool throwStarted = false;

    Rigidbody rb;
    Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        ResetBall();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            startTime = Time.time;
            throwStarted = true;
            directionChosen = false;
        }

        if (Input.GetMouseButtonUp(0) && throwStarted)
        {
            direction = (Input.mousePosition - startPosition).normalized;
            duration = Time.time - startTime;
            directionChosen = true;
            throwStarted = false;
            endTime = Time.time;
        }

        if (directionChosen)
        {
            rb.mass = 1;
            rb.useGravity = true;
            rb.AddForce(mainCamera.transform.forward * ThrowForce / duration + mainCamera.transform.up * direction.y * ThrowDirectionY + mainCamera.transform.right * direction.x * ThrowDirectionX);

            // Reset variables
            startTime = 0.0f;
            duration = 0.0f;
            startPosition = Vector3.zero;
            direction = Vector3.zero;

            directionChosen = false;
        }

        if (Time.time - endTime >= 5 && Time.time - endTime < 6)
        {
            ResetBall();
        }
    }

    public void ResetBall()
    {
        rb.mass = 0;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        endTime = 0.0f;

        Vector3 ballPos = mainCamera.transform.position + mainCamera.transform.forward * BallCameraOffset.z + mainCamera.transform.up * BallCameraOffset.y + mainCamera.transform.right * BallCameraOffset.x;
        transform.position = ballPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounceSound.Play();
    }
}
