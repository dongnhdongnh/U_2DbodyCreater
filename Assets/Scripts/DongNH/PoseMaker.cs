using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PoseMaker : MonoBehaviour
{

    public PoseState poseState;
    public bool enableSave = false;
    public bool autoF5 = false;
    public Transform[] poseTransfroms;
    // Use this for initialization
    void Start()
    {
        MaKeStatic(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (autoF5)
        {
            LoadPose(this.poseState.ToString());
        }
    }
    string PoseName(string name)
    {
        return "pose" + name;
    }
    public Rigidbody2D[] poseStatic;
    public void MaKeStatic(bool isStatic)
    {
        foreach (var pose in poseStatic)
        {
            //  pose.simulated = isStatic;
            pose.isKinematic = isStatic;
        }
    }
    public void SavePose()
    {
        if (!enableSave) return;
        Debug.Log("SHOW");
        PoseDataSave data = new PoseDataSave();
        foreach (Transform t in poseTransfroms)
        {
            PoseData _data = new PoseData();
            _data.poseName = t.name;
            _data.posePosition = t.localPosition;
            _data.poseRotation = t.localRotation;
            data.datas.Add(_data);
        }
        string _file = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PoseName(poseState.ToString()), _file);
        Debug.Log(_file);
    }


    public void LoadPose(string poseName)
    {
        MaKeStatic(true);
        if (!PlayerPrefs.HasKey(PoseName(poseName))) return;
        string data = PlayerPrefs.GetString(PoseName(poseName));
        PoseDataSave poseData = JsonUtility.FromJson<PoseDataSave>(data);
        foreach (PoseData pose in poseData.datas)
        {
            foreach (Transform t in poseTransfroms)
            {
                if (pose.LoadPose(t))
                    break;
            }
        }

    }

    //public void SmoothChangePose()
    //{
    //    foreach ()
    //    { }
    //}
}

public enum PoseState
{
    STAND,
    READY,
    JUMP

}

[System.Serializable]
public class PoseDataSave
{

    public List<PoseData> datas = new List<PoseData>();
}

[System.Serializable]
public class PoseData
{

    public string poseName;
    public Vector3 posePosition;
    public Quaternion poseRotation;
    public bool LoadPose(Transform t)
    {
        if (!t.name.Equals(poseName)) return false;
        t.DOLocalMove(posePosition, 0.5f);
        t.DOLocalRotateQuaternion(poseRotation, 0.5f);
        //t.localPosition = posePosition;
        //t.localRotation = poseRotation;
        return true;
    }


}