using UnityEngine;
using KitchenLib.Customs;

namespace KitchenMyMod
{

  public static class MaterialUtils
  {
    public static void ApplyMaterial<T>(this GameObject gameObject, Material[] materials) where T : Renderer
    {
      var comp = gameObject.GetComponent<T>();
      if (comp == null)
        return;

      comp.materials = materials;
    }
    public static void ApplyMaterial(this GameObject gameObject, Material[] materials)
    {
      ApplyMaterial<MeshRenderer>(gameObject, materials);
    }

    public static void ApplyMaterial(this GameObject gameObject, string[] materials)
    {
      ApplyMaterial<MeshRenderer>(gameObject, GetMaterialArray(materials));
    }


    public static void ApplyMaterial(this GameObject gameObject, MaterialFlat[] materials)
    {
      ApplyMaterial<MeshRenderer>(gameObject, GetMaterialArray(materials));
    }

    public static void ApplyMaterial<T>(this GameObject gameObject, string[] materials) where T : Renderer
    {
      ApplyMaterial<T>(gameObject, GetMaterialArray(materials));
    }

    public static void ApplyMaterial<T>(this GameObject gameObject, MaterialFlat[] materials) where T : Renderer
    {
      ApplyMaterial<T>(gameObject, GetMaterialArray(materials));
    }
    public static void ApplyMaterialToChild(this GameObject gameObject, string childName, params string[] materials)
    {
      gameObject.GetChild(childName).ApplyMaterial(GetMaterialArray(materials));
    }

    public static void ApplyMaterialToChild<T>(this GameObject gameObject, string childName, params string[] materials) where T : Renderer
    {
      gameObject.GetChild(childName).ApplyMaterial<T>(GetMaterialArray(materials));
    }
    public static void ApplyMaterialToChild(this GameObject gameObject, string childName, params MaterialFlat[] materials)
    {
      gameObject.GetChild(childName).ApplyMaterial(GetMaterialArray(materials));
    }

    public static void ApplyMaterialToChild<T>(this GameObject gameObject, string childName, params MaterialFlat[] materials) where T : Renderer
    {
      gameObject.GetChild(childName).ApplyMaterial<T>(GetMaterialArray(materials));
    }

    public static Material[] GetMaterialArray(params string[] materials)
    {
      Material[] materialList = new Material[materials.Length];
      for (int i = 0; i < materials.Length; i++)
      {
        string matName = materials[i];
        string formatted = $"mgrant - \"{matName}\"";
        bool flag = CustomMaterials.CustomMaterialsIndex.ContainsKey(formatted);
        if (flag)
        {
          materialList[i] = CustomMaterials.CustomMaterialsIndex[formatted];
        }
        else
        {
          materialList[i] = KitchenLib.Utils.MaterialUtils.GetExistingMaterial(matName);
        }
      }
      return materialList;
    }

    public static Material[] GetMaterialArray(params MaterialFlat[] materials)
    {
      Material[] materialList = new Material[materials.Length];
      for (int i = 0; i < materials.Length; i++)
      {
        string matName = System.Enum.GetName(typeof(MaterialFlat), materials[i]);
        string formatted = GetFormattedName(matName);
        bool flag = CustomMaterials.CustomMaterialsIndex.ContainsKey(formatted);
        if (flag)
        {
          materialList[i] = CustomMaterials.CustomMaterialsIndex[formatted];
        }
        else
        {
          materialList[i] = KitchenLib.Utils.MaterialUtils.GetExistingMaterial(matName);
        }
      }
      return materialList;
    }

    public static Material CreateFlat(string name, Color color, float shininess = 0, float overlayScale = 10)
    {
      Material mat = new Material(Shader.Find("Simple Flat"));
      mat.name = GetFormattedName(name);
      mat.SetColor("_Color0", color);
      mat.SetFloat("_Shininess", shininess);
      mat.SetFloat("_OverlayScale", overlayScale);
      return mat;
    }
    public static Material CreateFlat(string name, int color, float shininess = 0, float overlayScale = 10)
    {
      return CreateFlat(name, ColorFromHex(color), shininess, overlayScale);
    }

    public static Material CreateTransparent(string name, Color color)
    {
      Material mat = new Material(Shader.Find("Simple Transparent"));
      mat.name = GetFormattedName(name);
      mat.SetColor("_Color", color);
      return mat;
    }
    public static Material CreateTransparent(string name, int color, float opacity)
    {
      Color col = ColorFromHex(color);
      col.a = opacity;
      return CreateTransparent(name, col);
    }


    public static Color ColorFromHex(int hex)
    {
      return new Color(((hex & 0xFF0000) >> 16) / 255.0f, ((hex & 0xFF00) >> 8) / 255.0f, (hex & 0xFF) / 255.0f);
    }

    private static string GetFormattedName(string name)
    {
      return $"{Mod.MOD_NAME} - {name}";
    }
  }
}