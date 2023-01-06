using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

// Scriptable data to preserve data throught scenes
public class SaveData : ScriptableObject
{
    // Keep track of whether enemy has died
    // Keeps track if enemy engaged combat
    public bool EnemyDeath, EnemyDouble = false;
    
    // Save which enemies have died
    public Dictionary<string, bool> Death = new Dictionary<string, bool>();

    // Save which clues have been collected
    public Dictionary<string, bool> Clue = new Dictionary<string, bool>();

    // Save which enemy has enageged combat
    public string enem;

    // How many clues collected
    public int ClueCount;

    // Save whether player is InBattle
    public bool inBattle;

    // Save the players health
    public int[] team_health = new int[4];

    // SAves the players MP
    public int[] team_MP = new int[4];

    // Saves difficulty
    public int difficulty ;

    // Check whether tutorial has been completed
    public bool tutorial = false; 
    // Current Level
    [SerializeField]
    private string current_level = "Main Menu";

    // Cargo Level
    [SerializeField]
    private string cargo_level = "Cargo Level";

    // Cargo Level
    [SerializeField]
    private string cyber_level = "City Level";


    // Switch into cargo battle
    [SerializeField]
    private string cargo_battle = "Battle";
    
    // Switch into cyber battle
    [SerializeField]
    private string cyber_battle = "Cyber Battle";

    // Saves last location before entering battle
    [SerializeField]
    private Vector3 player_location;

    // sAves last location saved
    [SerializeField]
    private Vector3 respawn_location;

    // Saves whether to go to last location spawned
    [SerializeField]
    private bool respawn = true;

    // Stores items
    [SerializeField]
    private Dictionary<string, int> items = new Dictionary<string, int>();

    // Stores how many items to spawn
    [SerializeField]
    private int item_spawn;

    private Dictionary<int, Dictionary<string, string>> checklist = new Dictionary<int, Dictionary<string, string>>();

    private Dictionary<int, Dictionary<string, string>> KnownEnemyAttributes = new Dictionary<int, Dictionary<string, string>>();
    
    // Shows where to start dialogue
    [SerializeField]
    private int dialogue_index;

    // Shows where to start dialogue when reaching checkpoint
    [SerializeField]
    private int save_index;

    // Saves last location saved
    public string LastSave;


    // Getters
    public string get_current_level() { return this.current_level; }
    public string get_cargo_level() { return this.cargo_level; }

    public string get_cyber_level() { return this.cyber_level; }

    public string get_cargo_battle() { return this.cargo_battle; }
    
    public string get_cyber_battle() { return this.cyber_battle; }
    public int GetDifficulty() { return difficulty; }

    public bool getDict(Dictionary<string, bool> dict, string obj) { return (dict[obj]); }

    public Vector3 get_player_location() { return this.player_location; }

    public bool getRespawn() { return respawn; }

    public Vector3 get_respawn_location() { return respawn_location; }

    public string GetEnemy() { return enem; }

    public Dictionary<string, int> get_items() { return this.items; }

    public int get_item_respawn() { return this.item_spawn; }

    public Dictionary<int, Dictionary<string, string>> get_checklist() { return checklist; }
    
    public Dictionary<int, Dictionary<string, string>> GetKnownEnemyAttributes() { return this.KnownEnemyAttributes; }
    
    public int get_dialogue_index() { return this.dialogue_index; } 
    
    public int get_save_index() { return this.save_index; }


    // Setters
    public void SetDifficulty(int diff) 
    { this.difficulty = diff; }
    public void set_current_level(string level)
    { this.current_level = level; }

    public void OnEnemyDouble()
    { EnemyDouble = true; }

    public void OffEnemyDouble()
    { EnemyDouble = false; }

    public void ChangeRespawn()
    { respawn = !respawn; }

    public void SaveLocation(Vector3 location)
    { this.player_location = location; }

    public void set_respawn(Vector3 transform)
    { respawn_location = transform; }

    public void SaveEnem(string obj)
    { enem = obj; }

    public void set_items(Dictionary<string, int> arr)
    { this.items = arr; }

    public void set_item_respawn(int arr)
    { this.item_spawn = arr; }

    public void set_dialogue_index(int index) 
        { this.dialogue_index = index; }
    
    public void set_save_index(int index) 
        { this.save_index = index; }



    public void DictBoolSwitch(Dictionary<string, bool> dict, string obj)
    { dict[obj] = true; }

    public void set_checklist(Dictionary<int, Dictionary<string, string>> arr) 
        { this.checklist = arr; }
    
    public void SetKnownEnemyAttributes(Dictionary<int, Dictionary<string, string>> arr) 
        { this.KnownEnemyAttributes = arr; }

    public void AddToDict(Dictionary<string, bool> dict, string obj)
    {
        if (!(dict.ContainsKey(obj)))
            dict.Add(obj, false);

    }

    public void SavePlayerHealth(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
            team_health[i] = arr[i];
    }

    public void SavePlayerMP(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
            team_MP[i] = arr[i];
    }

    public void SaveGame(string PathName)
    {
        // Saves savedata into binary
        SaveSystem.SaveSystemInformation(this, PathName);
        // Record save location
        this.LastSave = PathName;
    }

    public void LoadData(string FileName)
    {
        // Retrives data from binary file
        SystemSaveData data = SaveSystem.LoadData(FileName);


        // Loads binary file data into this class
        this.team_health = data.get_team_health();
        this.team_MP = data.get_team_mp();
        this.Death = data.get_death();
        this.Clue = data.get_clue();
        this.ClueCount = data.get_clue_count();
        this.difficulty = data.get_difficulty();
        this.current_level = data.get_current_level();
        this.player_location = new Vector3(data.get_player_location()[0], data.get_player_location()[1], data.get_player_location()[2]);
        this.respawn_location = new Vector3(data.get_respawn_location()[0], data.get_respawn_location()[1], data.get_respawn_location()[2]);
        this.items = data.get_items();
        this.item_spawn = data.get_item_respawn();
        this.checklist = data.get_checklist();
        this.KnownEnemyAttributes = data.KnownEnemyAttributes;
        this.dialogue_index = data.dialogue_index;
        this.save_index = data.save_index;

        // Spawn at last saved location
        this.respawn = !this.respawn;
    }

}
