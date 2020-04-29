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

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (state == State.Alive) {
            ShipBoost();
            ShipRotate();
        }
    }

    void OnCollisionEnter(Collision collision) {
        
        if (state != State.Alive) {
            return;
        }

        switch(collision.gameObject.tag) {

            case "Friendly":
                // print("Ok");
                break;
            case "Win":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("ReloadFirstScene", 2f);
                break;
        }
    }

    private void LoadNextScene() {
        SceneManager.LoadScene(1);
    }

    private void ReloadFirstScene() {
        SceneManager.LoadScene(0);
    }

    private void ShipBoost() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            //prevents sound layering
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } 
        else {
            audioSource.Stop();
        }
    }

    private void ShipRotate() {
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
