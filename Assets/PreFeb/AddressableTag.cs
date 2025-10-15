using UnityEngine.AddressableAssets;
using UnityEngine;

public class AddressableTag : MonoBehaviour {
	// 에디터에선 이 필드에 Addressable 에셋 드래그
	public AssetReference assetRef;

	public string AddressKey => assetRef.RuntimeKey.ToString();
}