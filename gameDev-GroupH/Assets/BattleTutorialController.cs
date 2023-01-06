using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTutorialController : MonoBehaviour
{

    public GameObject narrativeScreen;
    public BattleSystem battleSystem;
    public ScriptDialogue script;
    public DialogueManager dialogue;
    public SaveData saveData;
    bool isTutorial;
    // Start is called before the first frame update
    void Start()
    {
        isTutorial = saveData.tutorial;
        if (isTutorial)
        {
            ActivateTutorial();
        }
        else{
            Destroy(narrativeScreen);
        }
    }

    void ActivateTutorial()
    {
        
        dialogue.Start();
        this.dialogue.Script(0, script);
    }
    public void DeactivateTutorial()
    {
        battleSystem.state = BattleState.PLAYERTURN;
    }
}
