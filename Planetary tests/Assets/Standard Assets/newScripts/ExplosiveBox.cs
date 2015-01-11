using UnityEngine;
using System.Collections;

public class ExplosiveBox : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Skill") {
			Debug.Log("IsHit");
			Destroy(this);
		}
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Character") {

		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
