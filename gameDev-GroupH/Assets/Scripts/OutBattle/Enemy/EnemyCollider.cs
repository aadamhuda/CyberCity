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

        Enemies = new EnemyOutOfCombat[4 * PosSave.GetDifficulty()];

        // Instantiate enemies on each area
        for (int area = 0; area < 4; area++)
        {
            // Instantiating a number amount of enemies depending on difficulty
            for (int i = 0; i < PosSave.GetDifficulty(); i++)
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
        inRange = true;
        engage.text = "Press F to engage";
    }

    private void OnTriggerExit(Collider Other) {
        inRange = false;
        engage.text = "";
    }

    protected void BattleScene() {
        // Engage combat
        PosSave.SaveLocation(player.transform.position);
        PosSave.TruthBool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    protected virtual void Update()
    {
        if (inRange == true)
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Player engages combat
                PosSave.SaveEnem(gameObject.name);
                BattleScene();
            }
        }

        
    }

}
