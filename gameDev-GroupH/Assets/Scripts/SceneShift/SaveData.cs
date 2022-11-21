using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SaveData : ScriptableObject
{
    public bool isNextScene, EnemyDeath, EnemyDouble = false;
    public float x, y, z = 0;
    public Dictionary<string, bool> Death = new Dictionary<string, bool>();
    public Dictionary<string, bool> Clue = new Dictionary<string, bool>();
    public string enem;
    public int ClueCount;
    public float[] team_health = new float[4] { 150, 150, 150, 150};





    public void SaveLocation(float Nx, float Ny, float Nz)
    {
        x = Nx;
        y = Ny;
        z = Nz;
    }

    public void SaveEnem(string obj)
    {
        Debug.Log(obj);
        enem = obj;
    }

    public string GetEnemy()
    {
        return enem;
    }

    public void DictBoolSwitch(Dictionary<string, bool> dict, string obj)
    {
        dict[obj] = true;
    }

    public void AddToDict(Dictionary<string, bool> dict, string obj)
    {
        if (!(dict.ContainsKey(obj)))
        {
            Debug.Log("It's True");
            dict.Add(obj, false);
        }
            
        else
            Debug.Log("Fail");
    }

    public void LoadPlayerHealth(float [] arr)
    {
        for (int i = 0; i < arr.Length; i++)
            team_health[i] = arr[i];
    }

    public bool getDict(Dictionary<string, bool> dict, string obj)
    {
        return (dict[obj]);
    }

    public void SwitchBool()
    {
        isNextScene = !isNextScene;
    }

    public void TruthBool()
    {
        isNextScene = true;
    }

    public void OnEnemyDouble()
    {
        EnemyDouble = true;
    }

    public void OffEnemyDouble()
    {
        EnemyDouble = false;
    }

    public float getX()
    {
        return x;
    }
    public float getY()
    {
        return y;
    }
    public float getZ()
    {
        return z;
    }
}
