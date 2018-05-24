using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrouBonus : MonoBehaviour {

    [SerializeField] float lifeTime = 5f;
    float time;
    void Start () {
        time = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (time < lifeTime) time += Time.deltaTime;
        else Destroy(transform.gameObject);
	}
}
