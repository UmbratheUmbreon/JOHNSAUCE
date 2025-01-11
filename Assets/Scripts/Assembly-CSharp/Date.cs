using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class Date : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private static void KillUnitySplash()
    {
        Task.Run(() =>
        {
            SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
        });
    }

    private void FixedUpdate()
    {
        text.text = DateTime.Now.ToString();
    }

    [SerializeField]
    private TMP_Text text;
}