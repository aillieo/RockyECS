using System;
using AillieoUtils;
using UnityEngine;

namespace Sample
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        public T LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }
    }
}
