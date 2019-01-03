using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseMaker : MonoBehaviour
{
    public string poseName;
    public bool autoF5 = false;
    public Transform[] poseTransfroms;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (autoF5)
        {
            LoadPose();
        }
    }
    string PoseName(string name)
    {
        return "pose" + name;
    }
    public Rigidbody2D[] poseStatic;
    public void MaKeStatic()
    {
        foreach (var pose in poseStatic)
        {
            pose.isKinematic = !pose.isKinematic;
        }
    }
    public void SavePose()
    {
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
        PlayerPrefs.SetString(PoseName(poseName), _file);
        Debug.Log(_file);
    }


    public void LoadPose()
    {
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
        t.localPosition = posePosition;
        t.localRotation = poseRotation;
        return true;

    }
}