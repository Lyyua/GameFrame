using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class AnimAssetInfo : MonoBehaviour
{
    public List<Nodes[]> forward_stand_shoot = new List<Nodes[]>();
    public List<Nodes[]> back_stand_shoot = new List<Nodes[]>();
    public List<Nodes[]> left_stand_shoot = new List<Nodes[]>();
    public List<Nodes[]> right_stand_shoot = new List<Nodes[]>();
    public List<Nodes[]> forward_stand = new List<Nodes[]>();
    public List<Nodes[]> back_stand = new List<Nodes[]>();
    public List<Nodes[]> left_stand = new List<Nodes[]>();
    public List<Nodes[]> right_stand = new List<Nodes[]>();
    public List<Nodes[]> forward_squat_shoot = new List<Nodes[]>();
    public List<Nodes[]> back_squat_shoot = new List<Nodes[]>();
    public List<Nodes[]> left_squat_shoot = new List<Nodes[]>();
    public List<Nodes[]> right_squat_shoot = new List<Nodes[]>();
    public List<Nodes[]> shoot_squat_stand = new List<Nodes[]>();
    public List<Nodes[]> shoot_lying_stand = new List<Nodes[]>();
    public List<Nodes[]> shoot_stand_squat = new List<Nodes[]>();
    public List<Nodes[]> shoot_lying_squat = new List<Nodes[]>();
    public List<Nodes[]> shoot_stand_lying = new List<Nodes[]>();
    public List<Nodes[]> shoot_squat_lying = new List<Nodes[]>();
    public List<Nodes[]> fire_stand_0 = new List<Nodes[]>();
    public List<Nodes[]> fire_squat_0 = new List<Nodes[]>();
    public List<Nodes[]> fire_lying_0 = new List<Nodes[]>();
    public List<Nodes[]> reload_stand = new List<Nodes[]>();
    public List<Nodes[]> reload_squat = new List<Nodes[]>();
    public List<Nodes[]> reload_lying = new List<Nodes[]>();
    //List<Nodes[]> fire_stand = new List<Nodes[]>();
    //public List<Nodes[]> fire_squat = new List<Nodes[]>();
    public List<Nodes[]> fire_lying = new List<Nodes[]>();
    public List<Nodes[]> dead_stand_hou = new List<Nodes[]>();
    public List<Nodes[]> dead_stand_qian = new List<Nodes[]>();
    public List<Nodes[]> dead_squat = new List<Nodes[]>();
    public List<Nodes[]> dead_lying = new List<Nodes[]>();
    public List<Nodes[]> idle_shootStand = new List<Nodes[]>();  //举枪
    public List<Nodes[]> shootStand_idle = new List<Nodes[]>();
    public List<Nodes[]> run = new List<Nodes[]>();
    public List<Nodes[]> idle_stand_0 = new List<Nodes[]>();

    private void Awake()
    {
        StartCoroutine(LoadAssetSync());
    }

    IEnumerator LoadAssetSync()
    {
        //加载AssetBundle
        WWW www = new WWW("file://" + Application.dataPath + "/AssetBundles/" + "anim");
        yield return www;
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("forward_stand_shoot");
            forward_stand_shoot = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("back_stand_shoot");
            back_stand_shoot = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("left_stand_shoot");
            left_stand_shoot = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("right_stand_shoot");
            right_stand_shoot = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("forward_stand");
            forward_stand = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("back_stand");
            back_stand = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("left_stand");
            left_stand = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("right_stand");
            right_stand = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("forward_squat_shoot");
            forward_squat_shoot = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("back_squat_shoot");
            back_squat_shoot = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("left_squat_shoot");
            left_squat_shoot = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("right_squat_shoot");
            right_squat_shoot = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("shoot_stand_squat");
            shoot_stand_squat = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("shoot_squat_stand");
            shoot_squat_stand = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("idle_stand_0");
            idle_stand_0 = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("fire_stand_0");
            fire_stand_0 = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("fire_squat_0");
            fire_squat_0 = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("reload_stand");
            reload_stand = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("reload_squat");
            reload_squat = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("dead_stand_hou");
            dead_stand_hou = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("dead_stand_qian");
            dead_stand_qian = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("dead_squat");
            dead_squat = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("idle_shootStand");
            idle_shootStand = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("shootStand_idle");
            shootStand_idle = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
        {
            //加载对应动作的.asset文件
            NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>("run");
            run = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        }
    }
}
