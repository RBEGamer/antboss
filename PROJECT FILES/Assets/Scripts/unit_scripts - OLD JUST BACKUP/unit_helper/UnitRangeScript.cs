﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitRangeScript : MonoBehaviour {

	public UnitBase thisUnit;
	public SphereCollider myCollider;
	// Use this for initialization
	void Awake () {
		myCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	bool canSeeUnit(Vector3 target) {
		Ray testRay =new Ray(thisUnit.transform.position, (target - thisUnit.transform.position));
		RaycastHit hit;
		if(Physics.Raycast(testRay, out hit, LayerMask.GetMask("Obstacle"))) {
			if(hit.collider.tag.Contains(vars.blockage_tag)) {
				Debug.DrawRay(thisUnit.transform.position, (target - thisUnit.transform.position), Color.magenta, 10.0f);
				return false;
			}
			else {
				Debug.DrawRay(thisUnit.transform.position, (target - thisUnit.transform.position), Color.green, 10.0f);
				return true;
			}
		}
		else {
			Debug.DrawRay(thisUnit.transform.position, (target - thisUnit.transform.position), Color.green, 10.0f);
			return true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(!canSeeUnit(other.transform.position)) {
			return;
		}
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(!canSeeUnit(other.transform.position)) {
				return;
			}
			if(other.gameObject != null) {
				thisUnit.addUnitInRange(other.gameObject);
			}
			/*
			if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
				if(!canSeeUnit(other.transform.position)) {
					return;
				}
				if(other.gameObject != null) {
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}

			if(other.gameObject.tag.Contains(vars.friendly_tag) && thisUnit.gameObject.tag.Contains(vars.enemy_tag)){
				if(!canSeeUnit(other.transform.position)) {
					return;
				}
				if(other.gameObject != null) {
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
			*/
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if(!canSeeUnit(other.transform.position)) {
			return;
		}
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(!canSeeUnit(other.transform.position)) {
				return;
			}
			if(other.gameObject != null) {
				thisUnit.addUnitInRange(other.gameObject);
			}
			/*
			if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
				if(!canSeeUnit(other.transform.position)) {
					return;
				}
				if(other.gameObject != null) {
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
			
			if(other.gameObject.tag.Contains(vars.friendly_tag) && thisUnit.gameObject.tag.Contains(vars.enemy_tag)){
				if(!canSeeUnit(other.transform.position)) {
					return;
				}
				if(other.gameObject != null) {
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
			*/
		}
	}

	/*void OnTriggerStay(Collider other) {
		if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
			//other.gameObject.SendMessage("setRenderer", true, SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnCollisionStay(Collision other) {
		if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
			//other.gameObject.SendMessage("setRenderer", true, SendMessageOptions.DontRequireReceiver);
		}
	}*/

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject != null) {
				thisUnit.removeUnitInRange(other.gameObject);
			}
		}

		/*
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
				if(other.gameObject != null) {
					thisUnit.removeEnemyInRange(other.gameObject);
				}
			}

			if(other.gameObject.tag.Contains(vars.friendly_tag) && thisUnit.gameObject.tag.Contains(vars.enemy_tag)){
				if(other.gameObject != null) {
					thisUnit.removeEnemyInRange(other.gameObject);
				}
			}
		}*/
	}

	void OnCollisionExit (Collision other)
	{
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject != null) {
				thisUnit.removeUnitInRange(other.gameObject);
			}
		}
		/*
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
				if(other.gameObject != null) {
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
			
			if(other.gameObject.tag.Contains(vars.friendly_tag) && thisUnit.gameObject.tag.Contains(vars.enemy_tag)){
				
				if(other.gameObject != null) {
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
		}
		*/
	}
}
