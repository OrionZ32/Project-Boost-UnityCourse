using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour {

    Rigidbody rigidBody;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        ProcessInput();
    }

    private void ProcessInput() {
        
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddForce(Vector3.up);
            
            //prevents sound layering
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } 
        
        else {
            audioSource.Stop();
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
        } 
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward);
        }
    }
}
