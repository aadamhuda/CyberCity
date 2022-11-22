using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyCollider : MonoBehaviour
{
    protected bool inRange = false;
    public GameObject player;
    public SaveData PosSave;
    public TextMeshProUGUI engage;

    public void Start()
    {
        engage.text = "";
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
