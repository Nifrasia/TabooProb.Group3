using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float minZoomDist;
    [SerializeField] private float maxZoomDist;

    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomModifier;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform Corner1, Corner2;

    [SerializeField] private float rotationAmount;
    [SerializeField] private Quaternion newRotation;

    private Camera cam;

    public static CameraController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cam = Camera.main;

        newRotation = transform.rotation;
        rotationAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MoveByKB();
        Zoom();
        Rotate();
    }

    private void Zoom()
    {
        zoomModifier = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.Z))
            zoomModifier = 0.01f;
        if (Input.GetKey(KeyCode.X))
            zoomModifier = -0.01f;

        float dist = Vector3.Distance(transform.position, cam.transform.position);

        if (dist < minZoomDist && zoomModifier > 0f)
            return;
        else if (dist > maxZoomDist && zoomModifier < 0f)
            return;

        cam.transform.position += cam.transform.forward * zoomModifier * zoomSpeed;
    }
    private void MoveByKB()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * zInput + transform.right * xInput;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.position = Clamp(Corner1.position, Corner2.position);
    }
    Vector3 Clamp(Vector3 lowerLeft, Vector3 topRight)
    {
        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x, lowerLeft.x, topRight.x),
                                    transform.position.y,
                                    Mathf.Clamp(transform.position.z, lowerLeft.z, topRight.z));

        return pos;
    }
    void Rotate()
    {
        if (Input.GetKey(KeyCode.Q))
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);

        if (Input.GetKey(KeyCode.E))
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);

        if (Input.GetKey(KeyCode.Home))
            newRotation *= Quaternion.identity;

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * moveSpeed);
    }
}