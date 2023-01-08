using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


//inherits from parent class BattleUnit
public class Player : BattleUnit
{
    [SerializeField]
    private SaveData saveData;
	// Player attacks
    public Dictionary<string, int> playerAttacks = new Dictionary<string, int>();
    public string selectedMove = "normal";
    public bool downed;
    public int currentMP;
    public int maxMP;

    public Dictionary<string, int> mpCost = new Dictionary<string, int>();
    // Set initial player attacks

    private void Start()
    {
        //set MP cost for attacks
        mpCost.Add("normal", 0);
        mpCost.Add("shoot", 5);
        mpCost.Add("fire", 3);
        mpCost.Add("water", 3);
        mpCost.Add("ice", 4);
        mpCost.Add("grass", 3);
        mpCost.Add("curse", 3);
        //multipliers for affinities
        float norm = 1f;
        float weak = 1.25f;
        float strong = 0.75f;

        // Initialising players

        if (ID == 0)
        {
            base.define_attributes<float>(new float[] { strong, norm, strong, norm, norm, norm, strong }, BattleUnit.GetAllATB(), base.GetATB());
            base.define_attributes<int>(new int[] { base.get_dmg(), 0, base.get_dmg() - 10 }, new string[] { "normal", "curse", "shoot" }, base.GetATK());
            this.unitName = "Nadiya";
        }
        else if (ID == 1)
        {
            //base.playerAttacks.Add("burn", new int[] { 1, 12, 5 }); 
            base.define_attributes<float>(new float[] { norm, norm, strong, weak, norm, norm, strong }, BattleUnit.GetAllATB(), base.GetATB());
            base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 10, 0 }, new string[] { "normal", "fire", "curse" }, base.GetATK());
            this.unitName = "Dreyar";
        }
        else if (ID == 2)
        {
            //base.playerAttacks.Add("poison", new int[] { -1, 40 });
            base.define_attributes<float>(new float[] { weak, norm, weak, strong, norm, strong, norm }, BattleUnit.GetAllATB(), base.GetATB());
            base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() + 5, base.get_dmg() + 5 }, new string[] { "normal", "grass", "water" }, base.GetATK());
            this.unitName = "Astra";
        }
        else
        {
            base.define_attributes<float>(new float[] { weak, norm, weak, strong, strong, weak, norm }, BattleUnit.GetAllATB(), base.GetATB());
            base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() + 5, 0 }, new string[] { "normal", "water", "ice" }, base.GetATK());
            this.unitName = "Joe";
        }
    }


    public void setHealth(int newHealth)
    {
        currentHP = newHealth;
    }

    // Get All Attacks
    public string[] allAttacks()
    {
        return (new List<string>(base.GetATK().Keys).ToArray());
    }

    // Change attack
    public void changeAttack(string attackName)
    {
        string[] attacks = allAttacks();
        int attacksNum = attacks.Length;


        for (int i = 0; i < attacksNum; i++)
        {
            if (attacks[i] == attackName)
            {
                Debug.Log(attacks[i]);
                if (i + 1 < attacksNum)
                {
                    selectedMove = attacks[i + 1];
                }
                else
                {
                    selectedMove = attacks[0];
                }
                break;
            }
        }
    }

    public void revive(float percentage) // can heal 50 or 100 percent for max revive
    {
        // Revive a player -- setactive
        this.downed = false;
        this.usePotion((int)Math.Round((float)this.maxHP * percentage));

        this.gameObject.GetComponent<Animator>().CrossFade("Idle", 0.2f); ;
    }

    public void heal(int amount)
	{
        // Heal  aplayer
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

    public void SetMP(int mp)
    {
        this.currentMP = mp;
    }

    public void Restore(int amount)
    {
        currentMP += amount;
        if (currentMP > maxMP)
            currentMP = maxMP;
    }

    public bool UseMP(int mpUsed)
    {
        // Consume MP after a move
        int newMP = currentMP - mpUsed;
        if(newMP < 0)
        {
            return false;
        }
        else
        {
            currentMP = newMP;
            return true;
        }
    }

    public int getTotalMaxHP()
    {
        return maxHP;
    }

    // Turn player to a position
    public IEnumerator RotatePlayer(float speed, Vector3 targetPos)
    {
        // Spin move :)
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

    // Move player to a position
    public IEnumerator MovePlayer(bool forward, float speed, float maxSpeed, float distOffsetToTarget, Vector3 targetPos)
    {
        // Move a player to a target location at a speed
        var transform = this.transform;
        var cc = this.GetComponent<CharacterController>();
        var offset = targetPos - transform.position;
        var animator = this.GetComponent<Animator>();

        // Movement
        animator.SetBool("moveBackwards", !forward);
        animator.SetBool("isMoving", true);

        // Gradually speed up until close to target
        while (Vector3.Distance(transform.position, targetPos) > distOffsetToTarget)
        {
            if (!forward) 
            {
                if (transform.position.z < targetPos.z)
                {
                    break;
                }
            }

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
