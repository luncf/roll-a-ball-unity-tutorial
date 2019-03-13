using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text winText;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        winText.text = "";
    }

    private void FixedUpdate()
    {
        // Update Player Location (Physics)

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);

            PickupController pickupController = other.gameObject.transform.parent.gameObject.GetComponent<PickupController>();
            pickupController.DestroyObject();
            if (!pickupController.HasRemainingObject())
            {
                winText.text = "You win!";
            }
        }
    }
}
