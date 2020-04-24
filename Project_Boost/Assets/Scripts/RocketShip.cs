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
        ShipBoost();
        ShipRotate();
    }

    private void ShipBoost() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
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

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back);
        }
        //resume physics control of rotation
        rigidBody.freezeRotation = false;
    }

}
