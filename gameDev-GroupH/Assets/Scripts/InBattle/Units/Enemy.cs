using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from parent class BattleUnit
public class Enemy : BattleUnit
{
	// function to enable - used on death of enemy sprite
	[SerializeField]
	private int ID;

    private void Start()
    {
        currentHP = maxHP;

		float norm = 1f;
		float weakness = 1.25f;
		float strength = 0.75f;

		if (ID == 1)
        {
			base.define_attributes<float>(new float[] { norm, norm, norm, norm, norm, norm, norm }, base.GetAllATB(), base.GetATB());
			base.define_attributes<int>(new int[] { base.get_dmg(), 0, base.get_dmg() - 10 }, new string[] { "normal", "curse", "shoot" }, base.GetATK());
		}
		else if (ID == 2)
		{
			//base.playerAttacks.Add("burn", new int[] { 1, 12, 5 }); 
			base.define_attributes<float>(new float[] { norm, norm, norm, norm, norm, norm, norm }, base.GetAllATB(), base.GetATB());
			base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 10 }, new string[] { "normal", "fire" }, base.GetATK());
		}
		else
		{
			//base.playerAttacks.Add("poison", new int[] { -1, 40 });
			base.define_attributes<float>(new float[] { norm, norm, norm, norm, norm, norm, norm }, base.GetAllATB(), base.GetATB());
			base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 5 }, new string[] { "normal", "grass" }, base.GetATK());
		}
	}

	public void set_ID(int num) { ID = num; }

    public void enableEnemy()
	{
		gameObject.SetActive(true);
	}



}