using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimator : MonoBehaviour
{
    public AudioClip[] audio;
    Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        audioDict.Add("melee", audio[0]);
        audioDict.Add("heal", audio[1]);
        audioDict.Add("fire", audio[2]);
        audioDict.Add("curse", audio[3]);
        audioDict.Add("ice", audio[4]);
        audioDict.Add("grass", audio[5]);
        audioDict.Add("water", audio[6]);
        audioDict.Add("shoot", audio[7]);
    }

    public IEnumerator Melee(Animator a, Transform t)
    {
        a.CrossFade("Melee", 0.1f);
        AudioSource.PlayClipAtPoint(audioDict["melee"], t.position);
        yield return new WaitForSeconds(0.4f);
    }

    public IEnumerator Heal(Animator a, Transform t)
    {
        a.GetComponent<Animator>().CrossFade("Heal", 0.1f);
        AudioSource.PlayClipAtPoint(audioDict["heal"], t.position);
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
                sword = sword.GetChild(i).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetChild(0);
                break;
            }
        }

        Debug.Log(sword.name);
        a.CrossFade("EquipSword", 0.1f);
        yield return new WaitForSeconds(0.5f);

        sword.GetComponent<MeshRenderer>().enabled = true;
        sword.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator Shoot(Animator a, Transform t)
    {
        a.CrossFade("DrawArrow", 0.1f);
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(audioDict["shoot"], t.position);
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
                sword = sword.GetChild(i).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetChild(0);
                break;
            }
        }
        sword.GetComponent<MeshRenderer>().enabled = false;
        sword.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        a.CrossFade("DisarmSword", 0.1f);
        yield return null;

    }


    public IEnumerator Magic(Animator a, Transform t, string attack)
    {
        if (attack.Equals("grass"))
        {
            a.CrossFade("Magic", 0.1f);
            AudioSource.PlayClipAtPoint(audioDict["grass"], t.position);
            yield return new WaitForSeconds(0.8f);
        }
        else
        {
            a.CrossFade("Magic", 0.1f);
            yield return new WaitForSeconds(0.5f);
            AudioSource.PlayClipAtPoint(audioDict[attack], t.position);
            yield return new WaitForSeconds(0.3f);
        }
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
