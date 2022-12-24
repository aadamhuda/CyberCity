using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimator : MonoBehaviour
{
    public AudioClip[] attackAudio;
    public AudioClip[] hitAudio;
    public GameObject[] attackFX;
    public GameObject[] hitFX;
    Dictionary<string, AudioClip> attackAudioDict = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> hitAudioDict = new Dictionary<string, AudioClip>();
    Dictionary<string, GameObject> attackFXDict = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> hitFXDict = new Dictionary<string, GameObject>();
    
    public float effectSpeed;
    string[] attacks = new string[] { "melee", "heal", "fire", "curse", "ice", "grass", "water", "shoot" };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < attacks.Length; i++)
        {
            attackAudioDict.Add(attacks[i], attackAudio[i]);
            hitAudioDict.Add(attacks[i], hitAudio[i]);
            attackFXDict.Add(attacks[i], attackFX[i]);
            hitFXDict.Add(attacks[i], hitFX[i]);
        }
    }

    public IEnumerator Melee(Animator a, Transform t)
    {
        a.CrossFade("Melee", 0.1f);
        AudioSource.PlayClipAtPoint(attackAudioDict["melee"], t.position);
        yield return new WaitForSeconds(0.4f);
    }

    public IEnumerator Heal(Animator a, Transform t)
    {
        a.GetComponent<Animator>().CrossFade("Heal", 0.1f);
        AudioSource.PlayClipAtPoint(attackAudioDict["heal"], t.position);
        yield return null;
    }

    public IEnumerator EquipBow(Animator a, Transform t)
    {
        a.CrossFade("EquipBow", 0.1f);
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator EquipSword(Animator a, Transform t)
    {
        Transform sword = t.GetChild(0);
        for (int i = 0; i < sword.childCount; i++)
        {
            if (sword.GetChild(i).name.Contains("Hips"))
            {
                sword = sword.GetChild(i).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);
                break;
            }
        }
        Debug.Log(sword.name);
        a.CrossFade("EquipSword", 0.1f);
        yield return new WaitForSeconds(0.5f);

        sword.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator Shoot(Animator a, Transform t)
    {
        a.CrossFade("DrawArrow", 0.1f);
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(attackAudioDict["shoot"], t.position);
    }

    public IEnumerator DisarmBow(Animator a, Transform t)
    {
        a.CrossFade("DisarmBow", 0.3f);
        yield return null;
    }

    public IEnumerator DisarmSword(Animator a, Transform t)
    {
        Transform sword = t.GetChild(0);
        for (int i = 0; i < sword.childCount; i++)
        {
            if (sword.GetChild(i).name.Contains("Hips"))
            {
                sword = sword.GetChild(i).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);
                break;
            }
        }
        sword.gameObject.SetActive(false);
        a.CrossFade("DisarmSword", 0.1f);
        yield return null;

    }
    public IEnumerator Magic(Animator a, Transform player, Transform enemy, string attack)
    {
        a.CrossFade("Magic", 0.1f);
        GameObject instantiatedAttackFX = Instantiate(attackFXDict[attack], player.position + player.forward, player.rotation);
        if (attack.Equals("grass"))
        {
            AudioSource.PlayClipAtPoint(attackAudioDict[attack], player.position);
            yield return new WaitForSeconds(0.9f);

        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            AudioSource.PlayClipAtPoint(attackAudioDict[attack], player.position);
            yield return new WaitForSeconds(0.3f);

        }
        instantiatedAttackFX.GetComponent<Rigidbody>().velocity = player.forward * effectSpeed;
        while (instantiatedAttackFX != null)
        {
            yield return null;
        }
        AudioSource.PlayClipAtPoint(hitAudioDict[attack], enemy.position);
        Destroy(Instantiate(hitFXDict[attack], enemy.position, enemy.rotation), 1);
    }

    public IEnumerator PlayerDeath(BattleUnit target, Animator a)
    {
        Debug.Log("downed");
        a.CrossFade("React", 0.1f);
        if (target.CheckIfDead())
            a.CrossFade("Kneel", 0.1f);
        yield return new WaitForSeconds(0.4f);
    }

    public IEnumerator EnemyDeath(BattleUnit target, Animator a)
    {
        a.CrossFade("React", 0.1f);
        if (target.CheckIfDead())
            a.CrossFade("Death", 0.1f);

        yield return new WaitForSeconds(0.4f);
    }
}