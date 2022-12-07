using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

//NOTE: damage and healing must be balanced to provide a challenge while not making it too difficult

//enumerator to hold battle states
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE, PLAYERWAIT, SELECTINGATTACK}

public class BattleSystem : MonoBehaviour
{
	[SerializeField]
	SaveData savedata;

	public GameObject[] playerPrefabs;
	public GameObject enemyPrefab;
	public string[] playerNames = new string[] {"Main Character", "Companion 1", "Companion 2", "Companion 3" };

	public Transform[] enemyLocations;
	public Transform playerLocation; //to be changed to hold multiple players once party system is created

	public BattleState state;

	public TextMeshProUGUI dialogue;

	public Enemy[] enemies;
	public Player[] players;
	public int tracker = 0;
	public Player currPlayer;//this will change to an array once a party system is implemented

	//holds position of currently selected enemy
	public int target = 0;

	public GameObject restartButtonPrefab;
	public Transform rbLocation;

	public Button[] playerMoveButtons;

	//HUD
	public GameObject hudPrefab;

	public PlayerHUDController[] playerHUD;
	public UnitHUD[] enemiesHUD;

	public RectTransform[] enemyHudLocations;


	public GameObject BattleHUD;

	//Animations
	private bool playerAttacking;
	private bool enemyAttacking;
  
	private float speed = 1.5f;

	//Cameras
	[SerializeField]
	private Camera[] battleCameras;
	[SerializeField]
	private Camera mainCamera;
	[SerializeField]
	private Attack play_attack;
	private Dictionary<string, Dictionary<string, string>> checklist = new Dictionary<string, Dictionary<string, string>>();


	// Start is called before the first frame update
	void Start()
    {
		DisableAllPlayerCameras(battleCameras.Length);
		EnableCamera(mainCamera);

		//saves that the player is in battle
		savedata.inBattle = true;

		//shows cursor so buttons can be selected
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		//starts battle
		state = BattleState.START;
        StartCoroutine(InitialiseBattle());
    }

	void Update()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		//refreshes HUDs every frame
		for (int i = 0; i < playerHUD.Length; i++)
			playerHUD[i].SetHealth(players[i]);

		for (int i = 0; i < enemiesHUD.Length; i++)
		{
			enemiesHUD[i].updateHUD(enemies[i]);
		}
		//allows escape button to only be interacted with in player turn
		if (state != BattleState.PLAYERTURN)
        {
			for (int i = 0; i < playerMoveButtons.Length; i++)
			{
				playerMoveButtons[i].interactable = false;
			}
		}
        else
        {
			for (int i = 0; i < playerMoveButtons.Length; i++)
			{
				playerMoveButtons[i].interactable = true;
			}
		}

		CheckTargetChange();

	}
	//-------------------------------------------UPDATE FUNCTIONS-------------------------------------------------------
	void CheckTargetChange()
	{
		if (state == BattleState.PLAYERTURN)
		{
			if (Input.GetKeyDown("a"))
			{
				ChangeTarget(-1);
			}
			if (Input.GetKeyDown("d"))
			{
				ChangeTarget(1);
			}
		}
	}
	//-------------------------------------------INITIALISE BATTLE-------------------------------------------------------
	//spawns hud in placeholder regions
	void InitialiseHUD()
    {
		enemiesHUD = new UnitHUD[3];

		for (int i = 0; i < enemyHudLocations.Length; i++)
		{ 
			GameObject enemyHudObj = Instantiate(hudPrefab, enemyHudLocations[i]);
			enemiesHUD[i] = enemyHudObj.GetComponent<UnitHUD>();
		}

		for (int i = 0; i < playerHUD.Length; i++)
			playerHUD[i].InitialiseSlider(players[i]);

	}

	public Player[] InstantiatePlayers()
    {
		Player[] allPlayers = new Player[4];

		for (int i = 0; i < playerPrefabs.Length; i++)
		{
			GameObject playerObj = Instantiate(playerPrefabs[i], new Vector3(playerLocation.position.x + (i * 2.5f), playerLocation.position.y + 1, playerLocation.position.z), playerLocation.rotation, playerLocation);
			allPlayers[i] = playerObj.GetComponent<Player>();
			allPlayers[i].unitName = playerNames[i];
			allPlayers[i].setHealth(savedata.team_health[i]);
			if (allPlayers[i].currentHP == 0)
				allPlayers[i].downed = true;
			else
				allPlayers[i].downed = false;
		}
		return allPlayers;
	}

    //initialises battle - spawns player and enemies, selects first target and then starts player turn
    IEnumerator InitialiseBattle()
    {
		float norm = 1f;

		string[] all_attributes = new string[] { "normal", "shoot", "fire", "water", "ice", "grass", "curse" };

		players = InstantiatePlayers();
		BattleInventory invMenu = GameObject.FindGameObjectsWithTag("InventoryButton")[0].GetComponent<BattleInventory>();
		invMenu.Init(players, playerNames);
		// player moves

		currPlayer = players[tracker];

        enemies = new Enemy[3];
		
		for (int i = 0; i < enemyLocations.Length; i++)
        {
			GameObject enemyObj = Instantiate(enemyPrefab, enemyLocations[i]);
            enemies[i] = enemyObj.GetComponent<Enemy>();
			enemies[i].unitName = "enemy " + (i + 1);
			enemies[i].set_ID(i);
        }


		ChangeTarget(0);

		InitialiseHUD();

		yield return new WaitForSeconds(1f);
		state = BattleState.PLAYERTURN;
		EnableCamera(battleCameras[0]);
		DisableCamera(mainCamera);
        PlayerTurn();
    }

	//------------------------------------------------------------------- Ibraheem Work --------------------------------------------

/*	private IEnumerator unit_attack(BattleUnit attacker, BattleUnit defender, Animator anim, string attackType)
	{
		Vector3 attackerPos = attacker.transform.position;
		Vector3 defenderPos = defender.transform.position;

		// Animator for player

		// Player is attacking


		// Rotating player until facing enemy
		yield return StartCoroutine(RotateUnit(attacker, 0.2f, defenderPos));



		if (attackType == "fire")
		{

			// Animation
			anim.CrossFade("Burn", 0.1f);
			yield return new WaitForSeconds(1.3f);

			defender.takeDamage(attacker.GetATK()[attackType], attackType);
		}
		else if (attackType == "poison")
		{
			defender.takeDamage(attacker.GetATK()[attackType], attackType);
		}
		else if (attackType == "curse")
		{
			defender.set_cursed();
		}
		else if (attackType == "ice")
		{
			// Animation
			anim.CrossFade("Freeze", 0.1f);
			yield return new WaitForSeconds(1.3f);

			defender.frozen = true;
		}
		else if (attackType == "shoot")
		{
			for (int i = 0; i < enemies.Length; i++)
			{
				defender.takeDamage(attacker.GetATK()[attackType], attackType);
			}
		}
		else
		{
			// Moving player until next to enemy
			yield return StartCoroutine(MoveUnit(attacker, true, 0, 2f, defenderPos));

			// Attack animation
			anim.CrossFade("Melee", 0.1f);
			yield return new WaitForSeconds(1.3f);

			defender.takeDamage(attacker.GetATK()[attackType], attackType);

			// Moving player back to original position
			yield return StartCoroutine(MoveUnit(attacker, false, 0, 0.1f, attackerPos));
		}*/




		//-------------------------------------------Player Attack-------------------------------------------------------
		IEnumerator PlayerAttack(string attackType)
	{
        if (players[tracker].downed)
        {
			ChangePartyTurn(1);
        }

		DestroyAbilities();

		Player playerScript = players[tracker].GetComponent<Player>();
		bool isDead;
		Debug.Log(attackType);
		string[] all_attributes = new string[] { "normal", "shoot", "fire", "water", "ice", "grass", "curse" };



		// Coords of player start and enemy start positions
		Vector3 playerPos = currPlayer.transform.position;
		Vector3 enemyPos = enemies[target].transform.position;

		// Animator for player
		var animator = currPlayer.GetComponent<Animator>();

		// Player is attacking
		state = BattleState.PLAYERWAIT;

		// Rotating player until facing enemy
		yield return StartCoroutine(RotatePlayer(currPlayer, 0.2f, enemyPos));

		players[tracker].RemoveAilments();

		
		if (players[target].get_frozen())
        {
			int number = UnityEngine.Random.Range(0, 100);
			// 34% chance to unfreeze 
			if (number > 66)
				players[tracker].set_frozen();
		}
		if (attackType == "fire")
		{

			// Animation
			animator.CrossFade("Burn", 0.1f);
			yield return new WaitForSeconds(1.3f);

			enemies[target].takeDamage( players[tracker].GetATK()[attackType] , attackType );

			int number = UnityEngine.Random.Range(0, 100);
			if (number < 26)
				enemies[target].set_burned();
		}
		else if (attackType == "poison")
		{
			enemies[target].takeDamage(players[tracker].GetATK()[attackType] , attackType);

			int number = UnityEngine.Random.Range(0, 100);
			if (number < 26)
				enemies[target].set_poisoned();
		}
		else if (attackType == "curse")
        {
			enemies[target].set_cursed();
        }
		else if (attackType == "ice")
		{
			// Animation
			animator.CrossFade("Freeze", 0.1f);
			yield return new WaitForSeconds(1.3f);

			enemies[target].set_frozen();
        }
        else if (attackType == "shoot")
        {
			for (int i = 0; i<enemies.Length; i++)
            {
				enemies[i].takeDamage(players[tracker].GetATK()[attackType], attackType);
				if (enemies[i].CheckIfDead())
                {
					enemies = RemoveEnemies(i);
					enemiesHUD = RemoveHUDs(i, enemiesHUD);
				}
            }
        }
        else 
        {
			// Moving player until next to enemy
			yield return StartCoroutine(MovePlayer(currPlayer, true, 0, 2f, enemyPos));

			// Attack animation
			animator.CrossFade("Melee", 0.1f);
			yield return new WaitForSeconds(1.3f);

			enemies[target].takeDamage(players[tracker].GetATK()[attackType], attackType);

			// Moving player back to original position
			yield return StartCoroutine(MovePlayer(currPlayer, false, 0, 0.1f, playerPos));
		}

		isDead = enemies[target].CheckIfDead();
		dialogue.text = currPlayer.unitName + " attacked " + enemies[target].unitName;
		yield return new WaitForSeconds(2f);

		if (isDead)
        {
			enemies = RemoveEnemies(target);
			enemiesHUD = RemoveHUDs(target, enemiesHUD);
			ChangeTarget(0); //automatically changes target on enemy death
        }

		//checks if all enemies are dead - win condition
		bool enemiesDead = (enemies.Length == 0);
		
		if (enemiesDead)
		{
			state = BattleState.WIN;
			yield return new WaitForSeconds(2f);
			StartCoroutine(EndBattle());
		}
		else
		{
			ChangePartyTurn(1);
				
		}

	}
	void PlayerTurn()
	{
		dialogue.text = "Choose an action!";
	}

	IEnumerator PlayerHeal()
	{

		int amount = Random.Range(60, 100);
		currPlayer.heal(amount);

		dialogue.text = "You healed by " + amount + " hp!";

		state = BattleState.PLAYERWAIT;
		yield return new WaitForSeconds(2f);
		ChangePartyTurn(1);
	}

	//-------------------------------------------ENEMY-------------------------------------------------------
	IEnumerator EnemyTurn()
	{
		bool isDead;

		for (int i = 0; i < enemies.Length; i++)
		{

			int player_target = Random.Range(0, players.Length);


			while (players[player_target].downed)
            {
				player_target = Random.Range(0, players.Length);
			}
			

			// Current enemy
			Enemy currEnemy = enemies[i];

			// Coords of player start and enemy start positions
			Vector3 playerPos = players[player_target].transform.position;
			Vector3 enemyPos = currEnemy.transform.position;

			// Animator for current enemy
			var animator = currEnemy.GetComponent<Animator>();

			dialogue.text = enemies[i].unitName + " attacks!";
			yield return new WaitForSeconds(1f);

			// Rotating enemy until facing player
			yield return StartCoroutine(RotateEnemy(currEnemy, 0.2f, playerPos));

			if (enemies[i].get_frozen())
            {
				// skip turn
				int number = UnityEngine.Random.Range(0, 100);
				// 34% chance to unfreeze 
				if (number > 66)
					enemies[i].set_frozen();
			}
            else
            {
				var randomKey = enemies[i].GetATK().Keys.ElementAt((int)Random.Range(0, enemies[i].GetATK().Keys.Count - 1));


				if (randomKey == "shoot")
                {
					for (int interate = 0; i < enemies.Length; i++)
					{
						players[interate].takeDamage(enemies[i].GetATK()[randomKey], randomKey);
						if (players[interate].CheckIfDead())
							players[i].downed = true;
					}
				}

				// Moving enemy until next to player
				yield return StartCoroutine(MoveEnemy(currEnemy, true, 0, 2f, playerPos));

				// Animation
				animator.CrossFade("Melee", 0.1f);
				yield return new WaitForSeconds(1.3f);

				//adds 15% damage if enemy hits player first
				if (savedata.EnemyDouble == true)
					players[player_target].takeDamage( enemies[i].GetATK()[randomKey] * 1.15f, randomKey); // change 1 to enemy's move type
				else //regular damage
					players[player_target].takeDamage(enemies[i].GetATK()[randomKey] , randomKey);
				// Moving enemy back to original position
				yield return StartCoroutine(MoveEnemy(currEnemy, false, 0, 0.1f, enemyPos));

				if (randomKey == "curse")
					players[player_target].set_cursed();
				if (randomKey == "ice")
					players[player_target].set_frozen();
				if (randomKey == "fire")
                {
					int number = UnityEngine.Random.Range(0, 100);
					if (number < 26)
						players[player_target].set_burned();
				}
				if (randomKey == "grass")
				{
					int number = UnityEngine.Random.Range(0, 100);
					if (number < 26)
						players[player_target].set_poisoned();
				}
			}

			//deal burn damage
/*			if (enemies[i].burned)
            {
				float damage = (float)enemies[i].maxHP * enemies[i].burnDamage * enemies[i].burnMultiplier;
				isDead = enemies[i].takeDamage(damage, 1);
				int number = UnityEngine.Random.Range(0, 100);
				// 20% chance to stop burning 
				if (number > 80)
				{
					enemies[i].burned = false;
				}
			}

			//deal poison damage
			if (enemies[i].poisoned)
			{
				isDead = enemies[i].takeDamage(enemies[i].poisonDamage, -1);
				int number = UnityEngine.Random.Range(0, 100);
				// 20% chance to stop poisoned
				if (number > 80)
				{
					enemies[i].poisoned = false;
				}
			}*/


			isDead = players[player_target].CheckIfDead();
			//stops enemy attacking if player is already dead

			if (isDead)
            {
				players[player_target].downed = true;
            }

			if (CheckAllDead())
				break;
		}

		bool playersDeath = CheckAllDead();

		yield return new WaitForSeconds(1f);

		if (playersDeath)
		{
			state = BattleState.LOSE;
			StartCoroutine(EndBattle());
		}
		else
		{
			state = BattleState.PLAYERTURN;
			ChangePartyTurn(0);
			PlayerTurn();
		}
	}

	//-------------------------------------------BATTLE LOOP-------------------------------------------------------
	private bool CheckAllDead()
    {
		bool allDead = true;

		for (int i = 0; i < players.Length; i++)
		{
			if (players[i].downed == false) 
            {
				allDead = false;
            }
		}
		return allDead;
	}
	private void ChangePartyTurn(int incre)
	{
		// Iterates to next player switching turns when all of currPlayer have had a turn

		tracker += incre;

		if (tracker >= players.Length)
		{
			tracker = 0;
			state = BattleState.ENEMYTURN;
			EnableCamera(mainCamera);
			DisableAllPlayerCameras(battleCameras.Length);
			StartCoroutine(EnemyTurn());
		}

		else
		{
			if (players[tracker].downed == false)
			{
				EnableCamera(battleCameras[tracker]);
				DisableAllPlayerCameras(tracker);
				currPlayer = players[tracker];
				state = BattleState.PLAYERTURN;
				PlayerTurn();
			}
			else
			{
				ChangePartyTurn(1);
			}
		}
	}

	IEnumerator EndBattle()
	{
		if (state == BattleState.WIN)
		{
			dialogue.text = "You WIN the battle!";
			savedata.DictBoolSwitch(savedata.Death, savedata.GetEnemy());
			savedata.OffEnemyDouble();
			savedata.SavePlayerHealth(new int[] { players[0].currentHP, players[1].currentHP,  players[2].currentHP, players[3].currentHP });
			yield return new WaitForSeconds(3f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
		else if (state == BattleState.LOSE)
		{
			dialogue.text = "You were defeated.";
			yield return new WaitForSeconds(2f);

			createRestartButton();
		}
	}

//-------------------------------------------ARRAY FUNCTIONS-------------------------------------------------------

	//removes enemy from array, disables gameObject and returns new array - to be used on enemy death
	private Enemy[] RemoveEnemies(int removeAt)
	{
		Enemy[] new_enemies = new Enemy[enemies.Length - 1];

		//disables the enemy object that has died
		enemies[removeAt].disableUnit();

		int i = 0;
		int j = 0;
		while (i < enemies.Length)
		{
			if (i != removeAt)
			{
				new_enemies[j] = enemies[i];
				j++;
			}

			i++;
		}
		return new_enemies;
	}

	private UnitHUD[] RemoveHUDs(int removeAt, UnitHUD [] arr)
	{
		UnitHUD[] newHUD = new UnitHUD[arr.Length - 1];

		//disables the Objects object that has died
		arr[removeAt].disableHUD();

		int i = 0;
		int j = 0;
		while (i < arr.Length)
		{
			if (i != removeAt)
			{
				newHUD[j] = arr[i];
				j++;
			}

			i++;
		}
		return newHUD;
	}

	//allows player to change targeted enemy
	public void ChangeTarget(int x)
    {
		if (enemies.Length == 0)
			return;
		if (x == 0)
        {
			target = 0;
		}
		else if (x > 0)
        {
			target = target + 1;
			if (target > enemies.Length - 1)
			{
				target = 0;
			}
		}
        else
        {
			target = target - 1;
			if (target < 0)
			{
				target = enemies.Length - 1;
			}
		}
		StartCoroutine(RotatePlayer(currPlayer, 0.2f, enemies[target].transform.position));
	}



//-------------------------------------------ANIMATIONS/MOVEMENT-------------------------------------------------------

	// Turn player to a position
	IEnumerator RotatePlayer(Player p,  float speed, Vector3 targetPos)
	{
		var transform = p.transform;
		var startRotation = transform.rotation;
		var direction = targetPos - transform.position;
		var targetRotation = Quaternion.LookRotation(direction);
		targetRotation.x = 0;
		var t = 0f;
		while (t <= 1f)
		{
			t += Time.deltaTime / speed;
			transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
			yield return null;
		}
		transform.rotation = targetRotation;
	}

	// Turn enemy to a position
	IEnumerator RotateEnemy(Enemy e, float speed, Vector3 targetPos)
	{
		var transform = e.transform;
		var startRotation = transform.rotation;
		var direction = targetPos - transform.position;
		var targetRotation = Quaternion.LookRotation(direction);
		targetRotation.x = 0;
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
	IEnumerator MovePlayer(Player p, bool forward, float speed, float distOffsetToTarget, Vector3 targetPos)
	{
		var transform = p.transform;
		var cc = p.GetComponent<CharacterController>();
		var offset = targetPos - transform.position;
		var animator = p.GetComponent<Animator>();

		// Movement
		animator.SetBool("moveBackwards", !forward);
		animator.SetBool("isMoving", true);

		// Gradually speed up until close to target
		while (Vector3.Distance(transform.position, targetPos) > distOffsetToTarget)
		{
			if (speed > this.speed)
			{
				speed = this.speed;
			}

			cc.Move(offset * speed * Time.deltaTime);
			speed += 0.1f;
			yield return null;
		}
		// Stop moving when target reached
		animator.SetBool("isMoving", false);
	}

	// Move enemy to a position
	IEnumerator MoveEnemy(Enemy e, bool forward, float speed, float distOffsetToTarget, Vector3 targetPos)
	{
		var transform = e.transform;
		var cc = e.GetComponent<CharacterController>();
		var offset = targetPos - transform.position;
		var animator = e.GetComponent<Animator>();

		// Movement
		animator.SetBool("moveBackwards", !forward);
		animator.SetBool("isMoving", true);

		// Speed up until close to target
		while (Vector3.Distance(transform.position, targetPos) > distOffsetToTarget )
		{
			if (speed > this.speed)
			{
				speed = this.speed;
			}

			cc.Move(offset * speed * Time.deltaTime);
			speed += 0.1f;
			yield return null;
		}

		// Stop moving when target reached
		animator.SetBool("isMoving", false);

	}

	//-------------------------------------------CAMERAS-------------------------------------------------------
	private void DisableAllPlayerCameras(int dontRemove)
    {
		//pass 'dontRemove' as battleCameras.length if removing all

		for (int i = 0; i < battleCameras.Length; i++)
        {   if (i != dontRemove)
            {
				battleCameras[i].enabled = false;
			}
			
        }
	}
	private void DisableCamera(Camera cam)
	{
		cam.enabled = false;
	}
	private void EnableCamera(Camera cam)
	{
		cam.enabled = true;
	}
	//-------------------------------------------BUTTONS-------------------------------------------------------

	//initialises restart button - ran after state = LOSE
	public void createRestartButton()
	{
		savedata.ChangeRespawn();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

	}

	//reloads scene on restart
	IEnumerator Restart()
    {
		dialogue.text = "Restarting Battle...";
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


	}

	//sets save data to spawn coordinates to allow player to escape to spawn
	IEnumerator EscapeToSpawn()
	{
		dialogue.text = "Escaping Battle...";
		yield return new WaitForSeconds(2f);

		savedata.ChangeRespawn();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void DisplayAbilities()
    {
		HUDController hudController = BattleHUD.GetComponent<HUDController>();
		hudController.InitaliseMenu(players[tracker]);
	}

	public void DestroyAbilities()
	{
		HUDController hudController = BattleHUD.GetComponent<HUDController>();
		hudController.DestroyMenu();
	}

	public void ExitAbilities()
	{
		DestroyAbilities();
		state = BattleState.PLAYERTURN;
	}
	


	//button methods

	public void OnAttackButton(string attackType)
	{
		if (state != BattleState.SELECTINGATTACK)
			return;

		StartCoroutine(PlayerAttack(attackType));
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}

	public void OnRestartButton()
	{
		if (state != BattleState.LOSE)
			return;

		StartCoroutine(Restart());
	}

    public void OnEscapeButton()
    {
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(EscapeToSpawn());
	}

	public void OnItemButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

	}
	public void OnAbilityButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		state = BattleState.SELECTINGATTACK;
		DisplayAbilities();
	}

	public void OnExitAbilityButton()
	{
		if (state != BattleState.SELECTINGATTACK)
			return;

		ExitAbilities();
	}

	public void OnGuardButton()
	{
		if (state != BattleState.LOSE)
			return;

	}
}
