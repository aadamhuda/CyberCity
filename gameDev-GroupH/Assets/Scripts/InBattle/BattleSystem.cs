using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.InputSystem;

//NOTE: damage and healing must be balanced to provide a challenge while not making it too difficult

//enumerator to hold battle states
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE, PLAYERWAIT, SELECTINGATTACK}

public class BattleSystem : MonoBehaviour
{
	[SerializeField]
	SaveData savedata;

	public GameObject[] playerPrefabs;
	public GameObject[] enemyPrefabs;

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

	//Environment
	public GameObject cargoEnv;
	public GameObject cityEnv;

	//HUD
	public GameObject hudPrefab;

	public PlayerHUDController[] playerHUD;
	public UnitHUD[] enemiesHUD;

	public RectTransform[] enemyHudLocations;

	public GameObject itemMenu;
	public GameObject BattleHUD;
	public GameObject startRender;

	public ElementInformation BattleInformation;

	// Animations
	private float speed = 1f;

	// Animations
	public BattleAnimator animator;

	//Cameras
	[SerializeField]
	private Camera[] battleCameras;
	[SerializeField]
	private Camera mainCamera;
	

	// Strores attributes of players known by enemies i.e { player : { AttackType : affinity, ... } , ... }
	private Dictionary<int, Dictionary<string, string>> checklist;
	private Dictionary<int, Dictionary<string, string>> KnownEnemyAttributes = new Dictionary<int, Dictionary<string, string>>();

	[SerializeField]
	private GameObject WinLoseScreen;

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

		SetEnvironment();

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
        {
			playerHUD[i].SetHealth(players[i]);
			playerHUD[i].SetMagic(players[i]);
		}
			

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

	}
	//-------------------------------------------UPDATE FUNCTIONS-------------------------------------------------------
	public void OnSwitchTargetLeft(InputValue value)
	{
		if (state == BattleState.PLAYERTURN)
		{
			if (value.isPressed)
			{
				StartCoroutine(ChangeTarget(-1));
			}
		}
	}
	public void OnSwitchTargetRight(InputValue value)
	{
		if (state == BattleState.PLAYERTURN)
		{
            if (value.isPressed)
            {
				StartCoroutine(ChangeTarget(1));
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
			allPlayers[i].setHealth(savedata.team_health[i]);
			allPlayers[i].SetMP(savedata.team_MP[i]);
			if (allPlayers[i].currentHP == 0)
				allPlayers[i].downed = true;
			else
				allPlayers[i].downed = false;
		}
		return allPlayers;
	}

	public Enemy[] InstantiateEnemies()
    {
		int enemyCount = 3;
		Enemy[] allEnemies = new Enemy[enemyCount];

		for (int i = 0; i < enemyCount; i++)
		{
			int enemID = Random.Range(0, 5);
			
			GameObject enemyObj = Instantiate(enemyPrefabs[enemID], enemyLocations[i]);
			allEnemies[i] = enemyObj.GetComponent<Enemy>();
			
		}
		return allEnemies;

    }

	public void SetEnvironment()
    {
        if (savedata.get_current_level() == savedata.get_cyber_level())
        {
			cityEnv.SetActive(true);
			cargoEnv.SetActive(false);
		}
        else
        {
			cityEnv.SetActive(false);
			cargoEnv.SetActive(true);
		}
    }

    //initialises battle - spawns player and enemies, selects first target and then starts player turn
    IEnumerator InitialiseBattle()
    {
        players = InstantiatePlayers();

        currPlayer = players[tracker];

        enemies = InstantiateEnemies();

        StartCoroutine(ChangeTarget(0));

        InitialiseHUD();



		InitialiseMemory();

		// Loading effects and models for smooth playback
		Destroy(Instantiate(startRender), 1.2f);

		yield return new WaitForSeconds(1f);
        state = BattleState.PLAYERTURN;
        EnableCamera(battleCameras[0]);
        DisableCamera(mainCamera);
        StartCoroutine(PlayerTurn());
    }

	// Initialise Enemy Memory

	private void InitialiseMemory()
    {
		this.checklist = new Dictionary<int, Dictionary<string, string>>();
		this.KnownEnemyAttributes = new Dictionary<int, Dictionary<string, string>>();
		// Check if there is data already on 
		if (savedata.get_checklist().Count != 0)
			this.checklist = savedata.get_checklist();
		else
			foreach (Player i in players)
				this.checklist.Add(i.getID(), new Dictionary<string, string>());	
		
		if (savedata.GetKnownEnemyAttributes().Count != 0)
			this.KnownEnemyAttributes = savedata.GetKnownEnemyAttributes();

		foreach (Enemy i in enemies)
			if (this.KnownEnemyAttributes.ContainsKey(i.getID()) == false)
				this.KnownEnemyAttributes.Add(i.getID(), new Dictionary<string, string>());

	}


    //-------------------------------------------Player Attack-------------------------------------------------------
    void PreAttackChecks(string attackType, int mpConsumption)
    {
		if (currPlayer.downed)
		{
			ChangePartyTurn(1);
		}

		DestroyAbilities();

        if (players[tracker].UseMP(mpConsumption) == false)
        {
            dialogue.text = "You do not have enough MP for this move!";
            PlayerTurn();
            state = BattleState.PLAYERTURN;
        }
        else
        {
        StartCoroutine(PlayerAttack(attackType));
		}
	}
	
	IEnumerator PlayerAttack(string attackType)
	{
		bool isDead;

		Enemy enemyTarget = enemies[target];

		// Coords of player start and enemy start positions
		Vector3 playerPos = currPlayer.transform.position;
		Vector3 enemyPos = enemyTarget.transform.position;

		// Animator for player
		var playerAnimator = currPlayer.GetComponent<Animator>();
		var enemyAnimator = enemyTarget.GetComponent<Animator>();

		// Rotating player until facing enemy
		yield return StartCoroutine(currPlayer.RotatePlayer(0.2f, enemyPos));

		// Player is attacking
		state = BattleState.PLAYERWAIT;

		players[tracker].RemoveAilments();

		
		if (players[target].get_frozen())
        {
			int number = UnityEngine.Random.Range(0, 100);
			// 34% chance to unfreeze 
			if (number > 66)
				players[tracker].set_frozen(false);
		}
		else
        {
			if (attackType == "fire")
			{
				yield return StartCoroutine(animator.Magic(playerAnimator, currPlayer.transform, enemyTarget.transform, "fire"));

				enemyTarget.takeDamage(players[tracker].GetATK()[attackType], attackType);
				StartCoroutine(animator.EnemyDeath(enemyTarget, enemyAnimator));

				int number = UnityEngine.Random.Range(0, 100);
				if (number < 26)
					enemyTarget.set_burned(true);

			}
			else if (attackType == "curse")
			{
				yield return StartCoroutine(animator.Magic(playerAnimator, currPlayer.transform, enemyTarget.transform, "curse"));
				enemyTarget.set_cursed(true);
			}
			else if (attackType == "ice")
			{
				yield return StartCoroutine(animator.Magic(playerAnimator, currPlayer.transform, enemyTarget.transform, "ice"));

				enemyTarget.set_frozen(true);
			}
			else if (attackType == "grass")
            {
				yield return StartCoroutine(animator.Magic(playerAnimator, currPlayer.transform, enemyTarget.transform, "grass"));
				enemyTarget.takeDamage(players[tracker].GetATK()[attackType], attackType);
				StartCoroutine(animator.EnemyDeath(enemyTarget, enemyAnimator));

				int number = UnityEngine.Random.Range(0, 100);
				if (number < 26)
					enemyTarget.set_poisoned(true);
			}
			else if (attackType == "water")
			{
				yield return StartCoroutine(animator.Magic(playerAnimator, currPlayer.transform, enemyTarget.transform, "water"));
				enemyTarget.takeDamage(players[tracker].GetATK()[attackType], attackType);
				StartCoroutine(animator.EnemyDeath(enemyTarget, enemyAnimator));
			}
			else if (attackType == "shoot")
			{
				yield return StartCoroutine(animator.EquipBow(playerAnimator, currPlayer.transform));

				for (int i = 0; i < enemies.Length; i++)
				{

					yield return StartCoroutine(currPlayer.RotatePlayer(0.2f, enemies[i].transform.position));
					yield return StartCoroutine(animator.Shoot(playerAnimator, currPlayer.transform, enemies[i].transform));

					enemies[i].takeDamage(players[tracker].GetATK()[attackType], attackType);
					StartCoroutine(animator.EnemyDeath(enemies[i], enemies[i].GetComponent<Animator>()));

				}
				StartCoroutine(animator.DisarmBow(playerAnimator, currPlayer.transform));

			}
			else
			{
				// Moving player until next to enemy
				yield return StartCoroutine(animator.EquipSword(playerAnimator, currPlayer.transform));
				yield return StartCoroutine(currPlayer.MovePlayer(true, 0, speed, 2f, enemyPos));

				yield return StartCoroutine(animator.Melee(playerAnimator, currPlayer.transform, enemyTarget.transform));
				enemyTarget.takeDamage(players[tracker].GetATK()[attackType], attackType);
				StartCoroutine(animator.EnemyDeath(enemyTarget, enemyAnimator));

				// Moving player back to original position
				yield return new WaitForSeconds(0.7f);
				yield return StartCoroutine(animator.DisarmSword(playerAnimator, currPlayer.transform));
				StartCoroutine(currPlayer.MovePlayer(false, 0, speed, 0.1f, playerPos));

			}
			string state;
			if (this.KnownEnemyAttributes[enemyTarget.getID()].ContainsKey(attackType) == false)
			{
				// Stores affinity of attribute in list
				if (enemyTarget.GetATB()[attackType] > 1f)
					state = "weak";
				else if (enemyTarget.GetATB()[attackType] == 1f)
					state = "norm";
				else
					state = "strength";

				this.KnownEnemyAttributes[enemyTarget.getID()].Add(attackType, state);
			}
		}
		                      

		isDead = enemyTarget.CheckIfDead();

		dialogue.text = currPlayer.unitName + " attacked " + enemyTarget.unitName;
		yield return new WaitForSeconds(2f);

		for (int i = 0; i < enemies.Length;)
		{
			if (enemies[i].CheckIfDead())
			{
				enemies = RemoveEnemies(i);
				enemiesHUD = RemoveHUDs(i, enemiesHUD);
			}
			else
				i++;
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
	IEnumerator PlayerTurn()
	{
		Vector3 enemyPos = enemies[target].transform.position;
		
		dialogue.text = "Choose an action!";
		yield return StartCoroutine(currPlayer.RotatePlayer(0.2f, enemyPos));
	}

	IEnumerator PlayerHeal()
	{
		int mpCost = 4;
		int amount = Random.Range(60, 100);
		currPlayer.heal(amount);
		currPlayer.UseMP(mpCost);
		StartCoroutine(animator.Heal(currPlayer.GetComponent<Animator>(), currPlayer.transform));
		dialogue.text = "You healed by " + amount + " hp!";

		state = BattleState.PLAYERWAIT;
		yield return new WaitForSeconds(2f);
		ChangePartyTurn(1);
	}


	//-------------------------------------------ITEM-------------------------------------------------------

	public void UseItem()
	{
		StartCoroutine(ItemWait());
		ChangePartyTurn(1);
	}

	IEnumerator ItemWait()
    {
		itemMenu.SetActive(false);
		state = BattleState.PLAYERWAIT;
		yield return new WaitForSeconds(2f);
	}




	//-------------------------------------------ENEMY-------------------------------------------------------
	IEnumerator EnemyTurn()
	{
		Dictionary<int, bool> playerAttacked = new Dictionary<int, bool>();
		foreach (Player unit in players)
			playerAttacked.Add(unit.getID(), false);
		Dictionary<int, string> weakTo = new Dictionary<int, string>();
		bool isDead;
		for (int i = 0; i < enemies.Length; i++)
		{


			Enemy currEnemy = enemies[i];
			Player playerTarget = null;

			float multi = 1f;
			if (savedata.EnemyDouble)
				multi += 0.15f;

			string randomKey = "attacknotfound";
			int highest = 0;

			

			

			// Current enemy & Player
			if (playerTarget == null)
            {
				int player_target = Random.Range(0, players.Length);

				while (players[player_target].downed)
				{
					player_target = Random.Range(0, players.Length);
				}

				playerTarget = players[player_target];
			}
			

			int playerTarget_HP = playerTarget.currentHP;

			// Coords of player start and enemy start positions
			Vector3 playerPos = playerTarget.transform.position;
			Vector3 enemyPos = currEnemy.transform.position;

			// Animator for player
			var playerAnimator = playerTarget.GetComponent<Animator>();
			var enemyAnimator = currEnemy.GetComponent<Animator>();

			currEnemy.RemoveAilments();

			dialogue.text = enemies[i].unitName + " attacks!";

			yield return new WaitForSeconds(1f);

			// Rotating enemy until facing player
			yield return StartCoroutine(currEnemy.RotateEnemy(0.2f, playerPos));


			if (currEnemy.get_frozen())
            {
				// skip turn
				int number = UnityEngine.Random.Range(0, 100);
				// 34% chance to unfreeze 
				if (number > 66)

					enemies[i].set_frozen(false);
			}
            else
            {
				// Moving enemy until next to player
				if (!currEnemy.transform.GetChild(0).name.Equals("Ely By K.Atienza"))
				{
					yield return StartCoroutine(animator.EquipSword(enemyAnimator, currEnemy.transform));
				}

				yield return StartCoroutine(currEnemy.MoveEnemy(true, 0, speed, 2f, playerPos));

				yield return StartCoroutine(animator.Melee(enemyAnimator, currEnemy.transform, playerTarget.transform));
				Debug.Log("I have to eat soon : " + randomKey);

				if (randomKey == "attacknotfound")
					randomKey = enemies[i].GetATK().Keys.ElementAt((int)Random.Range(0, enemies[i].GetATK().Keys.Count - 1));


				if (randomKey == "curse")
                {
					playerTarget.set_cursed(true);
				}
				else if (randomKey == "ice")
                {
					playerTarget.set_frozen(true);

				}
				else if (randomKey == "fire")
                {
					playerTarget.takeDamage(currEnemy.GetATK()[randomKey] * multi, randomKey);
					StartCoroutine(animator.PlayerDeath(playerTarget, playerAnimator));
					

					int number = UnityEngine.Random.Range(0, 100);
					if (number < 26)
						playerTarget.set_burned(true);

				}
				else if (randomKey == "grass")
				{
					playerTarget.takeDamage(currEnemy.GetATK()[randomKey] * multi, randomKey);
					StartCoroutine(animator.PlayerDeath(playerTarget, playerAnimator));


					int number = UnityEngine.Random.Range(0, 100);
					if (number < 26)
						playerTarget.set_poisoned(true);
				}
				else if (randomKey == "shoot")
				{
					for (int j = 0; j < players.Length; j++)
					{
						players[j].takeDamage(currEnemy.GetATK()[randomKey] * multi, randomKey);
						StartCoroutine(animator.PlayerDeath(players[j], players[j].GetComponent<Animator>()));
						if (players[j].CheckIfDead())
							players[j].downed = true;
					}
				}
				else
                {
					//adds 15% damage if enemy hits player first
					playerTarget.takeDamage(currEnemy.GetATK()[randomKey] * multi, randomKey); // change 1 to enemy's move type
					StartCoroutine(animator.PlayerDeath(playerTarget, playerAnimator));

				}

				// Moving enemy back to original position
				if (!currEnemy.transform.GetChild(0).name.Equals("Ely By K.Atienza"))
				{
					yield return new WaitForSeconds(0.7f);
					yield return StartCoroutine(animator.DisarmSword(enemyAnimator, currEnemy.transform));
				}
				else
				{
					yield return new WaitForSeconds(0.3f);
				}
				yield return StartCoroutine(currEnemy.MoveEnemy(false, 0, speed, 0.1f, enemyPos));

				string state;
				if (this.checklist[playerTarget.getID()].ContainsKey(randomKey) == false)
				{
					// Stores affinity of attribute in list
					if (playerTarget.GetATB()[randomKey] > 1f)
						state = "weak";
					else if (playerTarget.GetATB()[randomKey] == 1f)
						state = "norm";
					else
						state = "strength";

					this.checklist[playerTarget.getID()].Add(randomKey, state);
				}


			}

				

            //deal burn damage
            if (currEnemy.get_burned())
            {
				currEnemy.takeDamage(10f, "fire");
				StartCoroutine(animator.EnemyDeath(currEnemy, enemyAnimator));
			}

			//deal poison damage
			if (currEnemy.get_poisoned())
            {
				currEnemy.takeDamage(10f, "grass");

				StartCoroutine(animator.EnemyDeath(currEnemy, enemyAnimator));
			}


			isDead = playerTarget.CheckIfDead();

			//stops enemy attacking if player is already dead

			if (isDead)
            {
				playerTarget.downed = true;
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
			StartCoroutine(PlayerTurn());
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
                if (target > enemies.Length-1)
                {
                    StartCoroutine(ChangeTarget(0));
                }
				state = BattleState.PLAYERTURN;
				StartCoroutine(PlayerTurn());
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
			dialogue.text = "You won the battle!";
			savedata.DictBoolSwitch(savedata.Death, savedata.GetEnemy());
			savedata.OffEnemyDouble();
			savedata.SavePlayerMP(new int[] { players[0].currentMP, players[1].currentMP, players[2].currentMP, players[3].currentMP });
			savedata.SavePlayerHealth(new int[] { players[0].currentHP, players[1].currentHP,  players[2].currentHP, players[3].currentHP });
			savedata.set_checklist(this.checklist);
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene(this.savedata.get_current_level());
		}
		else if (state == BattleState.LOSE)
		{
			yield return new WaitForSeconds(1f);
			this.WinLoseScreen.SetActive(true);
			this.WinLoseScreen.GetComponent<WinLoseScreen>().ActivateLose();
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
	public IEnumerator ChangeTarget(int x)
    {
		if (enemies.Length == 0)
			yield return new WaitForSeconds(0.1f);
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
		if (enemies.Length > 0)
		{
			yield return StartCoroutine(currPlayer.RotatePlayer(0.2f, enemies[target].transform.position));
		}
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
		SceneManager.LoadScene(savedata.get_current_level());

	}

	//sets save data to spawn coordinates to allow player to escape to spawn
	IEnumerator EscapeToSpawn()
	{
		dialogue.text = "Escaping Battle...";
		yield return new WaitForSeconds(2f);

		savedata.ChangeRespawn();
		SceneManager.LoadScene(savedata.get_current_level());
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

	public void OnAttackButton(string attackType, int mpConsumption)
	{
		if (state != BattleState.SELECTINGATTACK)
			return;

		PreAttackChecks(attackType, mpConsumption);
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
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

		itemMenu.SetActive(true);
		itemMenu.GetComponent<InventoryMenu>().LoadMenu(players);

	}
	public void OnAbilityButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		state = BattleState.SELECTINGATTACK;
		DisplayAbilities();
	}	
	
	public void DisplayInfo()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		this.BattleInformation.LoadElements(players[tracker], this.KnownEnemyAttributes[enemies[target].getID()], enemies[target].unitName);
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
