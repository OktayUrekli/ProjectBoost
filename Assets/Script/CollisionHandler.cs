using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip finish, crash;
    [SerializeField] ParticleSystem finishParticles, crashParticles;
    AudioSource audioSource;
    ParticleSystem prtclSystem;
    bool isTransitioning=false;
    bool collisionDisabled = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        prtclSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        RespondToKeys();
    }
    void RespondToKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;

        if (isTransitioning || collisionDisabled) { return; }
       
            switch (tag)
            {
                case "Friendly":
                    Debug.Log("Go little burstad");
                    break;
                case "Finish":

                    StartDewamSequence();
                    break;
                case "Fuel":
                    Debug.Log("taked fuel");
                    break;
                default:

                    StartCrashSequence();
                    break;
            }
        
    }

    void StartCrashSequence()
    { 
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled= false;
        Invoke("ReloadLevel", 1f);
    }

    void StartDewamSequence()
    {
        isTransitioning =true;
        finishParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(finish);       
        GetComponent<Movement>().enabled= false;
        Invoke("LoadNextLevel", 1f);
    }

    void LoadNextLevel()
    {
        int sceneindex = SceneManager.GetActiveScene().buildIndex;
        if (sceneindex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneindex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
