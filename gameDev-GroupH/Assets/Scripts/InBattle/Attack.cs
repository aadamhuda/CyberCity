using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BattleSystem
{
	private string[] all_attributes = new string[] { "normal", "shoot", "fire", "water", "ice", "grass", "curse" };

	private IEnumerator DeathAnimation(BattleUnit Target, Animator anim)
	{
		if (Target.CheckIfDead())
			anim.CrossFade("Downed", 0.1f);

		yield return new WaitForSeconds(0.4f);
	}


	public IEnumerator cmd_attack(BattleUnit[] attacker, BattleUnit[] defender, int index, int current_unit, string attack_type, Animator attacker_anim, Animator defender_anim)
	{
		attacker[current_unit].RemoveAilments();

		BattleUnit current_attcker = attacker[current_unit];
		BattleUnit current_defenders = defender[index];

		Vector3 current_attcker_pos = current_attcker.transform.position;
		Vector3 current_defenderr_pos = current_defenders.transform.position;


		if (attacker[current_unit].get_frozen())
		{
			int number = UnityEngine.Random.Range(0, 100);
			// 34% chance to unfreeze 
			if (number > 66)
				attacker[index].set_frozen(false);
		}
		else
		{
			if (attack_type == "fire")
			{

				// Animation
				attacker_anim.CrossFade("Magic", 0.1f);
				yield return new WaitForSeconds(0.5f);
				AudioSource.PlayClipAtPoint(animationAudio[3], current_attcker.transform.position);
				yield return new WaitForSeconds(0.3f);

				current_defenders.takeDamage(attacker[index].GetATK()[attack_type], attack_type);
				StartCoroutine(DeathAnimation(current_defenders, defender_anim));

				int number = UnityEngine.Random.Range(0, 100);
				if (number < 26)
					current_defenders.set_burned(true);

			}
			else if (attack_type == "curse")
			{
				attacker_anim.CrossFade("Magic", 0.1f);
				yield return new WaitForSeconds(0.5f);
				AudioSource.PlayClipAtPoint(animationAudio[5], current_attcker.transform.position);
				yield return new WaitForSeconds(0.3f);

				current_defenders.set_cursed(true);
			}
			else if (attack_type == "ice")
			{
				// Animation
				attacker_anim.CrossFade("Magic", 0.1f);
				yield return new WaitForSeconds(0.5f);
				AudioSource.PlayClipAtPoint(animationAudio[4], current_attcker.transform.position);
				yield return new WaitForSeconds(0.3f);

				current_defenders.set_frozen(true);
			}
			else if (attack_type == "grass")
			{
				attacker_anim.CrossFade("Magic", 0.1f);
				AudioSource.PlayClipAtPoint(animationAudio[2], current_attcker.transform.position);
				yield return new WaitForSeconds(0.8f);


				current_defenders.takeDamage(attacker[index].GetATK()[attack_type], attack_type);
				StartCoroutine(DeathAnimation(current_defenders, defender_anim));


				int number = UnityEngine.Random.Range(0, 100);
				if (number < 26)
					current_defenders.set_poisoned(true);

			}
			else if (attack_type == "water")
			{
				attacker_anim.CrossFade("Magic", 0.1f);
				yield return new WaitForSeconds(0.5f);
				AudioSource.PlayClipAtPoint(animationAudio[6], current_attcker.transform.position);
				yield return new WaitForSeconds(0.3f);


				current_defenders.takeDamage(attacker[index].GetATK()[attack_type], attack_type);
				StartCoroutine(DeathAnimation(current_defenders, defender_anim));
			}
			else if (attack_type == "shoot")
			{
				// Equip Bow
				attacker_anim.CrossFade("EquipBow", 0.1f);
				yield return new WaitForSeconds(0.3f);

				for (int i = 0; i < defender.Length; i++)
				{

					yield return StartCoroutine(base.RotatePlayer(current_attcker, 0.2f, defender[i].transform.position));
					attacker_anim.CrossFade("DrawArrow", 0.1f);
					yield return new WaitForSeconds(0.5f);
					AudioSource.PlayClipAtPoint(animationAudio[7], current_attcker.transform.position);

					defender[i].takeDamage(attacker[index].GetATK()[attack_type], attack_type);
					if (defender[i].CheckIfDead())
					{
						StartCoroutine(DeathAnimation(defender[i], defender[i].GetComponent<Animator>()));

					}
				}
				foreach (Enemy i in defender)
				{
					Debug.Log(i.name + i.CheckIfDead());
				}
				// 
				attacker_anim.CrossFade("DisarmBow", 0.3f);

			}
			else
			{
				// Moving player until next to enemy
				yield return StartCoroutine(base.MovePlayer(current_attcker, true, 0, 2f, current_defenderr_pos));

				// Attack animation
				attacker_anim.CrossFade("Melee", 0.1f);
				AudioSource.PlayClipAtPoint(animationAudio[0], current_attcker.transform.position);
				yield return new WaitForSeconds(0.5f);


				current_defenders.takeDamage(attacker[index].GetATK()[attack_type], attack_type);

				StartCoroutine(DeathAnimation(current_defenders, defender_anim));

				// Moving player back to original position
				yield return new WaitForSeconds(0.4f);
				yield return StartCoroutine(MovePlayer(current_attcker, false, 0, 0.1f, current_attcker_pos));
			}
		}
	}



}
