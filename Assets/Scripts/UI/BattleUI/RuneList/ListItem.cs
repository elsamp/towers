using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class ListItem : MonoBehaviour {

    public Image itemImage;
    public string itemName;
    public GameObject iconSprite;
    public TowerModifier modifierObject;

    private bool isDragging = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            iconSprite.transform.position = ray.GetPoint(4.0f);
        }
    }

    public void setItemImage(string resourceName)
    {
        this.GetComponentInChildren<Text>().text = resourceName;
        itemName = resourceName;
    }

    public void showIconSprite()
    {
        Debug.Log("Mouse Down " + itemName);
        iconSprite = Instantiate(iconSprite);
        isDragging = true;
    }

    public void hideIconSprite()
    {
        Debug.Log("Mouse Up " + itemName);
        isDragging = false;

        iconSprite.transform.position = new Vector3(0, 0, -90);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
        {
            performDropActionOnTarget(hit);
        }
    }

    public virtual void performDropActionOnTarget(RaycastHit hit)
    {
        // let children override and provide implementation
    }

}
