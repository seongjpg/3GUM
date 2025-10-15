using UnityEngine;


public interface IAffectToPlayer {
	void SaveSpell(GameObject collided, GameObject prefab);
}

public interface ISummonPrefab {
	void UsePrefab(GameObject user, GameObject prefab);
}

public interface IFirePrefab {
	void ShootPrefab(GameObject prefab);
}

public interface IUseSpell {
	void UseSpell(GameObject user, GameObject prefab);
	void DiscardSpell(PlayerManager pm);
}


/*
 스펠의 적용
1. 스펠 효과 적용 - 스펠을 위한 프리펩 소환, 플레이어 오브젝트 변경, 플레이어 탄환 변경	
 */