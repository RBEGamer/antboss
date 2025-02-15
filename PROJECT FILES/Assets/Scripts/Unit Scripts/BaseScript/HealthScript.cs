﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public bool hasShield = false;
	public float BaseShield = 0.0f;
	public float CurrentShield = 0.0f;

	public bool hasHealth = true;
	public float BaseHealth = 100.0f;
	public float CurrentHealth = 100.0f;

	public bool isRegenerating = true;


	private UnitScript unitScript;


	public GameObject shieldObject;

    public HealthbarScript healthbarScript;

	public float currentShieldCooldown;

	void Awake() {
		unitScript = GetComponent<UnitScript>();
		resetHealth();

       
	}

    void Start()
    {
    }

	void Update() {
		checkDeath();

		if(currentShieldCooldown > 0.0f) {
			currentShieldCooldown -= Time.deltaTime;
		} else if(shieldObject != null && shieldObject.activeSelf) {
			shieldObject.SetActive(false);
		}
	}

	void OnSkillResult(SkillResult skillresult) {
		if(skillresult.skillType == SkillResult.SkillType.Damage) {
			float removedFromShield = 0;

			if(CurrentShield > 0) {
				removedFromShield = Mathf.Abs(CurrentShield - skillresult.skillPower);
				if(shieldObject != null) {
					shieldObject.SetActive(true);
					currentShieldCooldown = 3.0f;
				}
			}

			CurrentHealth = Mathf.Clamp(CurrentHealth - (skillresult.skillPower - removedFromShield), -1.0f, BaseHealth);
			if(unitScript != null) {
				unitScript.setIsInFight(true);
			}
		}

		else if(skillresult.skillType == SkillResult.SkillType.Heal) {
			CurrentHealth = Mathf.Clamp(CurrentHealth + skillresult.skillPower, -1.0f, BaseHealth);
		}

		else if(skillresult.skillType == SkillResult.SkillType.Buff) {
			if(skillresult.skillAttribute == SkillResult.SkillAttribute.Shield) {
				BaseShield += skillresult.skillPower;
				CurrentShield = BaseShield;
			}

			if(skillresult.skillAttribute == SkillResult.SkillAttribute.Health) {
				BaseHealth += skillresult.skillPower;
				CurrentHealth = BaseHealth;
			}
		}

		if(skillresult.skillFlag != "") {
			if(!unitScript.flagScript.currentSkillFlags.ContainsKey(skillresult.skillFlag)) {
				unitScript.flagScript.currentSkillFlags.Add(skillresult.skillFlag, skillresult.skillFlagTimer);
			}
		}

		checkDeath();
	}

	public void regenerate(float healthRegen, float shieldRegen) {
		if(hasHealth) {
			CurrentHealth = Mathf.Clamp(CurrentHealth + healthRegen * Time.deltaTime, 0.0f, BaseHealth);
		}

		if(hasShield) {
			CurrentShield = Mathf.Clamp(CurrentShield + shieldRegen * Time.deltaTime, 0.0f, BaseShield);
		}
	}


	void resetHealth() {
		CurrentShield = BaseShield;
		CurrentHealth = BaseHealth;
	}

	void checkDeath() {
		if(CurrentHealth <= 0) {
			hasHealth = false;
            unitScript.animator.SetTrigger("death");

            this.enabled = false;
            if (unitScript.flagScript.Faction == UnitFaction.EnemyFaction)
            {
                SendMessage("destroyObject", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
            
		}
	}
}
