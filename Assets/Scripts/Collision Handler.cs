using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    bool isControllable;
    bool isCollidable;

    AudioSource audioSource;

    void Start()
    {
        isControllable = true;
        isCollidable = true;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            //Debug.Log("p has been pressed");
            isCollidable = !isCollidable;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(isControllable && isCollidable)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                //    Debug.Log("下一关！");
                    StartSuccessSequence();
                    break;
                case "Start":
                    Debug.Log("准备起飞！");
                    break;
                // case "Fuel":
                //    Debug.Log("好像是燃料？");
                //    Destroy(other.gameObject);
                //    break;
                default:
                //    Debug.Log("BOOM！！！");
                    StartCrashSequence();
                    
                    break;
            }
        }
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel" , levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel" , levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
        
    }
}
