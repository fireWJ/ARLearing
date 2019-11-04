using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScreenCut : MonoBehaviour {

    private Camera ARcamera;
	// Use this for initialization
	void Start () {
        ARcamera = GameObject.Find("ARCamera").GetComponent<Camera>();
	}
    public void OnScreenCut()//点击截屏按钮
    {
        //规范所截图的文件命名格式
        System.DateTime now = System.DateTime.Now;//获取当前系统时间
        string times = now.ToString();
        times.Trim();//去除字符串中的空格
        times=times.Replace("/","-");//用—代替字符穿中的/times
        times=times.Replace(":","");
        string fileName = "ARScreenCut" + times + ".jpg";

        //判断运行平台
        if (Application.platform == RuntimePlatform.Android)//如果是安卓平台
        {
            //包含UI的截屏方法
            /* Texture2D tex = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,false);//采用彩色纹理材质，并且不需要映射
             tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);//读取截图
             tex.Apply();

             byte[] bytes = tex.EncodeToPNG();//将图片转为字节形式
             string destination = "sdcard/DCIM/ScreenCut";//图片的存储路径
             if (!Directory.Exists(destination))//判断是否存在之前的存储目录
             {
                 Directory.CreateDirectory(destination);
             }
             string pathSave = destination + "/" + fileName;
             File.WriteAllBytes(pathSave, bytes);*/

            //不包含UI的截屏方法
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 1);
            ARcamera.targetTexture = rt;
            ARcamera.Render();
            RenderTexture.active = rt;

            Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);//采用彩色纹理材质，并且不需要映射
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);//读取截图
            tex.Apply();
            ARcamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);//销毁截图
            byte[] bytes = tex.EncodeToJPG();//将图片转为字节形式
            string destination = "sdcard/截屏";//图片的存储路径
            if (!Directory.Exists(destination))//判断是否存在之前的存储目录
            {
                Directory.CreateDirectory(destination);
            }
            string pathSave = destination + "/" + fileName;
            File.WriteAllBytes(pathSave, bytes);
        }

    }
}
