﻿using System;
using System.Collections.Generic;
using ScriptRuntime;
using StorySystem;
using GameFramework;

namespace GameFramework.Story.Values
{
    public sealed class BlackboardGetValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "blackboardget") {
                m_ParamNum = callData.GetParamNum();
                if (m_ParamNum > 0) {
                    m_AttrName.InitFromDsl(callData.GetParam(0));
                }
                if (m_ParamNum > 1) {
                    m_DefaultValue.InitFromDsl(callData.GetParam(1));
                }
            }
        }
        public IStoryValue<object> Clone()
        {
            BlackboardGetValue val = new BlackboardGetValue();
            val.m_ParamNum = m_ParamNum;
            val.m_AttrName = m_AttrName.Clone();
            val.m_DefaultValue = m_DefaultValue.Clone();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;
            if (m_ParamNum > 0) {
                m_AttrName.Evaluate(instance, iterator, args);
            }
            if (m_ParamNum > 1) {
                m_DefaultValue.Evaluate(instance, iterator, args);
            }
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                if (m_AttrName.HaveValue) {
                    string name = m_AttrName.Value;
                    m_HaveValue = true;
                    if (!scene.SceneContext.BlackBoard.TryGetVariable(name, out m_Value)) {
                        if (m_ParamNum > 1) {
                            m_Value = m_DefaultValue.Value;
                        }
                    }
                }
            }
        }
        private int m_ParamNum = 0;
        private IStoryValue<string> m_AttrName = new StoryValue<string>();
        private IStoryValue<object> m_DefaultValue = new StoryValue();
        private bool m_HaveValue;
        private object m_Value;
    }
    public sealed class GetDialogItemValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "getdialogitem") {
                int num = callData.GetParamNum();
                if (num > 1) {
                    m_DlgId.InitFromDsl(callData.GetParam(0));
                    m_Index.InitFromDsl(callData.GetParam(1));
                }
            }
        }
        public IStoryValue<object> Clone()
        {
            GetDialogItemValue val = new GetDialogItemValue();
            val.m_DlgId = m_DlgId.Clone();
            val.m_Index = m_Index.Clone();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;        
            m_DlgId.Evaluate(instance, iterator, args);
            m_Index.Evaluate(instance, iterator, args);
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                if (m_DlgId.HaveValue && m_Index.HaveValue) {
                    m_HaveValue = true;
                    int dlgId = m_DlgId.Value;
                    int index = m_Index.Value;
                    int dlgItemId = TableConfigUtility.GenStoryDlgItemId(dlgId, index);
                    TableConfig.StoryDlg cfg = TableConfig.StoryDlgProvider.Instance.GetStoryDlg(dlgItemId);
                    if (null != cfg) {
                        m_Value = cfg;
                    } else {
                        m_Value = null;
                    }
                }
            }
        }

        private IStoryValue<int> m_DlgId = new StoryValue<int>();
        private IStoryValue<int> m_Index = new StoryValue<int>();
        private bool m_HaveValue;
        private object m_Value;
    }
    public sealed class GetActorIconValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "getactoricon") {
                int num = callData.GetParamNum();
                if (num > 0) {
                    m_Index.InitFromDsl(callData.GetParam(0));
                }
            }
        }
        public IStoryValue<object> Clone()
        {
            GetActorIconValue val = new GetActorIconValue();
            val.m_Index = m_Index.Clone();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;        
            m_Index.Evaluate(instance, iterator, args);
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                if (m_Index.HaveValue) {
                    m_HaveValue = true;
                    int index = m_Index.Value;
                    m_Value = null;
                }
            }
        }

        private IStoryValue<int> m_Index = new StoryValue<int>();
        private bool m_HaveValue;
        private object m_Value;
    }
    public sealed class GetMonsterInfoValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "getmonsterinfo") {
                int num = callData.GetParamNum();
                if (num > 1) {
                    m_CampId.InitFromDsl(callData.GetParam(0));
                    m_Index.InitFromDsl(callData.GetParam(1));
                }
            }
        }
        public IStoryValue<object> Clone()
        {
            GetMonsterInfoValue val = new GetMonsterInfoValue();
            val.m_CampId = m_CampId.Clone();
            val.m_Index = m_Index.Clone();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;        
            m_CampId.Evaluate(instance, iterator, args);
            m_Index.Evaluate(instance, iterator, args);
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                if (m_CampId.HaveValue && m_Index.HaveValue) {
                    m_HaveValue = true;
                    int sceneId = scene.SceneResId;
                    int campId = m_CampId.Value;
                    int index = m_Index.Value;
                    int monstersId = TableConfigUtility.GenLevelMonstersId(sceneId, campId);
                    List<TableConfig.LevelMonster> monsterList;
                    if (TableConfig.LevelMonsterProvider.Instance.TryGetValue(monstersId, out monsterList)) {
                        if (index >= 0 && index < monsterList.Count) {
                            m_Value = monsterList[index];
                        } else {
                            m_Value = null;
                        }
                    } else {
                        m_Value = null;
                    }
                }
            }
        }

        private IStoryValue<int> m_CampId = new StoryValue<int>();
        private IStoryValue<int> m_Index = new StoryValue<int>();
        private bool m_HaveValue;
        private object m_Value;
    }
    public sealed class GetAiDataValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "getaidata") {
                int num = callData.GetParamNum();
                if (num > 1) {
                    m_ObjId.InitFromDsl(callData.GetParam(0));
                    m_DataType.InitFromDsl(callData.GetParam(1));
                }
            }
        }
        public IStoryValue<object> Clone()
        {
            GetAiDataValue val = new GetAiDataValue();
            val.m_ObjId = m_ObjId.Clone();
            val.m_DataType = m_DataType.Clone();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;        
            m_ObjId.Evaluate(instance, iterator, args);
            m_DataType.Evaluate(instance, iterator, args);
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                if (m_ObjId.HaveValue && m_DataType.HaveValue) {
                    m_HaveValue = true;
                    m_Value = null;
                    int objId = m_ObjId.Value;
                    string typeName = m_DataType.Value;
                    EntityInfo npc = scene.SceneContext.GetEntityById(objId);
                    if (null != npc) {
                    }
                }
            }
        }

        private IStoryValue<int> m_ObjId = new StoryValue<int>();
        private IStoryValue<string> m_DataType = new StoryValue<string>();
        private bool m_HaveValue;
        private object m_Value;
    }
    public sealed class GetLeaderIdValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "getleaderid") {
                m_ParamNum = callData.GetParamNum();
                if (m_ParamNum > 0) {
                    m_ObjId.InitFromDsl(callData.GetParam(0));
                }
            }
        }
        public IStoryValue<object> Clone()
        {
            GetLeaderIdValue val = new GetLeaderIdValue();
            val.m_ParamNum = m_ParamNum;
            val.m_ObjId = m_ObjId.Clone();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;
            if (m_ParamNum > 0)
                m_ObjId.Evaluate(instance, iterator, args);
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                m_HaveValue = true;
                if (m_ParamNum > 0) {
                    int objId = m_ObjId.Value;
                    EntityInfo npc = scene.GetEntityById(objId);
                    if (null != npc) {
                        m_Value = npc.GetAiStateInfo().LeaderId;
                    } else {
                        m_Value = 0;
                    }
                } else {
                    m_Value = 0;
                }
            }
        }
        private int m_ParamNum = 0;
        private IStoryValue<int> m_ObjId = new StoryValue<int>();
        private bool m_HaveValue;
        private object m_Value;
    }
    public sealed class GetLeaderTableIdValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "getleadertableid") {
                int num = callData.GetParamNum();
                if (num > 0) {
                    m_ObjId.InitFromDsl(callData.GetParam(0));
                }
            }
        }
        public IStoryValue<object> Clone()
        {
            GetLeaderTableIdValue val = new GetLeaderTableIdValue();
            val.m_ObjId = m_ObjId.Clone();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;
            m_ObjId.Evaluate(instance, iterator, args);
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                if (m_ObjId.HaveValue) {
                    m_HaveValue = true;
                    int objId = m_ObjId.Value;
                    EntityInfo obj = scene.SceneContext.GetEntityById(objId);
                    if (null != obj) {
                        int leaderId = obj.GetAiStateInfo().LeaderId;
                        EntityInfo leader = scene.SceneContext.GetEntityById(leaderId);
                        if (null != leader) {
                            m_Value = leader.GetTableId();
                        } else {
                            m_Value = 0;
                        }
                    } else {
                        m_Value = 0;
                    }
                }
            }
        }

        private IStoryValue<int> m_ObjId = new StoryValue<int>();
        private bool m_HaveValue;
        private object m_Value;
    }
    public sealed class IsClientValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "isclient") {
            }
        }
        public IStoryValue<object> Clone()
        {
            IsClientValue val = new IsClientValue();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;
        
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            m_HaveValue = true;
            m_Value = 0;
        }
        private bool m_HaveValue;
        private object m_Value;
    }
    internal sealed class GetRoomIdValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "getroomid") {
            }
        }
        public IStoryValue<object> Clone()
        {
            GetRoomIdValue val = new GetRoomIdValue();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                m_HaveValue = true;
                m_Value = scene.GetRoomUserManager().RoomId;
            }
        }

        private bool m_HaveValue;
        private object m_Value;
    }
    internal sealed class GetSceneIdValue : IStoryValue<object>
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.CallData callData = param as Dsl.CallData;
            if (null != callData && callData.GetId() == "getsceneid") {
            }
        }
        public IStoryValue<object> Clone()
        {
            GetSceneIdValue val = new GetSceneIdValue();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, object iterator, object[] args)
        {
            m_HaveValue = false;
            TryUpdateValue(instance);
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        private void TryUpdateValue(StoryInstance instance)
        {
            Scene scene = instance.Context as Scene;
            if (null != scene) {
                m_HaveValue = true;
                m_Value = scene.SceneResId;
            }
        }

        private bool m_HaveValue;
        private object m_Value;
    }
}
