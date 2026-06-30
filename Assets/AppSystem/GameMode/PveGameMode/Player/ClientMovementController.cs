using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMovementController : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rigidbody;

    private void Update()
    {
        rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    }

}