using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonScript : MonoBehaviour
{
    public BattleInventory parent;
	public Button thisButton;
	public Player player;

	// Init
	public void Init() {
		thisButton.onClick.AddListener(TaskOnClick);
	}

    // Use item
    void TaskOnClick(){
		parent.UseItemOnPlayer(player);
	}
}
