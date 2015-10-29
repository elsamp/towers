using UnityEngine;
using System.Collections;

public class OfferingListItem : ListItem {

    public override void performDropActionOnTarget(RaycastHit hit)
    {
        Tower tower = hit.collider.GetComponentInParent<Tower>();

        Debug.Log("Offering -- Hit Something");

        if (tower != null)
        {
            tower.SetOffering(modifierObject as Offering);
            Debug.Log("Hit TOWER!");
        }
    }
}
