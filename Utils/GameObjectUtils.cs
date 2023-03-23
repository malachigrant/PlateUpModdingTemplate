using UnityEngine;
using System.Collections.Generic;

namespace KitchenMyMod
{

  public static class GameObjectUtils
  {

    public static GameObject GetChild(this GameObject obj, string childName)
    {
      return obj.transform.Find(childName).gameObject;
    }

    public static List<GameObject> GetChildren(this GameObject obj)
    {
      List<GameObject> children = new List<GameObject>();
      for (int i = 0; i < obj.transform.childCount; i++)
      {
        children.Add(obj.transform.GetChild(i).gameObject);
      }
      return children;
    }
  }
}