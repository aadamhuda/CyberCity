using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem;

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
    public bool tutorial = false;


    public bool can_move = true;

    private void Start()
    {
        int limit;
        Enemies = new EnemyOutOfCombat[4 * PosSave.GetDifficulty()];

        // Instantiate enemies on each area
        for (int area = 0; area < plane.Length; area++)
        {
            limit = Mathf.RoundToInt( plane[area].get_max() / (4-PosSave.GetDifficulty()) );

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
                enem.name = PosSave.get_current_level() + " : " + area.ToString() + "-" + i.ToString();
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
    //pauses all enemies, used for dialogue
    public void StopAll()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            // Freezes all enemies and puts them into Idle anim
            gameObject.transform.GetChild(i).GetComponent<EnemyOutOfCombat>().GetAgent().SetDestination(this.gameObject.transform.GetChild(i).transform.position);
            gameObject.transform.GetChild(i).GetComponent<EnemyOutOfCombat>().can_move = false;
            gameObject.transform.GetChild(i).GetComponent<EnemyOutOfCombat>().GetAnim().SetBool("isRunning", false);
            gameObject.transform.GetChild(i).GetComponent<EnemyOutOfCombat>().GetAnim().SetBool("First" , true);
        }
    }
    //resumes all enemies, used for dialogue
    public void ResumeAll()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            // Resumes all enmies
            gameObject.transform.GetChild(i).GetComponent<EnemyOutOfCombat>().GetAnim().SetBool("First", false);
            gameObject.transform.GetChild(i).GetComponent<EnemyOutOfCombat>().can_move = true;
        }
    }

    // Trigger if player leaves or enter range

    protected void OnTriggerEnter(Collider Other)
    {
        inRange = true;
    }

    protected void OnTriggerExit(Collider Other)
    {
        inRange = false;
    }

    //loads battle scene and saves useful vars beforehand
    protected void BattleScene()
    {
        // Engage combat
        PosSave.tutorial = tutorial;
        PosSave.SaveLocation(player.transform.position);
        PosSave.set_current_level(SceneManager.GetActiveScene().name);
        PosSave.inBattle = true;
        SceneManager.LoadScene("Battle");
    }
    //The Event that occurs when players attack, used in OnAttack in child class
    public void EngageControl(InputValue value)
    {
        if (inRange == true)
        {

            if (value.isPressed)
            {
                // Player engages combat
                StartCoroutine(PlayerEngage());
            }
        }
    }
    //handles all events for player engagement
    public IEnumerator PlayerEngage()
    {
        // Fetches weapon from player
        Transform t = player.transform;
        Animator a = player.GetComponent<Animator>();
        Transform sword = t.GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);
        StartCoroutine(Rotate(player, 0.2f, gameObject.transform.position));
        player.GetComponent<PlayerController>().canMove = false;

        // Animations for weapons

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
        // Spin move :)
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
