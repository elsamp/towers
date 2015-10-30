using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneListItem : ListItem {


    public override void performDropActionOnTarget(RaycastHit hit)
    {
        LevelController.Instance.town.TakeHit(modifierObject.itemCost);

        Tower tower = hit.collider.GetComponentInParent<Tower>();

        Debug.Log("Hit Something");

        if (tower != null)
        {
            tower.AddRune(modifierObject as Gem);
            Debug.Log("Hit TOWER!");
        }
    }


}
