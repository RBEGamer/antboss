﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitGroupScript : MonoBehaviour {
	public GameObject unitGroupAttackTarget = null;

	public List<GameObject> unitsInGroup;
	public List<UnitScript> unitsInGroupScripts;

	public List<UnitScript> unitsInBaseScripts;


	public List<GameObject> unitsInGroupRange;
	public List<UnitScript> unitsInGroupRangeScripts;

	public UnitCommand unitGroupCommand;

	public List<string> normalUpgrades;
	public string specialUpgrade;

	public bool isGroupInFight = false;


	[SerializeField]
	public Dictionary<string, float> currentSkillFlags;

	void Start() {

	}

	protected void updateInFight() {
		isGroupInFight = false;
		for(int i =0; i < unitsInGroupScripts.Count; i++) {
			if(unitsInGroupScripts[i].isInFight) {
				isGroupInFight = true;
			}
		}


	}

	public int getNumUnitsInRangeByFlag(string flag, UnitFaction targetfaction, bool includeOwn) {
		int returnNum = 0;
		for(int i =0; i < unitsInGroupRange.Count; i++) {
			if(unitsInGroupRange[i] != null) {
				if(unitsInGroupRange[i].gameObject.tag.Contains(flag) && unitsInGroupRange[i].GetComponent<FlagScript>().Faction == targetfaction) {
					returnNum++;
				}
			}
		}

		if(includeOwn) {
			returnNum += getNumUnitsByFlag(flag, targetfaction);
		}

		return returnNum;
	}

	public int getNumUnitsByFlag(string flag, UnitFaction targetfaction) {
		int returnNum = 0;
		for(int i =0; i < unitsInGroupScripts.Count; i++) {
			if(unitsInGroupScripts[i] != null) {
				if(unitsInGroupScripts[i].gameObject.tag.Contains(flag) && unitsInGroupScripts[i].flagScript.Faction == targetfaction) {
					returnNum++;
				}
			}
		}
		return returnNum;
	}


	// Update is called once per frame
	void Update () {
		cleanUp();
		updateInFight();
	}

	public void passSkillResultToGroup(SkillResult skillResult, GameObject attacker) {
		if(skillResult.skillType == SkillResult.SkillType.Buff) {
			if(!currentSkillFlags.ContainsKey(skillResult.skillFlag)) {
				currentSkillFlags.Add(skillResult.skillFlag, skillResult.skillFlagTimer);
			}
			for(int i = 0; i < unitsInGroup.Count; i++) {
				SkillCalculator.passSkillResult(attacker, unitsInGroup[i], skillResult);
			}
			return;
		}
		else if(skillResult.skillType == SkillResult.SkillType.RemoveBuff) {
			if(currentSkillFlags.ContainsKey(skillResult.skillFlag)) {
				currentSkillFlags.Remove(skillResult.skillFlag);
			}
			for(int i = 0; i < unitsInGroup.Count; i++) {
				SkillCalculator.passSkillResult(attacker, unitsInGroup[i], skillResult);
			}
			return;
		}

		for(int i = 0; i < unitsInGroup.Count; i++) {
			SkillCalculator.passSkillResult(attacker, unitsInGroup[i], skillResult);
		}

		return;
	}

	public bool changeCommand(UnitCommand newCommand) {
		if(newCommand <= unitGroupCommand) {
			unitGroupCommand = newCommand;
			return true;
		}
		return false;
	}

	// helper function
	public UnitScript findNearestUnitTowardsDestination(Vector3 destination) {
		cleanUp();
		UnitScript nearestUnit = unitsInGroupScripts[0];
		foreach (UnitScript t in unitsInGroupScripts)
		{
			if (Vector3.Distance(nearestUnit.transform.position, destination) > Vector3.Distance(t.transform.position, destination))
			{
				nearestUnit = t;
			}
		}
		
		return nearestUnit;
	}
	
	public void addUnitToRange(GameObject enemy) {
		if(!unitsInGroupRange.Contains(enemy) && !unitsInGroup.Contains(enemy)) {
			unitsInGroupRange.Add (enemy);
			UnitScript enemyUnitScript = enemy.GetComponent<UnitScript>();
			if(enemyUnitScript != null) {
				unitsInGroupRangeScripts.Add(enemyUnitScript);
			}
		}
	}
	
	public void removeUnitFromRange(GameObject enemy) {
		if(!unitsInGroup.Contains(enemy)) {
			bool last = true;
			for(int i = 0; i < unitsInGroup.Count; i++) {
				if(unitsInGroup[i].GetComponent<UnitVision>() != null) {
					if(unitsInGroup[i].GetComponent<UnitVision>().objectsInRange.Contains(enemy)) {
						last = false;
					}
				}
			}

			if(last) {
				unitsInGroupRange.Remove(enemy);
				UnitScript enemyUnitScript = enemy.GetComponent<UnitScript>();
				if(enemyUnitScript != null) {
					unitsInGroupRangeScripts.Remove(enemyUnitScript);
				}
			}
		}
	}

	public void addUnit(GameObject unit) {
		if (!unitsInGroup.Contains(unit)) {
			unitsInGroup.Add(unit);
			unitsInGroupScripts.Add(unit.GetComponent<UnitScript>());
		}
		
	}
	
	public void removeUnit(GameObject unit) {
		if (unitsInGroup.Contains(unit)) {
			unitsInGroup.Remove(unit);
			unitsInGroupScripts.Remove(unit.GetComponent<UnitScript>());
		}
	}

	public void addUnit(UnitScript unit) {
		if (!unitsInGroupScripts.Contains(unit)) {
			unitsInGroup.Add(unit.gameObject);
			unitsInGroupScripts.Add(unit);
		}
		
	}
	
	public void removeUnit(UnitScript unit) {
		if (unitsInGroupScripts.Contains(unit)) {
			unitsInGroup.Remove(unit.gameObject);
			unitsInGroupScripts.Remove(unit);
		}
	}

	public void addUnitToBaseList(UnitScript unit) {
		if (unitsInGroupScripts.Contains(unit)) {
			unitsInGroup.Remove(unit.gameObject);
			unitsInGroupScripts.Remove(unit);
			unitsInBaseScripts.Add(unit);
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

		for(int i = unitsInGroupScripts.Count - 1; i >= 0; i--) {
			if (unitsInGroupScripts[i] == null)
			{
				unitsInGroupScripts.RemoveAt(i);
			}
		}
		
		for(int i = unitsInGroupRange.Count - 1; i >= 0; i--) {
			if (unitsInGroupRange[i] == null)
			{
				unitsInGroupRange.RemoveAt(i);
			}
		}

		for(int i = unitsInGroupRangeScripts.Count - 1; i >= 0; i--) {
			if (unitsInGroupRangeScripts[i] == null)
			{
				unitsInGroupRangeScripts.RemoveAt(i);
			}
		}



		if(unitsInGroup.Count == 0) {
			Destroy(gameObject);
		}
	}
}
