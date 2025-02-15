﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitGroupFriendlyScript : UnitGroupScript {
	private CapsuleCollider capsuleCollider;
	[SerializeField]
	public static UIUnitFighterSelection unitFighterUI;

	//public List<GameObject> baseList;
	public GameObject nearestBase;
	public Vector3 nearestBasePosition;


	public string unitGroupName = "";
	public float floatingDistance = 5.0f;
	private bool isGroupSelected = false;

	public bool startedLeaving = false;

	public bool isLowered = true;
	public bool acceptCommand = true;
	public void Start() {

		capsuleCollider = GetComponent<CapsuleCollider>();
		if(unitFighterUI == null) {
			unitFighterUI = GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>();
		}

		unitFighterUI.unitGroupList.Add(this);
		transform.rotation = new Quaternion(0, 180, 180, 1);

	}

	void Update() {
		updateInFight();
		cleanUp();
		//transform.rotation = new Quaternion (270.0f, 0.0f, 0.0f, 1.0f);
		if (Input.GetKeyDown(vars.key_panic) && isSelected())
		{
			setPanic();
		}

		transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
	}

	// issues retreat command
	public void setPanic()
	{
		findNearestBasePosition();
		moveToBase(unitFighterUI.baseList[0]);
		acceptCommand = false;
		transform.position = nearestBasePosition + Vector3.up * floatingDistance;

	}
	
	// rightclick(when selected)
	public void OnRightclick(Vector3 destination) {

		if(unitsInBaseScripts.Count > 0) {
			startedLeaving = true;
			return;
		}

		unitGroupAttackTarget = null;
		if (!acceptCommand || startedLeaving)
		{
			return;
		}
		else {
			if (capsuleCollider.bounds.Contains(destination + Vector3.up))
			{
				moveToDefensePoint(this.transform.position);
				return;
			}
			else
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.collider.gameObject.tag.Contains(vars.blockage_tag)) {
						return;
					}
					if (hit.collider.gameObject.tag.Contains(vars.enemy_tag) && hit.collider.gameObject.tag.Contains(vars.attackable_tag))
					{
						setTargetEnemy(hit.collider.gameObject);
						return;
					}
					
					if(hit.collider.gameObject.tag.Contains (vars.base_tag)) {
						findNearestBasePosition();
						moveToBase(unitFighterUI.baseList[0]);
						return;
					}
				} 
				placeNewDefensePoint(destination);
			}
		}
	}

	// a-move / standard right click
	public void placeNewDefensePoint(Vector3 destination)
	{
		transform.position = new Vector3(destination.x, destination.y, destination.z);
		isLowered = false;
		UnitScript nearestUnit = findNearestUnitTowardsDestination(destination);
		unitGroupCommand = UnitCommand.AttackMove;
		foreach (UnitScript t in unitsInGroupScripts)
		{
			t.unitTargetScript.resetTarget();
			t.movementScript.followTarget = null;
			/*if (t == nearestUnit)
			{*/
				t.movementScript.UpdateDestination(destination);
			/*
			}
			else
			{
				t.movementScript.followTarget = nearestUnit.gameObject;
			}*/
			
			t.currentCommand = UnitCommand.AttackMove;
			unitGroupCommand = UnitCommand.AttackMove;
		}
	}

	// retreat to base / double right click
	public void moveToBase(GameObject targetbase)
	{
		if(unitGroupCommand == UnitCommand.RetreatToBase) {
			acceptCommand = false;
		} else {
		UnitScript nearestUnit = findNearestUnitTowardsDestination(targetbase.transform.position);
		transform.position = targetbase.transform.position;
		unitGroupAttackTarget = targetbase;
		unitGroupCommand = UnitCommand.RetreatToBase;
		foreach (UnitScript t in unitsInGroupScripts)
		{
			/*t.unitTargetScript.resetTarget();
			t.movementScript.followTarget = null;
			if (t == nearestUnit)
			{*/
				Vector3 targetPosition = targetbase.transform.position + (Vector3.Normalize(transform.position - targetbase.transform.position) * 2.0f);
				t.movementScript.UpdateDestination(targetPosition);
			/*}
			else
			{
				t.movementScript.followTarget = nearestUnit.gameObject;
			}*/
			t.currentCommand = UnitCommand.RetreatToBase;
		}
		}
	}
	
	// retreat to defense point / double right click
	public void moveToDefensePoint(Vector3 destination)
	{
		UnitScript nearestUnit = findNearestUnitTowardsDestination(destination);
		unitGroupCommand = UnitCommand.Move;
		foreach (UnitScript t in unitsInGroupScripts)
		{

			t.movementScript.followTarget = null;
			if (t == nearestUnit)
			{
				t.movementScript.UpdateDestination(destination);
			}
			else
			{
				t.movementScript.followTarget = nearestUnit.gameObject;
			}
			t.currentCommand = UnitCommand.Move;
			t.unitTargetScript.resetTarget();
		}
	}

	public void setTargetEnemy(GameObject enemy) {
		transform.position = enemy.transform.position;
		unitGroupAttackTarget = enemy;
		unitGroupCommand = UnitCommand.Attack;
		foreach (UnitScript t in unitsInGroupScripts)
		{
			t.currentCommand = UnitCommand.Attack;
			t.unitTargetScript.attackTarget = enemy;
		}
	}

	// helper function
	public void findNearestBasePosition()
	{
		//baseList = myUnitGroupManager.baseList;
		if(unitFighterUI.baseList.Count > 0 ) {
			nearestBasePosition = unitFighterUI.baseList[0].transform.position;
			foreach (GameObject t in unitFighterUI.baseList)
			{
				if (Vector3.Distance(transform.position, nearestBasePosition) > Vector3.Distance(t.transform.position, nearestBasePosition))
				{
					nearestBasePosition = t.transform.position;
					nearestBase = t;
				}
			}
		} else {
			nearestBasePosition = Vector3.zero;
		}
	}

	// keep order in list
	public void cleanUp()
	{
		for(int i = unitsInGroup.Count - 1; i >= 0; i--) {
			if (unitsInGroup[i] == null)
			{
				unitsInGroup.RemoveAt(i);
			}
		}
		
		for(int i = unitsInGroupRange.Count - 1; i >= 0; i--) {
			if (unitsInGroupRange[i] == null)
			{
				unitsInGroupRange.RemoveAt(i);
			}
		}

		for(int i = unitsInGroupScripts.Count - 1; i >= 0; i--) {
			if (unitsInGroupScripts[i] == null)
			{
				unitsInGroupScripts.RemoveAt(i);
			}
		}

		for(int i = unitsInGroupRangeScripts.Count - 1; i >= 0; i--) {
			if (unitsInGroupRangeScripts[i] == null)
			{
				unitsInGroupRangeScripts.RemoveAt(i);
			}
		}
		
		if(unitsInGroup.Count == 0) {
			if(unitGroupCommand == UnitCommand.RetreatToBase) {
				nearestBase.GetComponent<UnitGroupCache>().addUnitGroup(this);
			}
			unitFighterUI.unitGroupList.Remove(this);
			Destroy(gameObject);
		}
	}

	public void OnSelected()
	{
		isGroupSelected = true;
		unitFighterUI.SelectedUnitGroup = this;
		GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_unit();
	}
	
	public void OnUnselected()
	{
		if (unitFighterUI.isAmove != true)
		{
			isGroupSelected = false;
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_empty();
			unitFighterUI.SelectedUnitGroup = null;	
		}
	}
	
	public bool isSelected() { return isGroupSelected;}
	
	public string getName() { return unitGroupName; }
}
