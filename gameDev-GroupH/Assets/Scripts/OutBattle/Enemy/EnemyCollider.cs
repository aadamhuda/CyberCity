using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class EnemyCollider : MonoBehaviour
{
    private EnemyOutOfCombat[] Enemies;
    [SerializeField]
    private GameObject enemy_object;

    [SerializeField]
    private EnemyPatrols[] plane;


    protected bool inRange = false;
    public GameObject player;
    public SaveData PosSave;



    public bool can_move = true;

    private void Start()
    {
        int limit;
        Enemies = new EnemyOutOfCombat[4 * PosSave.GetDifficulty()];

        // Instantiate enemies on each area
        for (int area = 0; area < plane.Length; area++)
        {
            limit = Mathf.RoundToInt( plane[area].get_max() / PosSave.GetDifficulty() );

            if (limit == 0)
                limit = 1;

            // Instantiating a number amount of enemies depending on difficulty
            for (int i = 0; i < limit; i++)
            {
                // Graps the patrol paths
                EnemyPatrolInfo temp = plane[area].GetPatrols()[i];

                // Creates enemy with patrol path
                GameObject enem = Instantiate(enemy_object, temp.GetFirst(), Quaternion.identity, gameObject.transform);

                // Assigning name such that : (  area number  -  enemy number  )
                enem.name = area.ToString() + "-" + i.ToString();
                var assigner = enem.GetComponent<EnemyOutOfCombat>();

                // Assigning variables to each enemy
                assigner.player = player;
                assigner.whatisGround = plane[area].GetLayer();
                assigner.PatrolPoints = temp.GetPath();
                assigner.whatisGround = plane[area].GetLayer();
            }
        }


    }

    public bool getInRange()
    {
        return inRange;
    }

    public void StopAll()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<EnemyOutOfCombat>().can_move = false;
        }
    }
    public void ResumeAll()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<EnemyOutOfCombat>().can_move = true;
        }
    }

    protected void OnTriggerEnter(Collider Other)
    {
        inRange = true;
    }

    protected void OnTriggerExit(Collider Other)
    {
        inRange = false;
    }

    protected void BattleScene()
    {
        // Engage combat
        PosSave.SaveLocation(player.transform.position);
        PosSave.set_current_level(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Battle");
    }

    protected virtual void Update()
    {
        if (inRange == true)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Player engages combat
                StartCoroutine(PlayerEngage());
            }
        }
    }

    public IEnumerator PlayerEngage()
    {
        Transform t = player.transform;
        Animator a = player.GetComponent<Animator>();
        Transform sword = t.GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);
        StartCoroutine(Rotate(player, 0.2f, gameObject.transform.position));
        player.GetComponent<PlayerController>().canMove = false;

        a.CrossFade("EquipSword", 0.1f);
        yield return new WaitForSeconds(0.5f);

        sword.gameObject.SetActive(true);
        a.CrossFade("Melee", 0.1f);
        yield return new WaitForSeconds(1f);

        PosSave.SaveEnem(gameObject.name);
        BattleScene();
    }

    public IEnumerator Rotate(GameObject o, float speed, Vector3 targetPos)
    {
        var transform = o.transform;
        var startRotation = transform.rotation;
        var direction = targetPos - transform.position;
        var targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0;
        targetRotation.z = 0;
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / speed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
