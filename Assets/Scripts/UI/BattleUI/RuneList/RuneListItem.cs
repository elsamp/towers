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
            if (LevelController.Instance.SpendFavour(modifierObject.itemCost))
            {
                tower.AddRune(modifierObject as Gem);
                Debug.Log("Hit TOWER! cost= " + modifierObject.itemCost);
            }
            
        }
    }


}
