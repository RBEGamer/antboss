﻿using UnityEngine;
using System.Collections;

public class audio_clip_state : MonoBehaviour {

	public bool is_playing = false;
	public bool is_looping = false;
	private float saved_vol  =1.0f;

	public vars.audio_playback_type type;

	private AudioSource asc;
	private AudioClip ac;

	// Use this for initialization
	void Awake () {

	//	type = vars.audio_playback_type.none;
		DontDestroyOnLoad(this);

		if(this.gameObject.GetComponent<AudioSource>() == null){
			this.gameObject.AddComponent<AudioSource>();
		}
		asc = this.gameObject.GetComponent<AudioSource>();

	}


	public void start(vars.audio_name audio_file){
	
		asc = this.gameObject.GetComponent<AudioSource>();
		switch (audio_file) {

		case vars.audio_name.bgmusic:
			 ac = (AudioClip)Resources.Load(vars.audio_clip_info_bgmusic.audio_clip_path);
			type = vars.audio_clip_info_bgmusic.ptype;
			saved_vol = vars.audio_clip_info_bgmusic.volume;
			asc.priority = vars.audio_clip_info_bgmusic.priority;
			asc.pitch = vars.audio_clip_info_bgmusic.pitch;
			break;
		case vars.audio_name.bgmusic2:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_bgmusic2.audio_clip_path);
			type = vars.audio_clip_info_bgmusic2.ptype;
			saved_vol = vars.audio_clip_info_bgmusic2.volume;
			asc.priority = vars.audio_clip_info_bgmusic2.priority;
			asc.pitch = vars.audio_clip_info_bgmusic2.pitch;
			break;
		case vars.audio_name.ui_click:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_uiclick.audio_clip_path);
			type = vars.audio_clip_info_uiclick.ptype;
			saved_vol = vars.audio_clip_info_uiclick.volume ;
			asc.priority = vars.audio_clip_info_uiclick.priority;
			asc.pitch = vars.audio_clip_info_uiclick.pitch;
			break;
		case vars.audio_name.wp_add:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_wp_add.audio_clip_path);
			type = vars.audio_clip_info_wp_add.ptype;
			saved_vol = vars.audio_clip_info_wp_add.volume ;
			asc.priority = vars.audio_clip_info_wp_add.priority;
			asc.pitch = vars.audio_clip_info_wp_add.pitch;
			break;
		case vars.audio_name.wp_move:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_wp_move.audio_clip_path);
			type = vars.audio_clip_info_wp_move.ptype;
			saved_vol = vars.audio_clip_info_wp_move.volume ;
			asc.priority = vars.audio_clip_info_wp_move.priority;
			asc.pitch = vars.audio_clip_info_wp_move.pitch;
			break;
		case vars.audio_name.wp_connect:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_wp_connect.audio_clip_path);
			type = vars.audio_clip_info_wp_connect.ptype;
			saved_vol = vars.audio_clip_info_wp_connect.volume ;
			asc.priority = vars.audio_clip_info_wp_connect.priority;
			asc.pitch = vars.audio_clip_info_wp_connect.pitch;
			break;
		case vars.audio_name.wp_remove_connect:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_wp_remove_connect.audio_clip_path);
			type = vars.audio_clip_info_wp_remove_connect.ptype;
			saved_vol = vars.audio_clip_info_wp_remove_connect.volume ;
			asc.priority = vars.audio_clip_info_wp_remove_connect.priority;
			asc.pitch = vars.audio_clip_info_wp_remove_connect.pitch;
			break;
		case vars.audio_name.unit_move_1:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_unit_move_1.audio_clip_path);
			type = vars.audio_clip_info_unit_move_1.ptype;
			saved_vol = vars.audio_clip_info_unit_move_1.volume ;
			asc.priority = vars.audio_clip_info_unit_move_1.priority;
			asc.pitch = vars.audio_clip_info_unit_move_1.pitch;
			break;
		case vars.audio_name.unit_move_2:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_unit_move_2.audio_clip_path);
			type = vars.audio_clip_info_unit_move_2.ptype;
			saved_vol = vars.audio_clip_info_unit_move_2.volume ;
			asc.priority = vars.audio_clip_info_unit_move_2.priority;
			asc.pitch = vars.audio_clip_info_unit_move_2.pitch;
			break;
		case vars.audio_name.unit_move_3:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_unit_move_3.audio_clip_path);
			type = vars.audio_clip_info_unit_move_3.ptype;
			saved_vol = vars.audio_clip_info_unit_move_3.volume ;
			asc.priority = vars.audio_clip_info_unit_move_3.priority;
			asc.pitch = vars.audio_clip_info_unit_move_3.pitch;
			break;
		case vars.audio_name.unit_move_4:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_unit_move_4.audio_clip_path);
			type = vars.audio_clip_info_unit_move_4.ptype;
			saved_vol = vars.audio_clip_info_unit_move_4.volume ;
			asc.priority = vars.audio_clip_info_unit_move_4.priority;
			asc.pitch = vars.audio_clip_info_unit_move_4.pitch;
			break;
		case vars.audio_name.unit_back_move_1:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_unit_back_move_1.audio_clip_path);
			type = vars.audio_clip_info_unit_back_move_1.ptype;
			saved_vol = vars.audio_clip_info_unit_back_move_1.volume ;
			asc.priority = vars.audio_clip_info_unit_back_move_1.priority;
			asc.pitch = vars.audio_clip_info_unit_back_move_1.pitch;
			break;
			break;
		case vars.audio_name.unit_back_move_2:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_unit_back_move_2.audio_clip_path);
			type = vars.audio_clip_info_unit_back_move_2.ptype;
			saved_vol = vars.audio_clip_info_unit_back_move_2.volume ;
			asc.priority = vars.audio_clip_info_unit_back_move_2.priority;
			asc.pitch = vars.audio_clip_info_unit_back_move_2.pitch;
			break;
		case vars.audio_name.unit_attack_move_1:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_unit_attack_move_1.audio_clip_path);
			type = vars.audio_clip_info_unit_attack_move_1.ptype;
			saved_vol = vars.audio_clip_info_unit_attack_move_1.volume ;
			asc.priority = vars.audio_clip_info_unit_attack_move_1.priority;
			asc.pitch = vars.audio_clip_info_unit_attack_move_1.pitch;
			break;
		case vars.audio_name.unit_attack_move_2:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_unit_attack_move_2.audio_clip_path);
			type = vars.audio_clip_info_unit_attack_move_2.ptype;
			saved_vol = vars.audio_clip_info_unit_attack_move_2.volume ;
			asc.priority = vars.audio_clip_info_unit_attack_move_2.priority;
			asc.pitch = vars.audio_clip_info_unit_attack_move_2.pitch;
			break;
		case vars.audio_name.select_base_1:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_select_base_1.audio_clip_path);
			type = vars.audio_clip_info_select_base_1.ptype;
			saved_vol = vars.audio_clip_info_select_base_1.volume ;
			asc.priority = vars.audio_clip_info_select_base_1.priority;
			asc.pitch = vars.audio_clip_info_select_base_1.pitch;
			break;
		case vars.audio_name.select_base_2:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_select_base_2.audio_clip_path);
			type = vars.audio_clip_info_select_base_2.ptype;
			saved_vol = vars.audio_clip_info_select_base_2.volume ;
			asc.priority = vars.audio_clip_info_select_base_2.priority;
			asc.pitch = vars.audio_clip_info_select_base_2.pitch;
			break;
		case vars.audio_name.select_unit:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_select_unit.audio_clip_path);
			type = vars.audio_clip_info_select_unit.ptype;
			saved_vol = vars.audio_clip_info_select_unit.volume ;
			asc.priority = vars.audio_clip_info_select_unit.priority;
			asc.pitch = vars.audio_clip_info_select_unit.pitch;
			break;
		case vars.audio_name.select_ressource:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_select_ressource.audio_clip_path);
			type = vars.audio_clip_info_select_ressource.ptype;
			saved_vol = vars.audio_clip_info_select_ressource.volume ;
			asc.priority = vars.audio_clip_info_select_ressource.priority;
			asc.pitch = vars.audio_clip_info_select_ressource.pitch;
			break;
		case vars.audio_name.select_waypoint:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_select_waypoint.audio_clip_path);
			type = vars.audio_clip_info_select_waypoint.ptype;
			saved_vol = vars.audio_clip_info_select_waypoint.volume ;
			asc.priority = vars.audio_clip_info_select_waypoint.priority;
			asc.pitch = vars.audio_clip_info_select_waypoint.pitch;
			break;
		case vars.audio_name.not_enought_ressources:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_not_enought_ressources.audio_clip_path);
			type = vars.audio_clip_info_not_enought_ressources.ptype;
			saved_vol = vars.audio_clip_info_not_enought_ressources.volume ;
			asc.priority = vars.audio_clip_info_not_enought_ressources.priority;
			asc.pitch = vars.audio_clip_info_not_enought_ressources.pitch;
			break;
		case vars.audio_name.destroy_skorpion_base:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_destroy_skorpion_base.audio_clip_path);
			type = vars.audio_clip_info_destroy_skorpion_base.ptype;
			saved_vol = vars.audio_clip_info_destroy_skorpion_base.volume ;
			asc.priority = vars.audio_clip_info_destroy_skorpion_base.priority;
			asc.pitch = vars.audio_clip_info_destroy_skorpion_base.pitch;
			break;
		case vars.audio_name.destroy_base:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_destory_base.audio_clip_path);
			type = vars.audio_clip_info_destory_base.ptype;
			saved_vol = vars.audio_clip_info_destory_base.volume ;
			asc.priority = vars.audio_clip_info_destory_base.priority;
			asc.pitch = vars.audio_clip_info_destory_base.pitch;
			break;



		default:
			ac = null;
			type = vars.audio_playback_type.none;
			asc.priority = 0;
			saved_vol = 0.0f;
			asc.pitch = 1.0f;
			break;
		}
		if(ac != null){
			asc.volume = saved_vol;
			manage_vol();
			this.name = "audio_playback_" + ac.name;
			
			asc.clip = ac;
			
			if(type == vars.audio_playback_type.music){
				asc.loop = true;
			}else{
				asc.loop = false;
			}
			
			asc.enabled = true;
			asc.Play();
		}else{
			this.name = "audio_playback_" + "null";
		}
	

	}



	public void manage_vol(){

		float master_vol = 1.0f;
		switch (type) {
		case vars.audio_playback_type.effect:
			master_vol = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().effect_volume;
			break;
		case vars.audio_playback_type.music:
			master_vol = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().music_volume;
			break;
			
		default:
			master_vol = 0.0f;
			break;
		}

		//Debug.Log(saved_vol);
		asc.volume = saved_vol * master_vol * GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().master_volume;





	}
	// Update is called once per frame
	void Update () {
		if(this.GetComponent<AudioSource>() != null){
		//	Debug.Log(true);
		
		is_playing = asc.isPlaying;
		is_looping = asc.loop;

		manage_vol();
		
			if((!asc.isPlaying && type == vars.audio_playback_type.effect) || type == vars.audio_playback_type.none){
				Destroy(this);
			}

	}
	}
}
