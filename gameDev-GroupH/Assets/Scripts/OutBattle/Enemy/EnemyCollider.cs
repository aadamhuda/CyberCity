using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollider : MonoBehaviour
{
    protected bool inRange = false;
    public GameObject player;
    public SaveData PosSave;
    public active canvas;

    public bool getInRange()
    {
        return inRange;
    }

    private void OnTriggerEnter(Collider Other) {
        Debug.Log("You have entered.");
        inRange = true;
        canvas.showCanvas(inRange);
   }

    private void OnTriggerExit(Collider Other) {
        Debug.Log("a");

        inRange = false;
        canvas.showCanvas(inRange);
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
