using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackButton : MonoBehaviour
{
	public Button button;
	public GameObject player;


	void Start()
	{
		Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
		playerAttack.doAttack();
	}
}

