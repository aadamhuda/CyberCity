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
	public TMPro.TMP_Dropdown resolutionDropdown;
	public Slider brightness;
	public Slider mousesens;
	public Slider volumeSlider;
	public CinemachineFreeLook cam;
	float currentVolume;
	Resolution[] resolutions;

	public void Start()
	{
		resolutionDropdown.ClearOptions();
		List<string> options = new List<string>();
		resolutions = Screen.resolutions;
		int currentResolutionIndex = -1;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " +
					 resolutions[i].height;
			options.Add(option);
			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			}
		}

		Debug.Log(currentResolutionIndex);

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.RefreshShownValue();
		LoadSettings(currentResolutionIndex);
	}

	public void SetVolume(Slider slider)
	{
		audioMixer.SetFloat("Volume", slider.value);
		currentVolume = slider.value;
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

	public void SetResolution(TMPro.TMP_Dropdown dropdown)
	{
		Resolution resolution = resolutions[dropdown.value];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetBrightness(Slider slider)
	{
		Screen.brightness = slider.value;
		Debug.Log(slider.value);
		Debug.Log(Screen.brightness);
	}

	public void SetSensitivity(Slider slider)
	{
		float sens = mousesens.value;
		cam.m_XAxis.m_MaxSpeed = 0.1f * sens;
		cam.m_YAxis.m_MaxSpeed = 0.001f * sens;
	}

	public void SaveSettings()
	{
		PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
		PlayerPrefs.SetFloat("BrightnessPreference", brightness.value);
		PlayerPrefs.SetFloat("MouseSensitivityPreference", mousesens.value);
		PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
		PlayerPrefs.SetFloat("VolumePreference", currentVolume);
	}

	public void LoadSettings(int currentResolutionIndex)
	{
		if (PlayerPrefs.HasKey("ResolutionPreference"))
			resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
		else
			resolutionDropdown.value = currentResolutionIndex;
		if (PlayerPrefs.HasKey("BrightnessPreference"))
			brightness.value = PlayerPrefs.GetFloat("BrightnessPreference");
		else
			brightness.value = Screen.brightness;
		if (PlayerPrefs.HasKey("MouseSensitivityPreference"))
			mousesens.value = PlayerPrefs.GetFloat("MouseSensitivityPreference");
		else
			mousesens.value = 0.5f;
		if (PlayerPrefs.HasKey("FullscreenPreference"))
			Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
		else
			Screen.fullScreen = true;
		if (PlayerPrefs.HasKey("VolumePreference"))
			volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
		else
			volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
	}
}
