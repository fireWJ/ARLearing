using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia
{
    public class CameraSetting : MonoBehaviour {

        // Use this for initialization
        void Start()
        {
            var vuforia = VuforiaARController.Instance;
            vuforia.RegisterVuforiaStartedCallback(Started);//回调函数，在启用Vuforia时调用
            vuforia.RegisterOnPauseCallback(OnPause);//回调函数，在暂停时调用
        }
        private void Started()
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
        private void OnPause(bool isPause)
        {
            
        }
        public void OnFocusClik()//单击时的聚焦模式
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
        }
        public void SwitchCameraDirection(CameraDevice.CameraDirection direction)//改变摄像机的前后摄像头
        {
            CameraDevice.Instance.Stop();//先关闭当前后置摄像头
            CameraDevice.Instance.Deinit();
            CameraDevice.Instance.Init(direction);//改为前置摄像头
            CameraDevice.Instance.Start();
        }

        public void FlashLight(bool ON)//闪光灯
        {
            CameraDevice.Instance.SetFlashTorchMode(ON);
        }
    }
}
