using UnityEngine;

[CreateAssetMenu]

public class SaveData : ScriptableObject
{
    [SerializeField]
    public bool isNextScene = false;
    [SerializeField]
    public float x, y, z = 0;

    public void SaveLocation(float Nx, float Ny, float Nz)
    {
        x = Nx;
        y = Ny;
        z = Nz;
    }

    public void Default()
    {
        x = 0;
        y = 0;
        z = 0;
    }
    
    public void SwitchBool()
    {
        isNextScene = !isNextScene;
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
