using UnityEngine;

[CreateAssetMenu(menuName = "Passive/Penetrate")]

public class Penetrate : ScriptableObject, IPassiveEquip, IFusionWithWeapon, IApplyEffect {
	public void SavePassive(GameObject user, GameObject prefab)
	{
		if (user != null && prefab != null)
		{
			PlayerManager pm = user.GetComponent<PlayerManager>();
			GameObject save = Instantiate(
					prefab,
					user.transform.position,
					Quaternion.identity,
					user.transform
					);
			pm.passive.Add(save);
			pm.applyPassives.Add(this);
			ApplyPassive(user);
            save.SetActive(false);
        }
		else
		{
			Debug.Log("플레이어 존재 안한다!(by penetrate)");
		}
	}

	public void AddEffectToWeapon(GameObject weapon)
	{
		//추후 추가 요망
		return;
	}
	public void RemoveEffectFromWeapon(GameObject weapon)
	{
		// 추후 추가 요망
		return;
	}

	public void ApplyPassive(GameObject user)
	{
		PlayerMovement pMov = user.GetComponent<PlayerMovement>();
		PlayerManager pm = user.GetComponentInParent<PlayerManager>();
		if (pMov.playerWeapon != null && pm.isUsingBullet)
		{
			PlayerBullet pb = pMov.playerWeapon.GetComponent<PlayerBullet>();
			pb.destroyByCollision = false; // 근접 무기일때에는 어떻게 처리해야할까?
		}
	}

	public void ApplyEffect(GameObject user)
	{
		//penetrate는 이곳에 구현할 것이 없어서 별도의 효과 적용하는 함수가 필요없다. 호출될 때 그대로 리턴
		//Debug.Log("penetrate 효과 적용중(사실 이미 적용됨)");
		return;

	}

	
	public void LostEffect(GameObject user)
	{
		PlayerMovement pMov = user.GetComponent<PlayerMovement>();
		PlayerManager pm = user.GetComponent <PlayerManager>();
		if (pMov.playerWeapon != null && pm.isUsingBullet)
		{
			PlayerBullet pb = pMov.playerWeapon.GetComponent<PlayerBullet>();
			pb.destroyByCollision = true;
		}
	}

	public void RemoveFromPM(GameObject user, GameObject passiveName, IApplyEffect pass)
	{
		PlayerManager pm = user.GetComponent<PlayerManager>();
		PlayerMovement pMov = user.GetComponent<PlayerMovement>();
		PlayerBullet pb = pMov.playerWeapon.GetComponent<PlayerBullet>();
		LostEffect(user);
		//RemoveEffectFromWeapon(pb);
		pm.passive.Remove(passiveName);
		pm.applyPassives.Remove(pass);
	}
}
