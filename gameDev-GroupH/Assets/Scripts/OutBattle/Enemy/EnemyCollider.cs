using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class EnemyCollider : MonoBehaviour
{
    private EnemyOutOfCombat [] Enemies;
    [SerializeField]
    private GameObject enemy_object;

    [SerializeField]
    private EnemyPatrols[] plane;


    protected bool inRange = false;
    public GameObject player;
    public SaveData PosSave;
    public TextMeshProUGUI engage;


    private void Start()
    {
        Debug.Log("I dnt like this keyboard : " + gameObject.layer);
/*        EnemyPatrolInfo temp = plane[0].GetPatrols()[0];
        GameObject obj = Instantiate(enemy_object, temp.GetFirst(), Quaternion.identity, gameObject.transform);
        var assigner = obj.GetComponent<EnemyOutOfCombat>();
        assigner.player = player;
        assigner.engage = engage;
        assigner.whatisGround = plane[0].GetLayer();
        assigner.PatrolPoints = temp.GetPath();*/
        Enemies = new EnemyOutOfCombat[4 * PosSave.GetDifficulty()];
        for (int area = 0; area < 4; area++)
        {
            for (int i = 0; i < PosSave.GetDifficulty(); i++)
            {

                EnemyPatrolInfo temp = plane[area].GetPatrols()[i];
                GameObject enem = Instantiate(enemy_object, temp.GetFirst(), Quaternion.identity, gameObject.transform);
                enem.name = area.ToString() + i.ToString();
                var assigner = enem.GetComponent<EnemyOutOfCombat>();
                assigner.player = player;
                assigner.engage = engage;
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

    private void OnTriggerEnter(Collider Other) {
        //Debug.Log("You have entered.");
        inRange = true;
        engage.text = "Press F to engage";
    }

    private void OnTriggerExit(Collider Other) {
        //Debug.Log("a");
        inRange = false;
        engage.text = "";
    }

    protected void BattleScene() {
        PosSave.SaveLocation(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        PosSave.TruthBool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    protected virtual void Update()
    {
        if (inRange == true)
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                PosSave.SaveEnem(gameObject.name);
                BattleScene();
            }
        }

        
    }

}
