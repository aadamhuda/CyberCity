using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemSaveData
{
    // Start is called before the first frame update

    private Dictionary<string, bool> death = new Dictionary<string, bool>();
    private Dictionary<string, bool> clue = new Dictionary<string, bool>();
    private int clue_count;
    private int[] team_health = new int[4];
    private int[] team_mp = new int[4];
    private int difficulty = 2;

    [SerializeField]
    private float[] player_location;

    [SerializeField]
    private float[] respawn_location;

    [SerializeField]
    private Dictionary<string, int> items = new Dictionary<string, int>();

    private int remainng_items;

    public SystemSaveData (SaveData savedata)
    {

        // What to save
        this.team_health = savedata.team_health;
        this.team_mp = savedata.team_MP;
        this.death = savedata.Death;
        this.clue = savedata.Clue;
        this.clue_count = savedata.ClueCount;
        this.difficulty = savedata.GetDifficulty();
        this.player_location = new float[] { savedata.get_player_location().x , savedata.get_player_location().y , savedata.get_player_location().z };
        this.respawn_location = new float[] { savedata.get_respawn_location().x, savedata.get_respawn_location().y, savedata.get_respawn_location().z };
        this.items = savedata.get_items();
        this.remainng_items = savedata.get_item_respawn();
    }


    public Dictionary<string, bool> get_death() { return this.death; }

    public Dictionary<string, bool> get_clue() { return this.clue; }

    public int get_clue_count() { return this.clue_count; }

    public int[] get_team_health () { return this.team_health; }
    
    public int[] get_team_mp () { return this.team_mp; }

    public int get_difficulty() { return this.difficulty; }

    public float[] get_player_location() { return this.player_location; }

    public float[] get_respawn_location() { return this.respawn_location; }

    public Dictionary<string, int> get_items() { return this.items; }

    public int get_item_respawn() { return this.remainng_items; }



}
