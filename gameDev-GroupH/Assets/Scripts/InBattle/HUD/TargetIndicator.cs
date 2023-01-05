using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Texture topLeftBorder;
    public Texture bottomLeftBorder;
    public Texture topRightBorder;
    public Texture bottomRightBorder;

    public GameObject battleSystemObj;

    public Collider enemyCollider;
    public Camera targetCamera;

    // Update is called once per frame
    void Update()
    {
        enemyCollider = new Collider();
        if (battleSystemObj.GetComponent<BattleSystem>().enemies.Length != 0)
        {
            int target = battleSystemObj.GetComponent<BattleSystem>().target;

            if (target > battleSystemObj.GetComponent<BattleSystem>().enemies.Length - 1)
            {
                target = 0;
                StartCoroutine(battleSystemObj.GetComponent<BattleSystem>().ChangeTarget(0));
            }
            Enemy currEnemy = battleSystemObj.GetComponent<BattleSystem>().enemies[target];
            enemyCollider = currEnemy.GetComponent<Collider>();

        }
        

    }

    void OnGUI()
    {
        if (targetCamera.isActiveAndEnabled && enemyCollider)
        {
            Vector3 boundPoint1 = enemyCollider.bounds.min;
            Vector3 boundPoint2 = enemyCollider.bounds.max;
            Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);
            Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);
            Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
            Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);
            Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);
            Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);

            Vector2[] screenPoints = new Vector2[8];
            screenPoints[0] = targetCamera.WorldToScreenPoint(boundPoint1);
            screenPoints[1] = targetCamera.WorldToScreenPoint(boundPoint2);
            screenPoints[2] = targetCamera.WorldToScreenPoint(boundPoint3);
            screenPoints[3] = targetCamera.WorldToScreenPoint(boundPoint4);
            screenPoints[4] = targetCamera.WorldToScreenPoint(boundPoint5);
            screenPoints[5] = targetCamera.WorldToScreenPoint(boundPoint6);
            screenPoints[6] = targetCamera.WorldToScreenPoint(boundPoint7);
            screenPoints[7] = targetCamera.WorldToScreenPoint(boundPoint8);

            Vector2 topLeftPosition = Vector2.zero;
            Vector2 topRightPosition = Vector2.zero;
            Vector2 bottomLeftPosition = Vector2.zero;
            Vector2 bottomRightPosition = Vector2.zero;

            for (int i = 0; i < screenPoints.Length; i++)
            {
                //Top Left
                if (topLeftPosition.x == 0 || topLeftPosition.x > screenPoints[i].x)
                {
                    topLeftPosition.x = screenPoints[i].x;
                }
                if (topLeftPosition.y == 0 || topLeftPosition.y > Screen.height - screenPoints[i].y)
                {
                    topLeftPosition.y = Screen.height - screenPoints[i].y;
                }
                //Top Right
                if (topRightPosition.x == 0 || topRightPosition.x < screenPoints[i].x)
                {
                    topRightPosition.x = screenPoints[i].x;
                }
                if (topRightPosition.y == 0 || topRightPosition.y > Screen.height - screenPoints[i].y)
                {
                    topRightPosition.y = Screen.height - screenPoints[i].y;
                }
                //Bottom Left
                if (bottomLeftPosition.x == 0 || bottomLeftPosition.x > screenPoints[i].x)
                {
                    bottomLeftPosition.x = screenPoints[i].x;
                }
                if (bottomLeftPosition.y == 0 || bottomLeftPosition.y < Screen.height - screenPoints[i].y)
                {
                    bottomLeftPosition.y = Screen.height - screenPoints[i].y;
                }
                //Bottom Right
                if (bottomRightPosition.x == 0 || bottomRightPosition.x < screenPoints[i].x)
                {
                    bottomRightPosition.x = screenPoints[i].x;
                }
                if (bottomRightPosition.y == 0 || bottomRightPosition.y < Screen.height - screenPoints[i].y)
                {
                    bottomRightPosition.y = Screen.height - screenPoints[i].y;
                }
            }

            GUI.DrawTexture(new Rect(topLeftPosition.x - 16, topLeftPosition.y - 16, 16, 16), topLeftBorder);
            GUI.DrawTexture(new Rect(topRightPosition.x, topRightPosition.y - 16, 16, 16), topRightBorder);
            GUI.DrawTexture(new Rect(bottomLeftPosition.x - 16, bottomLeftPosition.y, 16, 16), bottomLeftBorder);
            GUI.DrawTexture(new Rect(bottomRightPosition.x, bottomRightPosition.y, 16, 16), bottomRightBorder);
        }
    }
}
