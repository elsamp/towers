using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneListItem : ListItem {


    public override void performDropActionOnTarget(RaycastHit hit)
    {
        Tower tower = hit.collider.GetComponentInParent<Tower>();

        Debug.Log("Hit Something");

        if (tower != null)
        {
            tower.AddRune(modifierObject as Gem);
            LevelController.Instance.SpendFavour(modifierObject.itemCost);
            Debug.Log("Hit TOWER!");
        }
    }


}
