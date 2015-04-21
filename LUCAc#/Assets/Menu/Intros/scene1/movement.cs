using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
private float t=1f;
private Vector3 start_rot;
private float extra_force=1f;
private bool coll=false;
	public Transform collide_p;
	public LensFlare lf;
	// Use this for initialization
	void Start () {
		
	start_rot = this.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		lf.brightness+=(1f-lf.brightness)/20f;
		t+=3f*Time.deltaTime;
		
	this.GetComponent<Rigidbody>().AddRelativeForce(transform.worldToLocalMatrix.MultiplyVector(this.transform.right)*Time.deltaTime*120f*extra_force);
		this.transform.eulerAngles = new Vector3(0f,0f,start_rot.z - Mathf.Sin(t)*30f-t*3f-(extra_force-1f)*10f);
		this.GetComponent<ParticleSystem>().emissionRate= 10f*(120f+Mathf.Sin(t)*80f)/2f;
		
		
		if (coll==true){
			PanWM.shake_value+=(0f-PanWM.shake_value)/20f;
		PanWM.shake_speed+=(1f-PanWM.shake_speed)/20f;
			
		}
	}
	
	 void OnTriggerStay(Collider hit) {
		Text_control.visible+=.1f;
       lf.brightness+=(2f-lf.brightness)/5f;
		
		extra_force+=.1f;
		
    }
	 void OnTriggerEnter(Collider hit) {
		PanWM.shake_value=1f;
		//GameObject collide = Instantiate(collide_p,this.transform.position,this.transform.rotation) as GameObject;
		lf.color = new Color(1f,202f/255f,135f/255f,0f);
	}
	 void OnTriggerExit(Collider hit) {
		coll=true;
			Text_control.collided=true;
		lf.color = new Color(118f/255f,107f/255f,1f,0f);
	}

}
