using UnityEngine;

public class MobileControlsVisibility : MonoBehaviour
{
    [Header("Root object chứa Joystick + Jump Button")]
    public GameObject mobileControlsRoot;

    private void Start()
    {
        bool isMobile = Application.isMobilePlatform;

#if UNITY_WEBGL && !UNITY_EDITOR
        isMobile = IsMobileBrowser();
#endif

        if (mobileControlsRoot != null)
            mobileControlsRoot.SetActive(isMobile);
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int IsMobileBrowser();
#endif
}