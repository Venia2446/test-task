using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMovementController : MonoBehaviour
{
    public float speed = 5;

    public void Init(GameObject player)
    {
        Rigidbody = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    }

    private Rigidbody2D Rigidbody { get; set; }

}