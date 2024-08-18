using UnityEditor;
using UnityEngine;


public class Destroyer
{
    public interface IDestroyListener
    {
        void BeforeDestroy();
    }

    static public void Destroy(GameObject gameObject, float delay = 0f)
    {
        var listener = gameObject.GetComponent<IDestroyListener>();
        if (listener != null)
            listener.BeforeDestroy();
        Object.Destroy(gameObject, delay);
    }

}