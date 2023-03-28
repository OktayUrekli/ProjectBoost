using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust=1000f;
    [SerializeField] float rotationThrust=100f;
    [SerializeField] ParticleSystem  mainBooster, leftBooster, rightBooster;
    AudioSource audioSource;
    ParticleSystem prtclSystem;
    Rigidbody rb;

    void Start()
    {
        prtclSystem = GetComponent<ParticleSystem>();
        rb=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                mainBooster.Play();
                audioSource.Play();
            }        
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rightBooster.Play();
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            leftBooster.Play();
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
