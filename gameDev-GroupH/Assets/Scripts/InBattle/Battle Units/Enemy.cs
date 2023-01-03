using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from parent class BattleUnit
public class Enemy : BattleUnit
{
	// function to enable - used on death of enemy sprite
	[SerializeField]

    private void Start()
    {
        currentHP = maxHP;

		float norm = 1f;
		float weak = 1.25f;
		float strong = 0.75f;
		//attributes : "normal", "shoot", "fire", "water", "ice", "grass", "curse"
		if (ID == 1)
        {
			base.define_attributes<float>(new float[] { norm, norm, strong, weak, norm, weak, strong }, BattleUnit.GetAllATB(), base.GetATB());
			base.define_attributes<int>(new int[] { base.get_dmg(), 0, base.get_dmg() - 10 }, new string[] { "normal", "curse", "fire"  }, base.GetATK());
		}
		else if (ID == 2)
		{
			//base.playerAttacks.Add("burn", new int[] { 1, 12, 5 }); 
			base.define_attributes<float>(new float[] { norm, norm, weak, strong, strong, norm, weak }, BattleUnit.GetAllATB(), base.GetATB());
			base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 10, 0 }, new string[] { "normal", "water", "ice" }, base.GetATK());
		}
		else if (ID == 3)
		{
			//base.playerAttacks.Add("burn", new int[] { 1, 12, 5 }); 
			base.define_attributes<float>(new float[] { weak, strong, strong, weak, norm, norm, norm }, BattleUnit.GetAllATB(), base.GetATB());
			base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 10, base.get_dmg() - 5 }, new string[] { "normal", "fire", "grass" }, base.GetATK());
		}
		else if (ID == 4)
		{
			//base.playerAttacks.Add("poison", new int[] { -1, 40 });
			base.define_attributes<float>(new float[] { norm, norm, strong, weak, norm, weak, strong }, BattleUnit.GetAllATB(), base.GetATB());
			base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 5, 0 }, new string[] { "normal", "fire", "curse" }, base.GetATK());
		}
		else
		{
			//base.playerAttacks.Add("poison", new int[] { -1, 40 });
			base.define_attributes<float>(new float[] { norm, weak, weak, strong, norm, strong, norm }, BattleUnit.GetAllATB(), base.GetATB());
			base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 5, base.get_dmg() - 5 }, new string[] { "normal", "grass", "water" }, base.GetATK());
		}
	}

	public void set_ID(int num) { ID = num; }

    public void enableEnemy()
	{
		gameObject.SetActive(true);
	}



	// Turn enemy to a position
	public IEnumerator RotateEnemy(float speed, Vector3 targetPos)
	{
		var transform = this.transform;
		var startRotation = transform.rotation;
		var direction = targetPos - transform.position;
		var targetRotation = Quaternion.LookRotation(direction);
		targetRotation.x = 0;
		targetRotation.z = 0;
		var t = 0f;
		while (t <= 1f)
		{
			t += Time.deltaTime / speed;
			transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
			yield return null;
		}
		transform.rotation = targetRotation;
	}

	// Move enemy to a position
	public IEnumerator MoveEnemy(bool forward, float speed, float maxSpeed, float distOffsetToTarget, Vector3 targetPos)
	{
		var transform = this.transform;
		var cc = this.GetComponent<CharacterController>();
		var offset = targetPos - transform.position;
		var animator = this.GetComponent<Animator>();

		// Movement
		animator.SetBool("moveBackwards", !forward);
		animator.SetBool("isMoving", true);

		// Speed up until close to target
		while (Vector3.Distance(transform.position, targetPos) > distOffsetToTarget)
		{
			if (speed > maxSpeed)
			{
				speed = maxSpeed;
			}

			cc.Move(offset * speed * Time.deltaTime);
			speed += 0.1f;
			yield return null;
		}

		// Stop moving when target reached
		animator.SetBool("isMoving", false);

	}
}