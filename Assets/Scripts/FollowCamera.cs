using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Camera myCamera;
    public GameObject player;
    public Vector3 cameraPosition;


    private void Start()
    {
        myCamera = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        cameraPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10f);

        Vector3 cameraMoveDir = (cameraPosition - transform.position).normalized;

        float distance = Vector3.Distance(cameraPosition, transform.position);
        float cameraMoveSpeed = 1f;

        transform.position = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float cameraZoom = 7f;

        myCamera.orthographicSize = cameraZoom;
    }
}
