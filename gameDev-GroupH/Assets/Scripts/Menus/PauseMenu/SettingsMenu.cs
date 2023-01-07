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
	float masterVolume;
	float musicVolume;
	float effectsVolume;
	
	// Adjusting master volume thorugh audiomixer and slider
    public void SetMasterVolume(Slider slider)
	{
		audioMixer.SetFloat("MasterVolume", slider.value);
		masterVolume = slider.value;
	}

	//Adjusting musci volume thorugh audiomixer and slider
	public void SetMusicVolume(Slider slider)
	{
		audioMixer.SetFloat("MusicVolume", slider.value);
		musicVolume = slider.value;
	}
	//Adjusting effects volume thorugh audiomixer and slider

	public void SetEffectsVolume(Slider slider)
	{
		audioMixer.SetFloat("EffectsVolume", slider.value);
		effectsVolume = slider.value;
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
		PlayerPrefs.SetFloat("MouseSensitivityPreference", mousesens.value);
		PlayerPrefs.SetFloat("MasterVolumePreference", masterVolume);
		PlayerPrefs.SetFloat("MusicVolumePreference", musicVolume);
		PlayerPrefs.SetFloat("EffectsVolumePreference", effectsVolume);
	}

	// Pre-initialising the values when loading
	public void LoadSettings()
	{

		if (PlayerPrefs.HasKey("MouseSensitivityPreference"))
			mousesens.value = PlayerPrefs.GetFloat("MouseSensitivityPreference");
		else
			mousesens.value = 0.5f;

		if (PlayerPrefs.HasKey("MasterVolumePreference"))
			masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolumePreference");
		else
			masterVolumeSlider.value = 0f;

		if (PlayerPrefs.HasKey("MusicVolumePreference"))
			musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolumePreference");
		else
			musicVolumeSlider.value = 0f;

		if (PlayerPrefs.HasKey("EffectsVolumePreference"))
			effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolumePreference");
		else
			effectsVolumeSlider.value = 0f;
	}
}
