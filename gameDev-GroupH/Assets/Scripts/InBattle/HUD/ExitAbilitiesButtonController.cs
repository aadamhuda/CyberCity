using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitAbilitiesButtonController : MonoBehaviour
{
	public BattleSystem battleSystem;
	public Button thisButton;

	// Init
	public void Start()
	{
		battleSystem = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent<BattleSystem>();
		thisButton.onClick.AddListener(TaskOnClick);
	}

	// Use item
	void TaskOnClick()
	{
		battleSystem.OnExitAbilityButton();
	}
}
