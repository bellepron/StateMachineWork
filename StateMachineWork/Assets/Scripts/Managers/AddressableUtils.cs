using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class AddressableUtils { }

[Serializable]
public class AssetReferenceMaterial : AssetReferenceT<Material>
{
    public AssetReferenceMaterial(string guid) : base(guid) { }
}

[Serializable]
public class AssetReferenceMesh : AssetReferenceT<Mesh>
{
    public AssetReferenceMesh(string guid) : base(guid) { }
}
