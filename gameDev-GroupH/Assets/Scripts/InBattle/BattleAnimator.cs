using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BattleAnimator : MonoBehaviour
{
    public AudioClip[] attackAudio;
    public AudioClip[] hitAudio;
    public GameObject[] attackFX;
    public GameObject[] hitFX;
    public GameObject[] debuffFX;
    Dictionary<string, AudioClip> attackAudioDict = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> hitAudioDict = new Dictionary<string, AudioClip>();
    Dictionary<string, GameObject> attackFXDict = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> hitFXDict = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> debuffFXDict = new Dictionary<string, GameObject>();
    public float volume = 1f;

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
        debuffFXDict.Add("fire", debuffFX[0]);
        debuffFXDict.Add("grass", debuffFX[1]);
        debuffFXDict.Add("ice", debuffFX[2]);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("EffectsVolumePreference"))
            volume = (PlayerPrefs.GetFloat("EffectsVolumePreference") / 80) + 1;

        if (PlayerPrefs.HasKey("MasterVolumePreference"))
        {
            Debug.Log("master volume: " + PlayerPrefs.GetFloat("MasterVolumePreference"));
            volume += (PlayerPrefs.GetFloat("MasterVolumePreference") / 80);
            Debug.Log("Volume: " + volume);
        }
    }

    public IEnumerator Melee(Animator a, Transform player, Transform enemy)
    {
        a.CrossFade("Melee", 0.1f);
        AudioSource.PlayClipAtPoint(attackAudioDict["melee"], player.position, volume);
        yield return new WaitForSeconds(0.5f);
        Instantiate(hitFXDict["melee"], enemy.position-(0.2f*player.forward) + (0.2f*player.up), enemy.rotation);
    }

    public IEnumerator Heal(Animator a, Transform t)
    {
        a.GetComponent<Animator>().CrossFade("Heal", 0.1f);
        AudioSource.PlayClipAtPoint(attackAudioDict["heal"], t.position, volume);
        Instantiate(attackFXDict["heal"], t.position - t.up, t.rotation);
        yield return null;
    }

    public IEnumerator EquipBow(Animator a, Transform t)
    {
        Transform bow = t.GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(6);
        a.CrossFade("EquipBow", 0.1f);
        yield return new WaitForSeconds(0.5f);
        bow.gameObject.SetActive(true);
    }
    public IEnumerator DisarmBow(Animator a, Transform t)
    {
        Transform bow = t.GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(6);
        a.CrossFade("DisarmBow", 0.3f);
        yield return new WaitForSeconds(0.5f) ;
        bow.gameObject.SetActive(false);
    }
    public IEnumerator Shoot(Animator a, Transform t, Transform enemy)
    {
        GameObject shootOrb = Instantiate(attackFXDict["shoot"], t.position + t.forward + (0.2f*t.up), t.rotation);
        a.CrossFade("DrawArrow", 0.1f);
        yield return new WaitForSeconds(0.2f);
        AudioSource.PlayClipAtPoint(attackAudioDict["shoot"], t.position, volume);
        yield return new WaitForSeconds(0.3f);
        shootOrb.GetComponent<Rigidbody>().velocity = t.forward * 15;
        AudioSource.PlayClipAtPoint(hitAudioDict["shoot"], enemy.position, volume);
        while (shootOrb != null)
        {
            yield return null;
        }
        Instantiate(hitFXDict["shoot"], enemy.position-(0.2f*t.forward) + (0.2f * t.up), enemy.rotation);
    }

    public IEnumerator EquipSword(Animator a, Transform t)
    {
        Transform sword = t.GetChild(0);
        if (sword.name.Equals("Kachujin G Rosales"))
        {
            sword = sword.GetChild(1).GetChild(2).GetChild(2).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);
        }
        else
        {
            for (int i = 0; i < sword.childCount; i++)
            {
                if (sword.GetChild(i).name.Contains("Hips"))
                {
                    sword = sword.GetChild(i).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);

                    break;
                }
            }
        }
        //        Debug.Log(sword.name);
        a.CrossFade("EquipSword", 0.1f);
        yield return new WaitForSeconds(0.5f);

        sword.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator DisarmSword(Animator a, Transform t)
    {
        Transform sword = t.GetChild(0);
        if (sword.name.Equals("Kachujin G Rosales"))
        {
            sword = sword.GetChild(1).GetChild(2).GetChild(2).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);
        }
        else
        {
            for (int i = 0; i < sword.childCount; i++)
            {
                if (sword.GetChild(i).name.Contains("Hips"))
                {
                    sword = sword.GetChild(i).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);

                    break;
                }
            }
        }
        sword.gameObject.SetActive(false);
        a.CrossFade("DisarmSword", 0.1f);
        yield return null;

    }
    public IEnumerator Magic(Animator a, Transform player, Transform enemy, string attack)
    {
        a.CrossFade("Magic", 0.1f);
        GameObject instantiatedAttackFX = Instantiate(attackFXDict[attack], player.position + player.forward + (0.2f * player.up), player.rotation);
        if (attack.Equals("grass"))
        {
            AudioSource.PlayClipAtPoint(attackAudioDict[attack], player.position, volume);
            yield return new WaitForSeconds(0.9f);

        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            AudioSource.PlayClipAtPoint(attackAudioDict[attack], player.position, volume);
            yield return new WaitForSeconds(0.3f);

        }
        instantiatedAttackFX.GetComponent<Rigidbody>().velocity = player.forward * effectSpeed;
        while (instantiatedAttackFX != null)
        {
            yield return null;
        }
        AudioSource.PlayClipAtPoint(hitAudioDict[attack], enemy.position, volume);
        Instantiate(hitFXDict[attack], enemy.position - (0.2f * player.forward) + (0.2f * enemy.up), enemy.rotation);
    }

    public IEnumerator PlayerDeath(BattleUnit target, Animator a)
    {
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

    public IEnumerator Debuff(Animator a, Transform enemy, string debuff)
    {
        Instantiate(debuffFXDict[debuff], enemy.position, enemy.rotation);
        yield return null;
    }
}