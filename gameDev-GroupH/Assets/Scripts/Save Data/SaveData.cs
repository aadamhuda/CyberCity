using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SaveData : ScriptableObject
{
    public bool isNextScene, EnemyDeath, EnemyDouble = false;

    public Dictionary<string, bool> Death = new Dictionary<string, bool>();
    public Dictionary<string, bool> Clue = new Dictionary<string, bool>();
    public string enem;
    public int ClueCount;
    public bool inBattle;
    public int[] team_health = new int[4];
    public int[] team_MP = new int[4];
    private int difficulty = 2;

    [SerializeField]
    private Vector3 player_location;

    [SerializeField]
    private Vector3 respawn_location;

    [SerializeField]
    private bool respawn = true;

    [SerializeField]
    private Dictionary<string, int> items = new Dictionary<string, int>();

    [SerializeField]
    private int item_spawn;

    public int GetDifficulty() { return difficulty; }

    public bool getDict(Dictionary<string, bool> dict, string obj) { return (dict[obj]); }

    public Vector3 get_player_location() { return this.player_location; }

    public bool getRespawn() { return respawn; }

    public Vector3 get_respawn_location() { return respawn_location; }

    public string GetEnemy() { return enem; }

    public Dictionary<string, int> get_items() { return this.items; }

    public int get_item_respawn() { return this.item_spawn; }

    public void SwitchBool()
    { isNextScene = !isNextScene; }

    public void TruthBool()
    { isNextScene = true; }

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


    public void DictBoolSwitch(Dictionary<string, bool> dict, string obj)
    { dict[obj] = true; }

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

    public void SaveGame()
    {
        SaveSystem.SaveSystemInformation(this);
    }

    public void LoadData()
    {
        SystemSaveData data = SaveSystem.LoadData();

        this.team_health = data.get_team_health();
        this.team_MP = data.get_team_mp();
        this.Death = data.get_death();
        this.Clue = data.get_clue();
        this.ClueCount = data.get_clue_count();
        this.difficulty = data.get_difficulty();
        this.player_location = new Vector3(data.get_player_location()[0], data.get_player_location()[1], data.get_player_location()[2]);
        this.respawn_location = new Vector3(data.get_respawn_location()[0], data.get_respawn_location()[1], data.get_respawn_location()[2]);
        this.items = data.get_items();
        this.item_spawn = data.get_item_respawn();

        this.respawn = !this.respawn;
    }

}
