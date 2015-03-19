using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {
	private Transform followTarget;
	private float moveSpeed; 
	private float turnSpeed; 
	private bool isZombie;
	private Vector3 targetPosition;

	void GrantCatTheSweetReleaseOfDeath()
	{
		DestroyObject( gameObject );
	}

	public void OnBecameInvisible() {
		if ( !isZombie )
		Destroy( gameObject ); 
	}

	public void JoinConga( Transform followTarget, float moveSpeed, float turnSpeed ) {
		
		//2
		this.followTarget = followTarget;
		this.moveSpeed = moveSpeed * 2f;
		this.turnSpeed = turnSpeed;
		targetPosition = followTarget.position;
		//3
		isZombie = true;
		
		//4
		Transform cat = transform.GetChild (0);
		cat.collider2D.enabled = false;
		cat.GetComponent<Animator>().SetBool( "InConga", true );
	}

	public void UpdateTargetPosition(){
		targetPosition = followTarget.position;
	}

	public void ExitConga(){
		Vector3 cameraPos = Camera.main.transform.position;
		var randomXRange = Random.Range (-1.5f, 1.5f);
		var randomYRange = Random.Range (-1.5f, 1.5f);

		targetPosition = new Vector3 (cameraPos.x + randomXRange, cameraPos.y + randomYRange, followTarget.position.z);

		Transform cat = transform.GetChild (0);
		cat.GetComponent<Animator> ().SetBool ("InConga", false);
	}

	void Update () {
		//1
		if(isZombie)
		{
			//2
			Vector3 currentPosition = transform.position;            
			Vector3 moveDirection = targetPosition - currentPosition;
			
			//3
			float targetAngle = 
				Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp( transform.rotation, 
			                                      Quaternion.Euler(0, 0, targetAngle), 
			                                      turnSpeed * Time.deltaTime );
			
			//4
			float distanceToTarget = moveDirection.magnitude;
			if (distanceToTarget > 0)
			{
				//5
				if ( distanceToTarget > moveSpeed )
					distanceToTarget = moveSpeed;
				
				//6
				moveDirection.Normalize();
				Vector3 target = moveDirection * distanceToTarget + currentPosition;
				transform.position = 
					Vector3.Lerp(currentPosition, target, moveSpeed * Time.deltaTime);
			}
		}
	}
}
