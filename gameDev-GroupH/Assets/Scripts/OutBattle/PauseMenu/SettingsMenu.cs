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

	public void Update()
	{
		
	}
    public void SetMasterVolume(Slider slider)
	{
		audioMixer.SetFloat("MasterVolume", slider.value);
		masterVolume = slider.value;
	}
	public void SetMusicVolume(Slider slider)
	{
		audioMixer.SetFloat("MusicVolume", slider.value);
		musicVolume = slider.value;
	}
	public void SetEffectsVolume(Slider slider)
	{
		audioMixer.SetFloat("EffectsVolume", slider.value);
		effectsVolume = slider.value;
	}

	public void SetSensitivity(Slider slider)
	{
		float sens = mousesens.value;
		cam.m_XAxis.m_MaxSpeed = 0.1f * sens;
		cam.m_YAxis.m_MaxSpeed = 0.001f * sens;
	}

	public void SaveSettings()
	{
		PlayerPrefs.SetFloat("MouseSensitivityPreference", mousesens.value);
		PlayerPrefs.SetFloat("MasterVolumePreference", masterVolume);
		PlayerPrefs.SetFloat("MusicVolumePreference", musicVolume);
		PlayerPrefs.SetFloat("EffectsVolumePreference", effectsVolume);
	}

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
			effectsVolumeSlider.value = 1f;
	}
}
