using UnityEngine;
using System.Collections;

public class MovesOnPlanetoids : MonoBehaviour {

	private GameObject closestPlanetoid;
	private bool hasPlanetBeenTouched;

	void Start () {
		hasPlanetBeenTouched = true;
		closestPlanetoid = PlanetoidHandler.getClosestPlanetoid(transform.root.gameObject);
	}

	void Update () {
		//Remove from the actual planetoid
		if (closestPlanetoid != null) {
			Planetoid planetoid = (Planetoid) closestPlanetoid.GetComponent (typeof(Planetoid));
			planetoid.removeObjectMovingWithPlanetoid (transform.root.gameObject);
		}
		//Find new closest planetoid
		closestPlanetoid = PlanetoidHandler.getClosestPlanetoid(transform.root.gameObject);

		//If there is a new one, move the object and apply the force of gravity
		if (closestPlanetoid != null) {
			Planetoid planetoidNew = (Planetoid)closestPlanetoid.GetComponent (typeof(Planetoid));
			planetoidNew.addObjectMovingWithPlanetoid (transform.root.gameObject);
		
			Vector3 distance = transform.position - closestPlanetoid.transform.position;

			//gravity of the planet
			rigidbody.AddForce(-distance.normalized * Constants.GRAVITY_FORCE_OF_PLANETS, ForceMode.VelocityChange);

			//Put the right orientation
			//Vector3 forward = transform.forward;
			Vector3 oldAngles = transform.eulerAngles;
			transform.up = distance.normalized;

			//oldAngles.z = transform.eulerAngles.z;
			if(oldAngles.y>90f){
				oldAngles.z = 360f-transform.eulerAngles.z;
			}else{
				oldAngles.z = transform.eulerAngles.z;
			}
			transform.eulerAngles = oldAngles;

			//transform.forward = forward;
		}
	}
}
