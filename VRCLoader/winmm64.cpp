#include "InternalHelpers.hpp"
#include "Loader\VRCLoader.h"

typedef FARPROC(*GetProcAddress_t)(HMODULE hModule, LPCSTR lpProcName);

HINSTANCE winmmDll = 0;
HINSTANCE selfDll = 0;

extern "C" UINT_PTR mProcs[181] = { 0 };
LPCSTR mImportNames[] = { "CloseDriver", "DefDriverProc", "DriverCallback", "DrvGetModuleHandle", "GetDriverModuleHandle", "OpenDriver", "PlaySound", "PlaySoundA", "PlaySoundW", "SendDriverMessage", "WOWAppExit", "auxGetDevCapsA", "auxGetDevCapsW", "auxGetNumDevs", "auxGetVolume", "auxOutMessage", "auxSetVolume", "joyConfigChanged", "joyGetDevCapsA", "joyGetDevCapsW", "joyGetNumDevs", "joyGetPos", "joyGetPosEx", "joyGetThreshold", "joyReleaseCapture", "joySetCapture", "joySetThreshold", "mciDriverNotify", "mciDriverYield", "mciExecute", "mciFreeCommandResource", "mciGetCreatorTask", "mciGetDeviceIDA", "mciGetDeviceIDFromElementIDA", "mciGetDeviceIDFromElementIDW", "mciGetDeviceIDW", "mciGetDriverData", "mciGetErrorStringA", "mciGetErrorStringW", "mciGetYieldProc", "mciLoadCommandResource", "mciSendCommandA", "mciSendCommandW", "mciSendStringA", "mciSendStringW", "mciSetDriverData", "mciSetYieldProc", "midiConnect", "midiDisconnect", "midiInAddBuffer", "midiInClose", "midiInGetDevCapsA", "midiInGetDevCapsW", "midiInGetErrorTextA", "midiInGetErrorTextW", "midiInGetID", "midiInGetNumDevs", "midiInMessage", "midiInOpen", "midiInPrepareHeader", "midiInReset", "midiInStart", "midiInStop", "midiInUnprepareHeader", "midiOutCacheDrumPatches", "midiOutCachePatches", "midiOutClose", "midiOutGetDevCapsA", "midiOutGetDevCapsW", "midiOutGetErrorTextA", "midiOutGetErrorTextW", "midiOutGetID", "midiOutGetNumDevs", "midiOutGetVolume", "midiOutLongMsg", "midiOutMessage", "midiOutOpen", "midiOutPrepareHeader", "midiOutReset", "midiOutSetVolume", "midiOutShortMsg", "midiOutUnprepareHeader", "midiStreamClose", "midiStreamOpen", "midiStreamOut", "midiStreamPause", "midiStreamPosition", "midiStreamProperty", "midiStreamRestart", "midiStreamStop", "mixerClose", "mixerGetControlDetailsA", "mixerGetControlDetailsW", "mixerGetDevCapsA", "mixerGetDevCapsW", "mixerGetID", "mixerGetLineControlsA", "mixerGetLineControlsW", "mixerGetLineInfoA", "mixerGetLineInfoW", "mixerGetNumDevs", "mixerMessage", "mixerOpen", "mixerSetControlDetails", "mmDrvInstall", "mmGetCurrentTask", "mmTaskBlock", "mmTaskCreate", "mmTaskSignal", "mmTaskYield", "mmioAdvance", "mmioAscend", "mmioClose", "mmioCreateChunk", "mmioDescend", "mmioFlush", "mmioGetInfo", "mmioInstallIOProcA", "mmioInstallIOProcW", "mmioOpenA", "mmioOpenW", "mmioRead", "mmioRenameA", "mmioRenameW", "mmioSeek", "mmioSendMessage", "mmioSetBuffer", "mmioSetInfo", "mmioStringToFOURCCA", "mmioStringToFOURCCW", "mmioWrite", "mmsystemGetVersion", "sndPlaySoundA", "sndPlaySoundW", "timeBeginPeriod", "timeEndPeriod", "timeGetDevCaps", "timeGetSystemTime", "timeGetTime", "timeKillEvent", "timeSetEvent", "waveInAddBuffer", "waveInClose", "waveInGetDevCapsA", "waveInGetDevCapsW", "waveInGetErrorTextA", "waveInGetErrorTextW", "waveInGetID", "waveInGetNumDevs", "waveInGetPosition", "waveInMessage", "waveInOpen", "waveInPrepareHeader", "waveInReset", "waveInStart", "waveInStop", "waveInUnprepareHeader", "waveOutBreakLoop", "waveOutClose", "waveOutGetDevCapsA", "waveOutGetDevCapsW", "waveOutGetErrorTextA", "waveOutGetErrorTextW", "waveOutGetID", "waveOutGetNumDevs", "waveOutGetPitch", "waveOutGetPlaybackRate", "waveOutGetPosition", "waveOutGetVolume", "waveOutMessage", "waveOutOpen", "waveOutPause", "waveOutPrepareHeader", "waveOutReset", "waveOutRestart", "waveOutSetPitch", "waveOutSetPlaybackRate", "waveOutSetVolume", "waveOutUnprepareHeader", "waveOutWrite", (LPCSTR)2 };

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
	selfDll = hinstDLL;
	if (fdwReason == DLL_PROCESS_ATTACH)
	{
		char* cPath = new char[MAX_PATH];
		if (GetWindowsDirectory(cPath, MAX_PATH) == 0)
		{
			MessageBox(NULL, "Failed to setup proper Windows path!", "VRCLoader", MB_ICONERROR | MB_OK);
			return FALSE;
		}
		
		std::string strPath = std::string(cPath);
		strPath.append("\\System32\\winmm.dll");

		winmmDll = LoadLibrary(strPath.c_str());
		if (!winmmDll) {
			MessageBox(NULL, "Failed to load winmm.dll!", "VRCLoader", MB_ICONERROR | MB_OK);
			return FALSE;
		}
		
		for (int i = 0; i < 181; i++)
			mProcs[i] = (UINT_PTR)GetProcAddress(winmmDll, mImportNames[i]);
		
		if (strstr(GetCommandLine(), "--no-mods") != NULL)
			return TRUE;
		if (strstr(GetCommandLine(), "--vrcloader.debug") != NULL)
			ConsoleUtils::CreateConsole();
		VRCLoader::Init_Loader();
	}
	else if (fdwReason == DLL_PROCESS_DETACH)
	{
		VRCLoader::Destroy_Loader();
		FreeLibrary(selfDll);
	}
	return TRUE;
}
/*
extern "C" void CloseDriver_wrapper();
extern "C" void DefDriverProc_wrapper();
extern "C" void DriverCallback_wrapper();
extern "C" void DrvGetModuleHandle_wrapper();
extern "C" void GetDriverModuleHandle_wrapper();
extern "C" void OpenDriver_wrapper();
extern "C" void PlaySound_wrapper();
extern "C" void PlaySoundA_wrapper();
extern "C" void PlaySoundW_wrapper();
extern "C" void SendDriverMessage_wrapper();
extern "C" void WOWAppExit_wrapper();
extern "C" void auxGetDevCapsA_wrapper();
extern "C" void auxGetDevCapsW_wrapper();
extern "C" void auxGetNumDevs_wrapper();
extern "C" void auxGetVolume_wrapper();
extern "C" void auxOutMessage_wrapper();
extern "C" void auxSetVolume_wrapper();
extern "C" void joyConfigChanged_wrapper();
extern "C" void joyGetDevCapsA_wrapper();
extern "C" void joyGetDevCapsW_wrapper();
extern "C" void joyGetNumDevs_wrapper();
extern "C" void joyGetPos_wrapper();
extern "C" void joyGetPosEx_wrapper();
extern "C" void joyGetThreshold_wrapper();
extern "C" void joyReleaseCapture_wrapper();
extern "C" void joySetCapture_wrapper();
extern "C" void joySetThreshold_wrapper();
extern "C" void mciDriverNotify_wrapper();
extern "C" void mciDriverYield_wrapper();
extern "C" void mciExecute_wrapper();
extern "C" void mciFreeCommandResource_wrapper();
extern "C" void mciGetCreatorTask_wrapper();
extern "C" void mciGetDeviceIDA_wrapper();
extern "C" void mciGetDeviceIDFromElementIDA_wrapper();
extern "C" void mciGetDeviceIDFromElementIDW_wrapper();
extern "C" void mciGetDeviceIDW_wrapper();
extern "C" void mciGetDriverData_wrapper();
extern "C" void mciGetErrorStringA_wrapper();
extern "C" void mciGetErrorStringW_wrapper();
extern "C" void mciGetYieldProc_wrapper();
extern "C" void mciLoadCommandResource_wrapper();
extern "C" void mciSendCommandA_wrapper();
extern "C" void mciSendCommandW_wrapper();
extern "C" void mciSendStringA_wrapper();
extern "C" void mciSendStringW_wrapper();
extern "C" void mciSetDriverData_wrapper();
extern "C" void mciSetYieldProc_wrapper();
extern "C" void midiConnect_wrapper();
extern "C" void midiDisconnect_wrapper();
extern "C" void midiInAddBuffer_wrapper();
extern "C" void midiInClose_wrapper();
extern "C" void midiInGetDevCapsA_wrapper();
extern "C" void midiInGetDevCapsW_wrapper();
extern "C" void midiInGetErrorTextA_wrapper();
extern "C" void midiInGetErrorTextW_wrapper();
extern "C" void midiInGetID_wrapper();
extern "C" void midiInGetNumDevs_wrapper();
extern "C" void midiInMessage_wrapper();
extern "C" void midiInOpen_wrapper();
extern "C" void midiInPrepareHeader_wrapper();
extern "C" void midiInReset_wrapper();
extern "C" void midiInStart_wrapper();
extern "C" void midiInStop_wrapper();
extern "C" void midiInUnprepareHeader_wrapper();
extern "C" void midiOutCacheDrumPatches_wrapper();
extern "C" void midiOutCachePatches_wrapper();
extern "C" void midiOutClose_wrapper();
extern "C" void midiOutGetDevCapsA_wrapper();
extern "C" void midiOutGetDevCapsW_wrapper();
extern "C" void midiOutGetErrorTextA_wrapper();
extern "C" void midiOutGetErrorTextW_wrapper();
extern "C" void midiOutGetID_wrapper();
extern "C" void midiOutGetNumDevs_wrapper();
extern "C" void midiOutGetVolume_wrapper();
extern "C" void midiOutLongMsg_wrapper();
extern "C" void midiOutMessage_wrapper();
extern "C" void midiOutOpen_wrapper();
extern "C" void midiOutPrepareHeader_wrapper();
extern "C" void midiOutReset_wrapper();
extern "C" void midiOutSetVolume_wrapper();
extern "C" void midiOutShortMsg_wrapper();
extern "C" void midiOutUnprepareHeader_wrapper();
extern "C" void midiStreamClose_wrapper();
extern "C" void midiStreamOpen_wrapper();
extern "C" void midiStreamOut_wrapper();
extern "C" void midiStreamPause_wrapper();
extern "C" void midiStreamPosition_wrapper();
extern "C" void midiStreamProperty_wrapper();
extern "C" void midiStreamRestart_wrapper();
extern "C" void midiStreamStop_wrapper();
extern "C" void mixerClose_wrapper();
extern "C" void mixerGetControlDetailsA_wrapper();
extern "C" void mixerGetControlDetailsW_wrapper();
extern "C" void mixerGetDevCapsA_wrapper();
extern "C" void mixerGetDevCapsW_wrapper();
extern "C" void mixerGetID_wrapper();
extern "C" void mixerGetLineControlsA_wrapper();
extern "C" void mixerGetLineControlsW_wrapper();
extern "C" void mixerGetLineInfoA_wrapper();
extern "C" void mixerGetLineInfoW_wrapper();
extern "C" void mixerGetNumDevs_wrapper();
extern "C" void mixerMessage_wrapper();
extern "C" void mixerOpen_wrapper();
extern "C" void mixerSetControlDetails_wrapper();
extern "C" void mmDrvInstall_wrapper();
extern "C" void mmGetCurrentTask_wrapper();
extern "C" void mmTaskBlock_wrapper();
extern "C" void mmTaskCreate_wrapper();
extern "C" void mmTaskSignal_wrapper();
extern "C" void mmTaskYield_wrapper();
extern "C" void mmioAdvance_wrapper();
extern "C" void mmioAscend_wrapper();
extern "C" void mmioClose_wrapper();
extern "C" void mmioCreateChunk_wrapper();
extern "C" void mmioDescend_wrapper();
extern "C" void mmioFlush_wrapper();
extern "C" void mmioGetInfo_wrapper();
extern "C" void mmioInstallIOProcA_wrapper();
extern "C" void mmioInstallIOProcW_wrapper();
extern "C" void mmioOpenA_wrapper();
extern "C" void mmioOpenW_wrapper();
extern "C" void mmioRead_wrapper();
extern "C" void mmioRenameA_wrapper();
extern "C" void mmioRenameW_wrapper();
extern "C" void mmioSeek_wrapper();
extern "C" void mmioSendMessage_wrapper();
extern "C" void mmioSetBuffer_wrapper();
extern "C" void mmioSetInfo_wrapper();
extern "C" void mmioStringToFOURCCA_wrapper();
extern "C" void mmioStringToFOURCCW_wrapper();
extern "C" void mmioWrite_wrapper();
extern "C" void mmsystemGetVersion_wrapper();
extern "C" void sndPlaySoundA_wrapper();
extern "C" void sndPlaySoundW_wrapper();
extern "C" void timeBeginPeriod_wrapper();
extern "C" void timeEndPeriod_wrapper();
extern "C" void timeGetDevCaps_wrapper();
extern "C" void timeGetSystemTime_wrapper();
extern "C" void timeGetTime_wrapper();
extern "C" void timeKillEvent_wrapper();
extern "C" void timeSetEvent_wrapper();
extern "C" void waveInAddBuffer_wrapper();
extern "C" void waveInClose_wrapper();
extern "C" void waveInGetDevCapsA_wrapper();
extern "C" void waveInGetDevCapsW_wrapper();
extern "C" void waveInGetErrorTextA_wrapper();
extern "C" void waveInGetErrorTextW_wrapper();
extern "C" void waveInGetID_wrapper();
extern "C" void waveInGetNumDevs_wrapper();
extern "C" void waveInGetPosition_wrapper();
extern "C" void waveInMessage_wrapper();
extern "C" void waveInOpen_wrapper();
extern "C" void waveInPrepareHeader_wrapper();
extern "C" void waveInReset_wrapper();
extern "C" void waveInStart_wrapper();
extern "C" void waveInStop_wrapper();
extern "C" void waveInUnprepareHeader_wrapper();
extern "C" void waveOutBreakLoop_wrapper();
extern "C" void waveOutClose_wrapper();
extern "C" void waveOutGetDevCapsA_wrapper();
extern "C" void waveOutGetDevCapsW_wrapper();
extern "C" void waveOutGetErrorTextA_wrapper();
extern "C" void waveOutGetErrorTextW_wrapper();
extern "C" void waveOutGetID_wrapper();
extern "C" void waveOutGetNumDevs_wrapper();
extern "C" void waveOutGetPitch_wrapper();
extern "C" void waveOutGetPlaybackRate_wrapper();
extern "C" void waveOutGetPosition_wrapper();
extern "C" void waveOutGetVolume_wrapper();
extern "C" void waveOutMessage_wrapper();
extern "C" void waveOutOpen_wrapper();
extern "C" void waveOutPause_wrapper();
extern "C" void waveOutPrepareHeader_wrapper();
extern "C" void waveOutReset_wrapper();
extern "C" void waveOutRestart_wrapper();
extern "C" void waveOutSetPitch_wrapper();
extern "C" void waveOutSetPlaybackRate_wrapper();
extern "C" void waveOutSetVolume_wrapper();
extern "C" void waveOutUnprepareHeader_wrapper();
extern "C" void waveOutWrite_wrapper();
extern "C" void ExportByOrdinal2();
*/