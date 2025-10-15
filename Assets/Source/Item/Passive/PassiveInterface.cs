using UnityEngine;

public interface IPassiveEquip {
	void SavePassive(GameObject user, GameObject prefab); // 패시브를 PlayerManager의 passives와 applyPassives에 저장
}

public interface IFusionWithWeapon {
	void AddEffectToWeapon(GameObject weapon); // weapon에 애니메이션 효과 부여해주기.
	void RemoveEffectFromWeapon(GameObject weapon);
}

public interface IApplyEffect {
	void ApplyEffect(GameObject user); // 실질 효과의 반영 총괄, 외부 호출시 이 함수 호출
	void ApplyPassive(GameObject user); // 패시브 효과를 플레이어 / 프리펩에 반영

	void RemoveFromPM(GameObject user, GameObject passiveName, IApplyEffect pass); // PlayerManager의 passives와 applyPassives에 저장된 패시브를 제거, 
	void LostEffect(GameObject user); // 패시브 효과 제거
}

public interface ISummonPrefab_Pass {
	void SummonPrefab(GameObject user, GameObject prefab); // 프리펩이 소환되는 패시브인 경우 필요
}

public interface IFirePrefab_Pass {
	void ShootPrefab(GameObject prefab); // 프리펩을 발사하는 패시브인 경우 필요
}




