/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class MyDefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    public GameObject player;//需要识别的物体
    public GameObject[] effect;//存放特效的数组
    private AudioSource audio;//声音播放器

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        Input.backButtonLeavesApp = true;//手机按返回推出程序
        audio = this.GetComponent<AudioSource>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()//识别到了物体
    {
        //生成模型
        GameObject Player = Instantiate(player,transform);
        Player.transform.localPosition = Player.transform.localPosition - new Vector3(0, 1, 0);
        //Player.transform.position = this.transform.position-new Vector3(0,-1,0);
        //播放背景音乐(
        if (!audio.isPlaying)
        {
            audio.Play();
        }
        //生成特效
        this.transform.position = this.transform.position + new Vector3(0, 0.1f, 0);
        GameObject Effect0 = GameObject.Instantiate(effect[0], this.transform.position, Quaternion.identity);
        Effect0.transform.parent = this.transform;
        Destroy(Effect0, 8f);
        GameObject Effect1 = GameObject.Instantiate(effect[1], this.transform.position, Quaternion.identity);
        Effect1.transform.SetParent(this.transform,true);
        Destroy(Effect1, 6f);
    }


    protected virtual void OnTrackingLost()//丢失了物体
    {
        if (audio.isPlaying)
        {
            audio.Pause();
        }
        Destroy(GameObject.Find("player(Clone)"));//只能销毁克隆出来的物体
        Destroy(GameObject.Find("smoke(Clone)"));
        Destroy(GameObject.Find("light(Clone)"));
    }

    #endregion // PROTECTED_METHODS
}
