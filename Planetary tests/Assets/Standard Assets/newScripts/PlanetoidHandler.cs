using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlanetoidHandler{

	private static List<GameObject> planetoids = new List<GameObject>();
	
	public static void addPlanetoid(GameObject planetoid){
		planetoids.Add (planetoid);
	}

	public static GameObject getClosestPlanetoid(float posx,float posy,float posz){
		//We get the planetoid with the shortest distance from it's surface to the point specified.
		float shortestDistance = float.MaxValue;
		GameObject closestPlanet = null;
		Vector3 position = new Vector3 (posx, posy, posz);
		for(int i = 0;i<planetoids.Count;i++){

			SphereCollider sphereCollider = (SphereCollider) planetoids[i].GetComponent (typeof(SphereCollider));
			float sphereRadius = Mathf.Max(sphereCollider.transform.lossyScale.x, sphereCollider.transform.lossyScale.x, sphereCollider.transform.lossyScale.x) * sphereCollider.radius;
			float distance = Mathf.Abs((position-planetoids[i].transform.position).magnitude) - sphereRadius;
			//In order to be valid the planetoid has to be closest than the distance of this constant to the point specified
			if((distance)<Constants.GRAVITY_DISTANCE_FROM_PLANET_FLOOR){
				if(distance<shortestDistance){
					shortestDistance = distance;
					closestPlanet = planetoids[i];
				}
			}
		}

		return closestPlanet;
	}



	public static GameObject getClosestPlanetoid(GameObject obj){
		return getClosestPlanetoid(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

	}
}
