using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollider : MonoBehaviour
{
    bool inRange = false;
    public PlayerController player;
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
        Debug.Log("Bitch pls");
        inRange = false;
        canvas.showCanvas(inRange);
    }

    private void BattleScene() {
        PosSave.SaveLocation(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        PosSave.SwitchBool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update(){
        if (inRange == true) {
            if (Input.GetKeyDown(KeyCode.F)) {
                BattleScene();
            }
        }
    }

}
