using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBattleFootsteps : MonoBehaviour
{
    public AudioClip[] metalWalk;
    public AudioClip[] metalRun;
    public AudioClip[] metalJump;
    private AudioSource audioSource;
    public const float playerWalkVolume = 0.05f;
    public const float playerRunVolume = 0.1f;
    public const float enemyWalkVolume = 0.1f;
    public const float enemyRunVolume = 0.8f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void WalkStep()
    {
        float walkVolume;
        if (this.name.Equals("Player"))
        {
            walkVolume = playerWalkVolume;
        }
        else
        {
            walkVolume = enemyWalkVolume;
        }

        if (audioSource.volume != walkVolume)
        {
            audioSource.volume = walkVolume;
        }
        int rand = Random.Range(0, metalWalk.Length);
        AudioClip clip = metalWalk[rand];
        audioSource.PlayOneShot(clip);
    }

    private void RunStep()
    {
        float runVolume;
        if (this.name.Equals("Player"))
        {
            runVolume = playerRunVolume;
        }
        else
        {
            runVolume = enemyRunVolume;
        }

        if (audioSource.volume != runVolume)
        {
            audioSource.volume = runVolume;
        }
        int rand = Random.Range(0, metalWalk.Length);
        AudioClip clip = metalWalk[rand];
        audioSource.PlayOneShot(clip);
    }
}
