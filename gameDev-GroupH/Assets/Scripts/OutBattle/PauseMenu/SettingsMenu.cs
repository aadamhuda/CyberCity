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
	public Slider volumeSlider;
	public CinemachineFreeLook cam;
	float currentVolume;
	public void SetVolume(Slider slider)
	{
		audioMixer.SetFloat("Volume", slider.value);
		currentVolume = slider.value;
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
		PlayerPrefs.SetFloat("VolumePreference", currentVolume);
	}

	public void LoadSettings(int currentResolutionIndex)
	{

		if (PlayerPrefs.HasKey("MouseSensitivityPreference"))
			mousesens.value = PlayerPrefs.GetFloat("MouseSensitivityPreference");
		else
			mousesens.value = 0.5f;

		if (PlayerPrefs.HasKey("VolumePreference"))
			volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
		else
			volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
	}
}
