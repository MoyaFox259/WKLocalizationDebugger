using System;
using System.Collections.Generic;
using UnityEngine;

namespace WKLocalizationDebugger.Scanners
{
    public abstract class ObjectScanner : IScanner
    {
        public abstract void Scan();

        public WKAssetDatabase GetFullAssetDatabase() =>
            CL_AssetManager.GetFullCombinedAssetDatabase();

        public void Scan<TObject>(Action<TObject> objectHandler)
            where TObject : UnityEngine.Object
        {
            if (objectHandler is null) return;
            var objects = Resources.FindObjectsOfTypeAll<TObject>();
            if (objects is null || objects.Length == 0) return;
            for (
                var objectIndex = 0;
                objectIndex < objects.Length;
                objectIndex++
            )
            {
                objectHandler.Invoke(objects[objectIndex]);
            }
        }
    }
}

