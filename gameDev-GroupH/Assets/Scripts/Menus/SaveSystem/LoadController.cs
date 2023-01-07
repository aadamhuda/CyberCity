using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class LoadController : MonoBehaviour
{
    private string[] Saves = new string[] { "SaveOne", "SaveTwo" , "SaveThree" , "SaveFour" , "SaveFive" };
    public void LoadMenu()
    {
        // Opens load menu
        this.gameObject.SetActive(true);
        Debug.Log(this.gameObject.name);

        // If save file exist then display otherwise display no save file
        for (int i = 1; i < this.gameObject.transform.childCount; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/" + this.Saves[i-1] + ".test"))
                this.gameObject.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = this.Saves[i - 1];
            else
                this.gameObject.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No Save data";
        }
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
