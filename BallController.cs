using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

public delegate void OnKillAction();

public OnKillAction OnKill;

float lifeTime = 4f;
float randomForce = 200f;
float timer;

	void Start () {

		this.GetComponent<Rigidbody>().AddForce(Vector3.right*Random.Range(-randomForce,randomForce)); 
		
	}
	
	void Update () {
		TimerSetUp();
		
	}
		void TimerSetUp(){
			timer += Time.deltaTime;
		
		if (timer>lifeTime/2){
			//make this either faster or slower
			float percentage = 1 -(timer-lifeTime/2)/(lifeTime/2);

			this.transform.localScale = Vector3.one*percentage;
		}

		if(timer>lifeTime){
			GameObject.Destroy(this.gameObject);
		}
	}
}
