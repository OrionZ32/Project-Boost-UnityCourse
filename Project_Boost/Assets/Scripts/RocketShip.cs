using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (state == State.Alive) {
            RespondToThrustInput();
            RespondToRotateInput();
        } 
    }

    void OnCollisionEnter(Collision collision) {
        
        if (state != State.Alive) {
            return;
        }

        switch(collision.gameObject.tag) {

            case "Friendly":
                //do nothing
                break;
            case "Win":
                WinSequence();
                break;
            default:
                DeathSequence();
                break;
        }
    }

    private void WinSequence() {
        audioSource.Stop();
        mainEngineParticles.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        state = State.Transcending;
        Invoke("LoadNextScene", 1f);
    }

    private void DeathSequence() {
        audioSource.Stop();
        mainEngineParticles.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        state = State.Dying;
        Invoke("ReloadFirstScene", 2f);
    }

    private void LoadNextScene() {
        SceneManager.LoadScene(1);
    }

    private void ReloadFirstScene() {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } 
        else {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust() {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            //prevents sound layering
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
                mainEngineParticles.Play();
    }

    private void RespondToRotateInput() {
        //takes manual control of rotation
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
        //resume physics control of rotation
        rigidBody.freezeRotation = false;
    }

}
