using UnityEngine;
using System.Collections;


public class CharacterController : MonoBehaviour {

	public float moveSpeed;
	public float normalJumpForce;
	public float spaceJumpForce;
	public float startChargeSpaceJump;
	public float readySpaceJump;


	private GravityBody body;
	private Vector3 moveAmount;
	private Vector3 smoothMoveVelocity;
	private float timeJumpPressed;
	private bool isSpaceJumping;
	private bool isSpaceJumpCharged;
	public GameObject particleSystemJumpCharge;

	void Start () {
		timeJumpPressed = 0;
		body = GetComponent<GravityBody> ();

	}

	void Update() {
		if (body.getIsTouchingPlanet ()) {

			float inputHorizontal = Input.GetAxisRaw ("Horizontal");
			//If there is horizontal input we stop the space jumping
			if(inputHorizontal!=0f){
				isSpaceJumpCharged = false;
				isSpaceJumping = false;
				timeJumpPressed = 0;
				ParticleSystem particules = particleSystemJumpCharge.GetComponent<ParticleSystem> ();
				particules.Stop ();
			}
			Vector3 moveDir = new Vector3 (Mathf.Abs (inputHorizontal), 0, 0).normalized;
			moveAmount = moveSpeed * moveDir;
			//If we change the character looking direction we change the characters orientation and we invert the z angle
			if (inputHorizontal > 0f) {
					if (transform.localEulerAngles.y < 90f) {
							transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
					} else {
							transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 0, 360f - transform.localEulerAngles.z);
					}
			} else if (inputHorizontal < 0f) {
					if (transform.localEulerAngles.y < 90f) {
							transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 180f, 360f - transform.localEulerAngles.z);
					} else {
							transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
					}
			}

			//If there's horizontal input we play the walking animation
			if (inputHorizontal != 0f) {
					animation.Play ();
			} else {
					animation.Stop ();
			}


			if (Input.GetKeyUp (KeyCode.Space)) {
					if (isSpaceJumpCharged == true) {
						rigidbody.AddForce (transform.up * spaceJumpForce, ForceMode.Impulse);
						//If we space jump we stop the particle system.
						ParticleSystem particules = particleSystemJumpCharge.GetComponent<ParticleSystem> ();
						particules.Stop ();
						
					} else {
						rigidbody.AddForce (transform.up * normalJumpForce, ForceMode.VelocityChange);
					}
			}

			if (Input.GetKey (KeyCode.Space)) {
					timeJumpPressed += Time.deltaTime;
					if (timeJumpPressed >= startChargeSpaceJump) {
							isSpaceJumping = true;
					}
					if (timeJumpPressed >= readySpaceJump) {
							//If the space jump is charged we play the jumping particle system
							isSpaceJumpCharged = true;
							ParticleSystem particles = particleSystemJumpCharge.GetComponent<ParticleSystem> ();
							particles.Play ();
					}
			} else {
					isSpaceJumpCharged = false;
					isSpaceJumping = false;
					timeJumpPressed = 0;
			}
		}
	}

	void FixedUpdate(){
		//Changed because the other way gave me some errors (Maurici)
		//rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
		Vector3 movement = transform.TransformDirection (moveAmount) * Time.fixedDeltaTime;
		this.transform.position = new Vector3(this.transform.position.x + movement.x,this.transform.position.y + movement.y,this.transform.position.z);
	}
}
