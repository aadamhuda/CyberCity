using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SaveData : ScriptableObject
{
    [SerializeField]
    public bool isNextScene, EnemyDeath, EnemyDouble = false;
    [SerializeField]
    public float x, y, z = 0;
    [SerializeField]
    public Dictionary<string, bool> Death = new Dictionary<string, bool>();
    [SerializeField]
    public string enem;


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

    public void killEnem(string obj)
    {
        Death[obj] = true;
    }

    public void AddToDict(string obj)
    {
        if (!(Death.ContainsKey(obj)))
        {
            Debug.Log("Success");
            Death.Add(obj, false);
        }
        else
            Debug.Log("Fail");
    }

    public bool getDeath(string obj)
    {
        return (Death[obj]);
    }

    public void ConfirmDeath()
    {
        EnemyDeath = !EnemyDeath;
    }

    public void SwitchBool()
    {
        isNextScene = !isNextScene;
    }
<<<<<<< Updated upstream
=======

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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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
