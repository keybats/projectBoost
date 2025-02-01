using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 1;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] AudioClip thrusterSounds;

    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem rightThruster;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrusterSounds);
        }

        if (!mainThruster.isEmitting)
        {
            mainThruster.Play();
        }

        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    
    private void StopThrusting()
    {
        mainThruster.Stop();
        audioSource.Stop();
    }



    void RotateLeft()
    {
        Rotate(rotationSpeed);
        if (!rightThruster.isEmitting)
        {
            rightThruster.Play();
        }
    }

    private void RotateRight()
    {
        Rotate(-rotationSpeed);
        if (!leftThruster.isEmitting)
        {
            leftThruster.Play();
        }
    }

    private void StopRotating()
    {
        leftThruster.Stop();
        rightThruster.Stop();
    }

    void Rotate(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
