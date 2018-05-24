using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunusRing : MonoBehaviour {

    public GameObject partical;
    public GameObject destroyObj;
    [SerializeField]
    private float points = 1;
    public GameManager gm;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision) {
        
        GameObject destr = Instantiate(destroyObj, transform.position, destroyObj.transform.localRotation) as GameObject;
        GameObject part = Instantiate(partical, transform.position, partical.transform.rotation) as GameObject;
        
        destr.GetComponentInChildren<Rigidbody>().AddExplosionForce(100, collision.transform.position, 5f);
        gm.AddScore(points);

        Destroy(gameObject);

    }

}
