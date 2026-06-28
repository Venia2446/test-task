using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMovementController : MonoBehaviour
{
    public float speed = 5;

    public void Init(GameObject player)
    {
        rigidbody = player.GetComponent<Rigidbody2D>();
    }

    public void Terminate() { }

    private void Update()
    {
        rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    }

    private Rigidbody2D rigidbody;
}