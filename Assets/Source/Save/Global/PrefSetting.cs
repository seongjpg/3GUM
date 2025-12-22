using UnityEngine;

public static class PrefSetting {
	// 1) PlayerPrefs 키 정의. 이 값을 기반으로 값이 저장된다. 
	private const string KeyFrameRate = "FrameRate";
	private const string KeyResolution = "ResolutionIndex";
	private const string KeyMasterVolume = "MasterVolume";
	private const string KeyBgmVolume = "BgmVolume";
	private const string KeySfxVolume = "SfxVolume";
	private const string KeyMobileConsole = "MobileConsoleEnabled";

	// 2) 기본값, 실제로 반영되는 값
	private const int DefaultFrameRate = 60;
	private const float DefaultMasterVolume = 0.5f;
	private const float DefaultBgmVolume = 0.4f;
	private const float DefaultSfxVolume = 0.4f;
	private const bool DefaultConsoleState = false;

	// 3) 해상도 목록 (플랫폼별로 조정 가능)
	public static readonly Resolution[] Resolutions = Screen.resolutions;

	// 4) 프로퍼티 래퍼
	//프로퍼티 래퍼에서 값을 정의한 키에 값을 매긴다.
	public static int FrameRate
	{
		get => PlayerPrefs.GetInt(KeyFrameRate, DefaultFrameRate);
		set
		{
			PlayerPrefs.SetInt(KeyFrameRate, value);
			Application.targetFrameRate = value;
		}
	}

	public static int ResolutionIndex
	{
		get => PlayerPrefs.GetInt(KeyResolution, Resolutions.Length - 1);
		set
		{
			PlayerPrefs.SetInt(KeyResolution, value);
			var res = Resolutions[Mathf.Clamp(value, 0, Resolutions.Length - 1)];
			Screen.SetResolution(res.width, res.height, Screen.fullScreen);
		}
	}

	public static float MasterVolume
	{
		get => PlayerPrefs.GetFloat(KeyMasterVolume, DefaultMasterVolume);
		set
		{
			PlayerPrefs.SetFloat(KeyMasterVolume, value);
			AudioListener.volume = value;
		}
	}

	public static float BgmVolume
	{
		get => PlayerPrefs.GetFloat(KeyBgmVolume, DefaultBgmVolume);
		set => PlayerPrefs.SetFloat(KeyBgmVolume, value);
	}

	public static float SfxVolume
	{
		get => PlayerPrefs.GetFloat(KeySfxVolume, DefaultSfxVolume);
		set => PlayerPrefs.SetFloat(KeySfxVolume, value);
	}

	public static bool MobileConsoleEnabled
	{
		get => PlayerPrefs.GetInt(KeyMobileConsole, DefaultConsoleState ? 1 : 0) == 1;
		set => PlayerPrefs.SetInt(KeyMobileConsole, value ? 1 : 0);
	}

	// 5) 저장 강제 호출
	public static void Save() => PlayerPrefs.Save();
}
