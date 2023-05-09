using UnityEngine;
using UnityEngine.UI;

public class LevelObjectEntry : MonoBehaviour
{
    [SerializeField]
    private Image image;
    protected GameObject prefab;
    protected string path;

    public void Load(LevelItemData data)
    {
        prefab = data.prefab;
        SetImage(data.sprite);
    }

    private void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void CreateObject()
    {
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        LevelObjectController controller = obj.AddComponent<LevelObjectController>();
        Item item = obj.GetComponent<Item>();
        controller.Load(item);
        //GridObject gridController = obj.AddComponent<GridObject>();
        //gridController.gridSystem = LevelEditor.instance.gridSystem;
        //gridController.size = item.size;
        //gridController.offset = item.offset;
        //Collider2D col = obj.GetComponent<Collider2D>();
        //if (col == null)
        //{
        //    BoxCollider2D boxCol = obj.AddComponent<BoxCollider2D>();
        //    boxCol.size = item.size;
        //    boxCol.offset = item.offset;
        //    boxCol.isTrigger = true;
        //}
        Destroy(item);
    }

}
