using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBattleFootsteps : MonoBehaviour
{
    public AudioClip[] metalWalk;
    public AudioClip[] metalRun;
    private AudioSource audioSource;
    public const float playerWalkVolume = 0.1f;
    public const float playerRunVolume = 0.2f;
    public const float enemyWalkVolume = 0.5f;
    public const float enemyRunVolume = 1f;

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
        if (this.name.Equals("Player"))
        {
            return;
        }

        if (audioSource.volume != enemyWalkVolume)
        {
            audioSource.volume = enemyWalkVolume;
        }
        int rand = Random.Range(0, metalWalk.Length);
        AudioClip clip = metalWalk[rand];
        audioSource.PlayOneShot(clip);
    }
    private void SlowRunStep()
    {
        if (audioSource.volume != playerWalkVolume)
        {
            audioSource.volume = playerWalkVolume;
        }
        int rand = Random.Range(0, metalWalk.Length);
        AudioClip clip = metalRun[rand];
        audioSource.PlayOneShot(clip);
    }
    private void RunStep()
    {
        if (this.name.Equals("Player"))
        {
            return;
        }

        if (audioSource.volume != enemyRunVolume)
        {
            audioSource.volume = enemyRunVolume;
        }
        int rand = Random.Range(0, metalRun.Length);
        AudioClip clip = metalRun[rand];
        audioSource.PlayOneShot(clip);
    }
}
