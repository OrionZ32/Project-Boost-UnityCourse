using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour {

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        ProcessInput();
    }

    private void ProcessInput() {
        
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A)) {
            print("rotating left");
        } 
        else if (Input.GetKey(KeyCode.D)) {
            print("rotating right");
        }
    }
}
