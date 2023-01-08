using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{

	public AudioMixer audioMixer;
	public Slider mousesens;
	public Slider masterVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider effectsVolumeSlider;
	public CinemachineFreeLook cam;


	bool battle;
	
	// Adjusting master volume thorugh audiomixer and slider
    public void SetMasterVolume(Slider slider)
	{
		audioMixer.SetFloat("MasterVolume", Mathf.Log10(slider.value)*20);
	}

	//Adjusting musci volume thorugh audiomixer and slider
	public void SetMusicVolume(Slider slider)
	{
		audioMixer.SetFloat("MusicVolume", Mathf.Log10(slider.value) * 20);
	}
	//Adjusting effects volume thorugh audiomixer and slider

	public void SetEffectsVolume(Slider slider)
	{
		audioMixer.SetFloat("EffectsVolume", Mathf.Log10(slider.value) * 20);
	}

	//Adjusting senseitivity by CMFreeLook

	public void SetSensitivity(Slider slider)
	{
		float sens = mousesens.value;
		cam.m_XAxis.m_MaxSpeed = 0.1f * sens;
		cam.m_YAxis.m_MaxSpeed = 0.001f * sens;
	}

	// SAving settings
	public void SaveSettings()
	{
		if(!battle)
			PlayerPrefs.SetFloat("MouseSensitivityPreference", mousesens.value);
		PlayerPrefs.SetFloat("MasterVolumePreference", masterVolumeSlider.value);
		PlayerPrefs.SetFloat("MusicVolumePreference", musicVolumeSlider.value);
		PlayerPrefs.SetFloat("EffectsVolumePreference", effectsVolumeSlider.value);
	}

	// Pre-initialising the values when loading
	public void LoadSettings(bool inBattle)
	{
		battle = inBattle;
		if (!inBattle)
        {
			if (PlayerPrefs.HasKey("MouseSensitivityPreference"))
				mousesens.value = PlayerPrefs.GetFloat("MouseSensitivityPreference");
			else
				mousesens.value = 0.5f;
		}
		
		if (PlayerPrefs.HasKey("MasterVolumePreference"))
			masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolumePreference");
		else
			masterVolumeSlider.value = 1f;

		if (PlayerPrefs.HasKey("MusicVolumePreference"))
			musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolumePreference");
		else
			musicVolumeSlider.value = 1f;

		if (PlayerPrefs.HasKey("EffectsVolumePreference"))
			effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolumePreference");
		else
			effectsVolumeSlider.value = 1f;
	}
}
