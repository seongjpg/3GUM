using UnityEngine.AddressableAssets;
using UnityEngine;

public class AddressableTag : MonoBehaviour {
	public AssetReference assetRef;

	public string AddressKey => assetRef.RuntimeKey.ToString();
}