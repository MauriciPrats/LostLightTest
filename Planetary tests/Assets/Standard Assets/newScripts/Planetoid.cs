using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planetoid : MonoBehaviour {

	//Speed in degrees that moves per second
	public float orbitalSpeed;

	//Center of the orbit
	public Vector3 orbitalCenter;

	//If not null this object will move around the father planetoid and not the orbit
	public GameObject fatherPlanetoid;

	private List<GameObject> planetoidSons = new List<GameObject>();

	private List<GameObject> objectsMovingWithPlanetoid = new List<GameObject>();
	// Use this for initialization
	void Start () {
		PlanetoidHandler.addPlanetoid (transform.root.gameObject);
		if (fatherPlanetoid != null) {
			Planetoid planetoid = (Planetoid) fatherPlanetoid.GetComponent (typeof(Planetoid));
			planetoid.addPlanetoidSon(transform.root.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Moves the planet on the orbit
		if (fatherPlanetoid != null) {
			orbitalCenter = fatherPlanetoid.transform.position;
		}
		//Calculate the new point in the rotation
		Vector3 newPoint = RotatePointAroundPivot (transform.position, orbitalCenter, new Vector3 (0, 0, orbitalSpeed*Time.deltaTime));
		Vector3 distanceMoved = newPoint - transform.position;
		transform.position = newPoint;
		moveAllObjects (distanceMoved);

		//Move all the objects of the satelites too
		if (planetoidSons != null) {
			for(int i = 0;i<planetoidSons.Count;i++){
				planetoidSons[i].transform.position = planetoidSons[i].transform.position+distanceMoved;
				Planetoid planetoid = (Planetoid) planetoidSons[i].GetComponent (typeof(Planetoid));
				planetoid.moveAllObjects(distanceMoved);
			}
		}
	}

	public void moveAllObjects(Vector3 distanceMoved){
		for (int i = 0; i<objectsMovingWithPlanetoid.Count; i++) {
			objectsMovingWithPlanetoid[i].transform.position = distanceMoved + objectsMovingWithPlanetoid[i].transform.position;
		}
	}

	public void addPlanetoidSon(GameObject planetoidSon){
		planetoidSons.Add (planetoidSon);
	}

	private Vector3 RotatePointAroundPivot(Vector3 point,Vector3 pivot,Vector3 angles){
		Vector3 dir = point - pivot; // get point direction relative to pivot
		dir = Quaternion.Euler(angles) * dir; // rotate it
		point = dir + pivot; // calculate rotated point
		return point; // return it
	}

	public void addObjectMovingWithPlanetoid(GameObject obj){
		if (!objectsMovingWithPlanetoid.Contains (obj)) {
			objectsMovingWithPlanetoid.Add(obj);
		}
	}

	public void removeObjectMovingWithPlanetoid(GameObject obj){
		if (objectsMovingWithPlanetoid.Contains (obj)) {
			objectsMovingWithPlanetoid.Remove(obj);
		}
	}
}
