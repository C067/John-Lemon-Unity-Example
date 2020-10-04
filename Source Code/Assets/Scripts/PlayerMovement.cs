using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private Rigidbody rb;
    private Animator anim;
    private AudioSource audioSource;

    //Variables 
    Vector3 movement;
    Quaternion rotation = Quaternion.identity;
    public float turnSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        anim.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * anim.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
