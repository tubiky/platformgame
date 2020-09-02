using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool m_moveRight = true;

    public Transform groundDetection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false)
        {
            if (m_moveRight == true)
            {
                transform.Rotate(0f, 180f, 0);
                // transform.eulerAngles = new Vector3(0, -180, 0);
                m_moveRight = false;

            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                m_moveRight = true;
            }
        }
    }
}
