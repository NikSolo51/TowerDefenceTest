﻿using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        GameObject Instantiate(GameObject prefab);
        GameObject Instantiate(GameObject prefab, Vector3 at);
        GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation);
    }
}