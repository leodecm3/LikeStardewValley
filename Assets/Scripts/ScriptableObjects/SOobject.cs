
using UnityEngine;



public class SOobject : ScriptableObject {

    public int baseBuyPrice;
    public int baseSellPrice;

    public Sprite sprite;
    


}







/// <summary>
/// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
///     
///    NOT MINE! I JUST COPY FROM https://forum.unity.com/threads/create-copy-of-scriptableobject-during-runtime.355933/
///   
/// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
/// </summary>
public static class ScriptableObjectExtension
{
    /// <summary>
    /// Creates and returns a clone of any given scriptable object.
    /// </summary>
    public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
    {
        if (scriptableObject == null)
        {
            Debug.LogError($"ScriptableObject was null. Returning default {typeof(T)} object.");
            return (T)ScriptableObject.CreateInstance(typeof(T));
        }
 
        T instance = Object.Instantiate(scriptableObject);
        instance.name = scriptableObject.name; // remove (Clone) from name
        return instance;
    }
}

