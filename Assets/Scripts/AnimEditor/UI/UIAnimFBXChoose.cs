﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIAnimFBXChoose : UIBasePanel
{
    public ScrollRect scrollRect;
    private ToggleGroup toggleGroup;
    private string itemStr = "Prefabs/CharacterItem";
    private Button sureButton;
    private Button backButton;
    private ToggleModel activeToggle;

    public UIAnimFBXChoose() : base(new UIProperty(UIWindowStyle.Normal, UIWindowMode.NeedBack, UIColliderType.Normal, UIAnimationType.Normal, "UI/ChooseFBXUI"))
    {
    }

    protected override void OnAwakeInitUI()
    {
        scrollRect = TransformExtension.FindComponent<ScrollRect>(CacheTrans, "Scroll View");
        toggleGroup = scrollRect.content.GetComponent<ToggleGroup>();
        sureButton = TransformExtension.FindComponent<Button>(CacheTrans, "SureButton");
        sureButton.onClick.AddListener(SureCharacter);
        backButton = TransformExtension.FindComponent<Button>(CacheTrans, "BackButton");
        backButton.onClick.AddListener(Back);

        LoadFBXItem();
    }

    void LoadFBXItem()
    {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/FBX/");
        FileInfo[] info = d.GetFiles();
        int count = 0;
        for (int i = 0; i < info.Length; i++)
        {
            if (info[i].Name.Contains(".meta"))
            {
                continue;
            }
            GameObject temp = GameObject.Instantiate(Resources.Load(itemStr)) as GameObject;
            temp.transform.parent = toggleGroup.transform;
            temp.transform.localPosition = new Vector3(0, -150 * (count + 1), 0);
            count++;
            temp.GetComponent<Toggle>().group = toggleGroup;

            ToggleModel tm = new ToggleModel();
            tm.itemName = info[i].Name.Remove(info[i].Name.Length - 4);
            Text text = TransformExtension.FindComponent<Text>(temp.transform, "Label");
            text.text = tm.itemName;
            tm.activeToggle = this.activeToggle;

            UnityAction<bool> ua = tm.Click;
            temp.GetComponent<Toggle>().onValueChanged.AddListener(ua);
            //temp.GetComponent<Toggle>().onValueChanged.AddListener((value) => { tm.Click(value); });

        }
        RectTransform rf = (RectTransform)toggleGroup.transform;
        rf.SetHeight(count * 150 + 150);
    }

    void SureCharacter()
    {
        UIWindowMgr.Instance.PopPanel(this);
        UIWindowMgr.Instance.PushPanel<UIRecordAnim>();
        AnimAssetCtrl.Instance.InitAnimInfo();
        AnimAssetCtrl.Instance.AnimStop();
    }

    void Back()
    {
        if (activeToggle != null)
        {
            activeToggle.toggle.isOn = false;
        }
        UIWindowMgr.Instance.PopPanel();
        AnimAssetCtrl.Instance.DestroyModel();
        //UIWindowMgr.Instance.BackPreWindow();
    }

    public override void OnRefresh()
    {
       
    }
}

public class ToggleModel
{
    public Toggle toggle;
    public string itemName;
    public ToggleModel activeToggle;

    public void Click(bool value)
    {
        if (value)
        {
            activeToggle = this;
            itemName.EndsWith(".fbx");

            GameObject temp = GameObject.Instantiate(Resources.Load("FBX/" + itemName)) as GameObject;
            temp.transform.parent = AnimAssetCtrl.Instance.modelRoot;
            temp.transform.localPosition = Vector3.zero;
            AnimAssetCtrl.Instance.SetModel(temp);
        }
        else
        {
            AnimAssetCtrl.Instance.DestroyModel();
        }

    }
}
