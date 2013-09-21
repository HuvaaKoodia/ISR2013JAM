using UnityEngine;
using System.Collections;

public class CameraMain : MonoBehaviour {

	public Transform Target;
	public Vector3 Offset;
	public float distance=5,x_speed=1,y_speed=1,scroll_speed=4;
	public float min_dis=1,max_dis=200;
	
	float x=0,y=0,new_distance;
	
	Vector3 player_pos;
	Quaternion player_angle;

	void Start (){
		setDistance(distance);
		
		x=transform.rotation.eulerAngles.y;
		y=transform.rotation.eulerAngles.x;
	
		Rotate();
		transform.rotation = Quaternion.Euler(45,-45, 0);
	}
	
	bool in_orbit=false;
	
	void Update () {
		
		if (in_orbit){
			return;
		}
		
		if (moving_to_obj){
			float speed=cam_obj_move_speed;
		
			//speed=Mathf.Max(1,speed);
			
			speed=speed*Time.deltaTime;
			transform.position+=cam_obj_direction*speed;
			cam_obj_dis-=speed;
			
			cam_obj_move_speed-=Time.deltaTime*cam_obj_dis;
		
			if (cam_obj_dis<=0){
				if (moving_to_cam_object){
					in_orbit=true;
					moving_to_cam_object=false;
					moving_to_obj=false;
				}
				if (moving_to_player){
					moving_to_player=false;
					moving_to_obj=false;
				}
			}
			
			transform.rotation=Quaternion.Slerp(transform.rotation,rotation_target,cam_obj_rot_diff*Time.deltaTime);
			
			return;
		}
		
	 	if (Input.GetMouseButton(2)){
			Rotate();
		}
				
		var sw=Input.GetAxis("Mouse ScrollWheel");
		if (sw!=0){
			float multi=1f;
			if (Input.GetKey(KeyCode.LeftShift)){
				multi=3f;
			}

			setDistanceMulti(-sw*scroll_speed*multi);
		}
		
		if (distance!=new_distance){
			float dif=new_distance-distance;
			float dif_a=Mathf.Abs(dif);
			distance+=Mathf.Clamp(dif,-1,1)*Mathf.Max(1,dif_a*0.9f)*scroll_speed*Time.deltaTime;
		}
		
		if (Target){
			//Move the camera to look at the target
			var position = Target.position+Offset+ transform.rotation * Vector3.forward*-distance;
			transform.position = position;
			player_angle=transform.rotation;
			player_pos=position;
		}
	}
	
	void Rotate(){

		//Change the angles by the mouse movement
		x += Input.GetAxis("Mouse X") * x_speed * 0.02f;
		y -= Input.GetAxis("Mouse Y") * y_speed * 0.02f;
		
		y=Mathf.Clamp(y,0,90);
		
		//Rotate the camera correctly
		var rotation = Quaternion.Euler(y, x, 0);
		transform.rotation = rotation;
		
	}
	
	public void setDistance(float distance){
		distance=Mathf.Clamp(distance,min_dis,max_dis);
		new_distance=distance;
	}
	/// <summary>
	/// +||- multiplier
	/// </param>
	public void setDistanceMulti(float multiplier){
		setDistance(distance+distance*multiplier);
	}
	
	bool moving_to_obj=false,moving_to_cam_object=false,moving_to_player=false;
	Vector3 cam_obj_pos;
	float cam_obj_dis,cam_obj_odis,cam_obj_rot_diff;
	Vector3 cam_obj_direction;
	Quaternion rotation_target;
	
	float cam_obj_move_speed,cam_obj_min_speed=1;
	
	public void MoveToCameraPos(GameObject pos){
		in_orbit=false;
		moving_to_player=false;
		moving_to_obj=true;
		moving_to_cam_object=true;
		
		cam_obj_pos=pos.transform.position;
		
		cam_obj_direction=cam_obj_pos-transform.position;
		cam_obj_dis=cam_obj_odis=cam_obj_direction.magnitude;
		cam_obj_direction.Normalize();
		
		cam_obj_move_speed=cam_obj_odis+cam_obj_min_speed;
		
		cam_obj_rot_diff=2;//Vector3.Angle(transform.rotation.eulerAngles,cam_obj.transform.rotation.eulerAngles);
		
		rotation_target=pos.transform.rotation;
	}
	
	public void MoveToPlayerPos(){
		in_orbit=false;
		moving_to_cam_object=false;
		
		
		moving_to_obj=true;
		moving_to_player=true;
		cam_obj_pos=player_pos;
		
		cam_obj_direction=cam_obj_pos-transform.position;
		cam_obj_dis=cam_obj_odis=cam_obj_direction.magnitude;
		cam_obj_direction.Normalize();
		
		cam_obj_move_speed=cam_obj_odis+cam_obj_min_speed;
		
		cam_obj_rot_diff=4;
		
		rotation_target=player_angle;
	}
}
