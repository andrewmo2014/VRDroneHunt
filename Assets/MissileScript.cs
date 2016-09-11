using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {

    public float speed = 3f;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward*100f, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 pos = this.transform.position;
        //this.transform.position = Vector3.MoveTowards(pos, this.transform.forward, speed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision c)
    {
        GameObject newExplosion = (GameObject)Instantiate(explosion);
        this.GetComponent<Rigidbody>().AddExplosionForce(2f, this.transform.position, 3f);
        newExplosion.transform.position = this.transform.position;
        Destroy(newExplosion, 4f);
        Destroy(this.gameObject);
    }
}
