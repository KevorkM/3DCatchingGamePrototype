using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

public delegate void OnCollectBallAction();

public OnCollectBallAction OnCollectBall;

float speed = 600f;

public Camera mainCamera;

Vector3 leftBound;
Vector3 rightBound;

	void Start () {

		//setting up the bounds
        leftBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, -mainCamera.transform.localPosition.z));
        rightBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, -mainCamera.transform.localPosition.z));
	}
	
	void Update () {
		keepInBounds();
		ProcessInput();
		lockPlayerZPosition();
	}

	void lockPlayerZPosition(){
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
	}

	void keepInBounds(){
        if (this.transform.position.x < leftBound.x)
        {
            this.transform.position = new Vector3(leftBound.x, this.transform.position.y, this.transform.position.z);
        
		if (this.transform.position.y < leftBound.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, leftBound.y, this.transform.position.z);
		}
        if (this.transform.position.x > rightBound.x)
            {
                this.transform.position = new Vector3(rightBound.x, this.transform.position.y, this.transform.position.z);

        if (this.transform.position.y < rightBound.y)
                {
                    this.transform.position = new Vector3(this.transform.position.x, rightBound.y, this.transform.position.z);
                }
		}	
	}
}
void ProcessInput()
    {
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.right * speed * Time.deltaTime);
        }

	}
	public void OnCollisionEnter(Collision collision){
		if (collision.gameObject.GetComponent<BallController>() !=null){
			if (OnCollectBall != null){
				OnCollectBall();
			}
			GameObject.Destroy (collision.gameObject);
		}
	}
}