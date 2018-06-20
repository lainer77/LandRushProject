using LandRushLibrary.Factory;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using UnityEngine;


public class ItemCreator
{
    private const string PrefabPath = "prefabs/Items/";

    public static GameObject CreateItemObject<T>(ItemID itemdId) where T : GameItem
    {
        GameItem gameItem = ItemFactory.Instance.Create<T>(itemdId);
        GameObject gameObject = GameObject.Instantiate(Resources.Load<GameObject>(PrefabPath + gameItem.PrefabName));
        gameObject.GetComponent<DroppedItem>().ItemInfo = gameItem;

        return gameObject;
    }

    public static GameObject CreateItemObject(ItemID itemdId)
    {
        GameItem gameItem = ItemFactory.Instance.Create(itemdId);
        GameObject gameObject = GameObject.Instantiate(Resources.Load<GameObject>(PrefabPath + gameItem.PrefabName));
        gameObject.GetComponent<DroppedItem>().ItemInfo = gameItem;

        return gameObject;
    }

    public static GameObject CreateItemObject(GameItem gameItem)
    {
        GameObject gameObject =
            GameObject.Instantiate(Resources.Load<GameObject>(PrefabPath + gameItem.PrefabName));
        gameObject.GetComponent<DroppedItem>().ItemInfo = gameItem;

        return gameObject;
    }
}
