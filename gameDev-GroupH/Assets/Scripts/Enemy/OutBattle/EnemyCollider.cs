using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollider : MonoBehaviour
{
    bool inRange = false;
    public PlayerController player;
    public SaveData PosSave;

   private void OnTriggerEnter(Collider Other) {
    Debug.Log("You have entered.");

    inRange = true;

   }

    private void OnTriggerExit(Collider Other) {
        Debug.Log("a");

        inRange = false;
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
