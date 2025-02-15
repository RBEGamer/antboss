﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class sound_manager : MonoBehaviour {



public	GameObject audio_player_template;

	public float effect_volume = 1.0f;
	public float music_volume = 1.0f;
	public float master_volume = 1.0f;

	AudioClip bgmusic;
	// Use this for initialization
	void Awake () {
		this.name = vars.sound_manager_name;
		DontDestroyOnLoad(this);
		create_audio_source(vars.audio_name.bgmusic);

		try {
			effect_volume = PlayerPrefs.GetFloat("effect_volume");
			music_volume = PlayerPrefs.GetFloat("music_volume");
			master_volume = PlayerPrefs.GetFloat("master_volume");
		} catch (System.Exception ex) {
			Debug.Log("could not load data");
		}
	}



	public void create_audio_source(vars.audio_name audio_file){
		GameObject tmp;
		tmp = audio_player_template;
		audio_player_template.GetComponent<audio_clip_state>().start(audio_file);
		GameObject igo = (GameObject)Instantiate(tmp,new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
		igo.transform.parent = this.gameObject.transform;
	}

	public void create_audio_source_to_object(vars.audio_name audio_file, GameObject obj){
		GameObject tmp;
		tmp = audio_player_template;
		audio_player_template.GetComponent<audio_clip_state>().start(audio_file);
		GameObject igo = (GameObject)Instantiate(tmp,new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
		igo.transform.parent = obj.gameObject.transform;
	}




	public void change_effect_volume(float value){
		effect_volume = value;
		PlayerPrefs.SetFloat("effect_volume", value);
		PlayerPrefs.Save();
	}
	public void change_music_volume(float value){
		music_volume = value;
		PlayerPrefs.SetFloat("music_volume", value);
		PlayerPrefs.Save();
	}

	public void change_master_volume(float value){
		master_volume = value;
		PlayerPrefs.SetFloat("master_volume", value);
		PlayerPrefs.Save();
	}

	void FixedUpdate(){

		/*
		if(GameObject.Find("MANAGERS") != null){
			if(this.transform.parent != GameObject.Find("MANAGERS").transform){
				this.transform.parent = GameObject.Find("MANAGERS").transform;
			}
		}
*/
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.audio_clip_tag)) {
			if(n.GetComponent<audio_clip_state>() != null){


			//	if(n.GetComponent<audio_clip_state>().init){
			//	n.GetComponent<audio_clip_state>().manage_vol();
			//	}

			}else{
				Destroy(n.gameObject);
			}

		}
	}

}
