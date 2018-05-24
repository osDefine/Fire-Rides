using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEvents : MonoBehaviour {

    public delegate void EnableObject();

    void OnCollisionEnter(Collision collision) {
        Destroy(this.gameObject);
    }
}
