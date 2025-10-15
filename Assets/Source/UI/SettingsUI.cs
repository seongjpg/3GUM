using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class SettingsUI : MonoBehaviour {
	public Toggle frame30;
	public Toggle frame60;
	public Toggle frame90;
	public Toggle resolutionLow;
	public Toggle resolutionMid;
	public Toggle resolutionHigh;
	public Slider masterVolSlider;
	public Slider bgmVolSlider;
	public Slider sfxVolSlider;
	public Toggle consoleToggle;

	void Start()
	{
		// 1) 초기값 세팅
		frame60.isOn = true; frame30.isOn = false; frame90.isOn = false;
		resolutionMid.isOn = true; resolutionHigh.isOn = false; resolutionLow.isOn = false;

		masterVolSlider.value = PrefSetting.MasterVolume;
		bgmVolSlider.value = PrefSetting.BgmVolume;
		sfxVolSlider.value = PrefSetting.SfxVolume;
		consoleToggle.isOn = PrefSetting.MobileConsoleEnabled;
		/*
		// 2) 이벤트 등록
		frameRateSlider.onValueChanged.AddListener(v =>
		{
			PrefSetting.FrameRate = (int)v;
			PrefSetting.Save();
		});
		resolutionDropdown.onValueChanged.AddListener(i =>
		{
			PrefSetting.ResolutionIndex = i;
			PrefSetting.Save();
		});
		*/
		masterVolSlider.onValueChanged.AddListener(v =>
		{
			PrefSetting.MasterVolume = v;
			PrefSetting.Save();
		});
		bgmVolSlider.onValueChanged.AddListener(v =>
		{
			PrefSetting.BgmVolume = v;
			PrefSetting.Save();
		});
		sfxVolSlider.onValueChanged.AddListener(v =>
		{
			PrefSetting.SfxVolume = v;
			PrefSetting.Save();
		});
		consoleToggle.onValueChanged.AddListener(on =>
		{
			PrefSetting.MobileConsoleEnabled = on;
			PrefSetting.Save();
		});
	}
}