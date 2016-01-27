﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace StorySystem.CommonCommands
{
    /// <summary>
    /// assign(@local,value);
    /// or
    /// assign(@@global,value);
    /// </summary>
    internal class AssignCommand : AbstractStoryCommand
    {
        public override IStoryCommand Clone()
        {
            AssignCommand cmd = new AssignCommand();
            cmd.m_VarName = m_VarName;
            cmd.m_Value = m_Value.Clone();
            return cmd;
        }

        protected override void Substitute(object iterator, object[] args)
        {
            m_Value.Substitute(iterator, args);
        }

        protected override void Evaluate(StoryInstance instance)
        {
            m_Value.Evaluate(instance);
        }

        protected override bool ExecCommand(StoryInstance instance, long delta)
        {
            if (m_Value.HaveValue) {
                instance.SetVariable(m_VarName, m_Value.Value);
            }
            return false;
        }

        protected override void Load(Dsl.CallData callData)
        {
            int num = callData.GetParamNum();
            if (num > 1) {
                m_VarName = callData.GetParamId(0);
                m_Value.InitFromDsl(callData.GetParam(1));
            }
        }

        private string m_VarName = null;
        private IStoryValue<object> m_Value = new StoryValue();
    }
    /// <summary>
    /// inc(@local,value);
    /// or
    /// inc(@@global,value);
    /// </summary>
    internal class IncCommand : AbstractStoryCommand
    {
        public override IStoryCommand Clone()
        {
            IncCommand cmd = new IncCommand();
            cmd.m_VarName = m_VarName;
            cmd.m_Value = m_Value.Clone();
            cmd.m_ParamNum = m_ParamNum;
            return cmd;
        }

        protected override void Substitute(object iterator, object[] args)
        {
            m_Value.Substitute(iterator, args);
        }

        protected override void Evaluate(StoryInstance instance)
        {
            m_Value.Evaluate(instance);
        }

        protected override bool ExecCommand(StoryInstance instance, long delta)
        {
            if (m_VarName.StartsWith("@@")) {
                if (null != instance.GlobalVariables) {
                    if (instance.GlobalVariables.ContainsKey(m_VarName)) {
                        object oval = instance.GlobalVariables[m_VarName];
                        if (oval is int) {
                            int ov = StoryValueHelper.CastTo<int>(oval);
                            if (m_ParamNum > 1 && m_Value.HaveValue) {
                                int v = StoryValueHelper.CastTo<int>(m_Value.Value);
                                ov += v;
                                instance.GlobalVariables[m_VarName] = ov;
                            } else {
                                ++ov;
                                instance.GlobalVariables[m_VarName] = ov;
                            }
                        } else {
                            float ov = StoryValueHelper.CastTo<float>(oval);
                            if (m_ParamNum > 1 && m_Value.HaveValue) {
                                float v = StoryValueHelper.CastTo<float>(m_Value.Value);
                                ov += v;
                                instance.GlobalVariables[m_VarName] = ov;
                            } else {
                                ++ov;
                                instance.GlobalVariables[m_VarName] = ov;
                            }
                        }
                    }
                }
            } else if (m_VarName.StartsWith("@")) {
                if (instance.LocalVariables.ContainsKey(m_VarName)) {
                    object oval = instance.LocalVariables[m_VarName];
                    if (oval is int) {
                        int ov = StoryValueHelper.CastTo<int>(oval);
                        if (m_ParamNum > 1 && m_Value.HaveValue) {
                            int v = StoryValueHelper.CastTo<int>(m_Value.Value);
                            ov += v;
                            instance.LocalVariables[m_VarName] = ov;
                        } else {
                            ++ov;
                            instance.LocalVariables[m_VarName] = ov;
                        }
                    } else {
                        float ov = StoryValueHelper.CastTo<float>(oval);
                        if (m_ParamNum > 1 && m_Value.HaveValue) {
                            float v = StoryValueHelper.CastTo<float>(m_Value.Value);
                            ov += v;
                            instance.LocalVariables[m_VarName] = ov;
                        } else {
                            ++ov;
                            instance.LocalVariables[m_VarName] = ov;
                        }
                    }
                }
            }
            return false;
        }

        protected override void Load(Dsl.CallData callData)
        {
            int num = callData.GetParamNum();
            m_ParamNum = num;
            if (num > 0) {
                m_VarName = callData.GetParamId(0);
            }
            if (num > 1) {
                m_Value.InitFromDsl(callData.GetParam(1));
            }
        }

        private int m_ParamNum = 0;
        private string m_VarName = null;
        private IStoryValue<object> m_Value = new StoryValue();
    }
    /// <summary>
    /// dec(@local,value);
    /// or
    /// dec(@@global,value);
    /// </summary>
    internal class DecCommand : AbstractStoryCommand
    {
        public override IStoryCommand Clone()
        {
            DecCommand cmd = new DecCommand();
            cmd.m_VarName = m_VarName;
            cmd.m_Value = m_Value.Clone();
            cmd.m_ParamNum = m_ParamNum;
            return cmd;
        }

        protected override void Substitute(object iterator, object[] args)
        {
            m_Value.Substitute(iterator, args);
        }

        protected override void Evaluate(StoryInstance instance)
        {
            m_Value.Evaluate(instance);
        }

        protected override bool ExecCommand(StoryInstance instance, long delta)
        {
            if (m_VarName.StartsWith("@@")) {
                if (null != instance.GlobalVariables) {
                    if (instance.GlobalVariables.ContainsKey(m_VarName)) {
                        object oval = instance.GlobalVariables[m_VarName];
                        if (oval is int) {
                            int ov = StoryValueHelper.CastTo<int>(oval);
                            if (m_ParamNum > 1 && m_Value.HaveValue) {
                                int v = StoryValueHelper.CastTo<int>(m_Value.Value);
                                ov -= v;
                                instance.GlobalVariables[m_VarName] = ov;
                            } else {
                                --ov;
                                instance.GlobalVariables[m_VarName] = ov;
                            }
                        } else {
                            float ov = StoryValueHelper.CastTo<float>(oval);
                            if (m_ParamNum > 1 && m_Value.HaveValue) {
                                float v = StoryValueHelper.CastTo<float>(m_Value.Value);
                                ov -= v;
                                instance.GlobalVariables[m_VarName] = ov;
                            } else {
                                --ov;
                                instance.GlobalVariables[m_VarName] = ov;
                            }
                        }
                    }
                }
            } else if (m_VarName.StartsWith("@")) {
                if (instance.LocalVariables.ContainsKey(m_VarName)) {
                    object oval = instance.LocalVariables[m_VarName];
                    if (oval is int) {
                        int ov = StoryValueHelper.CastTo<int>(oval);
                        if (m_ParamNum > 1 && m_Value.HaveValue) {
                            int v = StoryValueHelper.CastTo<int>(m_Value.Value);
                            ov -= v;
                            instance.LocalVariables[m_VarName] = ov;
                        } else {
                            --ov;
                            instance.LocalVariables[m_VarName] = ov;
                        }
                    } else {
                        float ov = StoryValueHelper.CastTo<float>(oval);
                        if (m_ParamNum > 1 && m_Value.HaveValue) {
                            float v = StoryValueHelper.CastTo<float>(m_Value.Value);
                            ov -= v;
                            instance.LocalVariables[m_VarName] = ov;
                        } else {
                            --ov;
                            instance.LocalVariables[m_VarName] = ov;
                        }
                    }
                }
            }
            return false;
        }

        protected override void Load(Dsl.CallData callData)
        {
            int num = callData.GetParamNum();
            m_ParamNum = num;
            if (num > 0) {
                m_VarName = callData.GetParamId(0);
            }
            if (num > 1) {
                m_Value.InitFromDsl(callData.GetParam(1));
            }
        }

        private int m_ParamNum = 0;
        private string m_VarName = null;
        private IStoryValue<object> m_Value = new StoryValue();
    }
    /// <summary>
    /// propset(name,value);
    /// </summary>
    internal class PropSetCommand : AbstractStoryCommand
    {
        public override IStoryCommand Clone()
        {
            PropSetCommand cmd = new PropSetCommand();
            cmd.m_VarName = m_VarName.Clone();
            cmd.m_Value = m_Value.Clone();
            return cmd;
        }

        protected override void Substitute(object iterator, object[] args)
        {
            m_VarName.Substitute(iterator, args);
            m_Value.Substitute(iterator, args);
        }

        protected override void Evaluate(StoryInstance instance)
        {
            m_VarName.Evaluate(instance);
            m_Value.Evaluate(instance);
        }

        protected override bool ExecCommand(StoryInstance instance, long delta)
        {
            if (m_VarName.HaveValue && m_Value.HaveValue) {
                string varName = m_VarName.Value;
                instance.SetVariable(varName, m_Value.Value);
            }
            return false;
        }

        protected override void Load(Dsl.CallData callData)
        {
            int num = callData.GetParamNum();
            if (num > 1) {
                m_VarName.InitFromDsl(callData.GetParam(0));
                m_Value.InitFromDsl(callData.GetParam(1));
            }
        }

        private IStoryValue<string> m_VarName = new StoryValue<string>();
        private IStoryValue<object> m_Value = new StoryValue();
    }
    /// <summary>
    /// listset(list,index,value);
    /// </summary>
    internal class ListSetCommand : AbstractStoryCommand
    {
        public override IStoryCommand Clone()
        {
            ListSetCommand cmd = new ListSetCommand();
            cmd.m_ListValue = m_ListValue.Clone();
            cmd.m_IndexValue = m_IndexValue.Clone();
            cmd.m_Value = m_Value.Clone();
            return cmd;
        }

        protected override void Substitute(object iterator, object[] args)
        {
            m_ListValue.Substitute(iterator, args);
            m_IndexValue.Substitute(iterator, args);
            m_Value.Substitute(iterator, args);
        }

        protected override void Evaluate(StoryInstance instance)
        {
            m_ListValue.Evaluate(instance);
            m_IndexValue.Evaluate(instance);
            m_Value.Evaluate(instance);
        }

        protected override bool ExecCommand(StoryInstance instance, long delta)
        {
            if (m_ListValue.HaveValue && m_IndexValue.HaveValue && m_Value.HaveValue) {
                IList listValue = m_ListValue.Value;
                int index = m_IndexValue.Value;
                object val = m_Value.Value;
                int ct = listValue.Count;
                if (index >= 0 && index < ct) {
                    listValue[index] = val;
                } else {
                    listValue.Add(val);
                }
            }
            return false;
        }

        protected override void Load(Dsl.CallData callData)
        {
            int num = callData.GetParamNum();
            if (num > 2) {
                m_ListValue.InitFromDsl(callData.GetParam(0));
                m_IndexValue.InitFromDsl(callData.GetParam(1));
                m_Value.InitFromDsl(callData.GetParam(2));
            }
        }

        private IStoryValue<IList> m_ListValue = new StoryValue<IList>();
        private IStoryValue<int> m_IndexValue = new StoryValue<int>();
        private IStoryValue<object> m_Value = new StoryValue();
    }
}