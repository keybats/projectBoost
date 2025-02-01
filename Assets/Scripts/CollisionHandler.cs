using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelay;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip reachedGoal;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem succesParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool dieOnCollision = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && dieOnCollision) 
        {
            dieOnCollision = false;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            dieOnCollision = true;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }
        switch (collision.gameObject.tag)
        {
            
            case "Friendly":
                Debug.Log("that was friendly");
                break;
            case "Finish":

                

                StartLoadNextLevel();
                
                break;
            default:



                if (dieOnCollision) { StartCrashSequence(); } 
                
                break;
        }
    }
    
    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;

        audioSource.Stop();
        audioSource.PlayOneShot(deathExplosion);

        crashParticles.Play();

        isTransitioning = true;
        
        Invoke("ReloadScene", loadDelay);

    }

    void StartLoadNextLevel()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(reachedGoal);
        isTransitioning = true;

        succesParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadDelay);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
