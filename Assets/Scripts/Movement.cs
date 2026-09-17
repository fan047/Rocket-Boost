using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float ratateStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    //[SerializeField] float maxRotationAngle = 45f;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
        
    }

    void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopStrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        Debug.Log("按空格助推！");
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine, 2f);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    void StopStrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    
    void ProcessRotation()
    {
        float rotationInput =  rotation.ReadValue<float>();
        //Debug.Log("旋转值：" + rotationInput);
        if(rotationInput < 0)
        {
            RotateRight();
        }
        else if(rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            RotateStop();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(ratateStrength);
        if (!rightBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Stop();
            rightBoosterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-ratateStrength);
        if (!leftBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Stop();
            leftBoosterParticles.Play();
        }
    }

    private void RotateStop()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
