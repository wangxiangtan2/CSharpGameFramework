using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CSharpCenterClient;
using GameFramework;
using GameFrameworkMessage;

namespace GameFramework
{
    internal partial class UserServer
    {
        /// <summary>
        /// ע�⣬node������Ϣ������Ҫ�ַ���DataProcess���û��߳�����д�����
        /// </summary>
        private void InstallNodeHandlers()
        {
            NodeMessageDispatcher.Init(UserServerConfig.WorldId);
            NodeMessageDispatcher.SetMessageFilter(this.FilterMessage);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.NodeRegister, typeof(GameFrameworkMessage.NodeRegister), typeof(GameFrameworkMessage.NodeRegister), HandleNodeRegister);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.AccountLogin, typeof(GameFrameworkMessage.NodeMessageWithAccount), typeof(GameFrameworkMessage.AccountLogin), HandleAccountLogin);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.RequestNickname, typeof(GameFrameworkMessage.NodeMessageWithAccount), typeof(GameFrameworkMessage.RequestNickname), HandleRequestNickname);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.RoleEnter, typeof(GameFrameworkMessage.NodeMessageWithAccount), typeof(GameFrameworkMessage.RoleEnter), HandleRoleEnter);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.UserHeartbeat, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.UserHeartbeat), HandleUserHeartbeat);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.ChangeName, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.ChangeName), HandleChangeName);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.EnterScene, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.EnterScene), HandleEnterScene);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.ChangeSceneRoom, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.ChangeSceneRoom), HandleChangeSceneRoom);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.RequestSceneRoomInfo, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.RequestSceneRoomInfo), HandleRequestSceneRoomInfo);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.RequestSceneRoomList, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.RequestSceneRoomList), HandleRequestSceneRoomList);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.QuitRoom, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.QuitRoom), HandleQuitRoom);
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.Msg_CLC_StoryMessage, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.Msg_CLC_StoryMessage), HandleStoryMessage);
            //---------------------------------------------------------------------------------------------------------------
            //�����緢���ͻ��˵���Ϣ�Ĺ۲��ߴ�����������Ϣ��handle����Ϊ0�������0��Ӧ�ô�������ϢΪ�ͻ���α�죩��
            //---------------------------------------------------------------------------------------------------------------
            NodeMessageDispatcher.RegisterMessageHandler((int)LobbyMessageDefine.EnterSceneResult, typeof(GameFrameworkMessage.NodeMessageWithGuid), typeof(GameFrameworkMessage.EnterSceneResult), ObserveEnterSceneResult);

            //--------------------------------------------------------------------------------------
            //GMָ����Ϣͳһ��һ�飬���������ŵ�LobbyServer_GmJsonHandler.cs����ڰ�ȫ���
            //��Ҫ�����ע�ͺ�ע����Ϣ������
            //--------------------------------------------------------------------------------------
        }
        //------------------------------------------------------------------------------------------------------
        private bool FilterMessage(NodeMessage msg, int handle, uint seq)
        {
            bool isContinue = true;
            if (handle > 0) {
                if (msg.m_ID == 0) {
                    //�⼸����Ϣ����������ͳ��
                } else {
                    GameFrameworkMessage.NodeMessageWithGuid msgWithGuid = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
                    if (null != msgWithGuid) {
                        ulong guid = msgWithGuid.m_Guid;
                        isContinue = OperationMeasure.Instance.CheckOperation(msgWithGuid.m_Guid);
                        if (!isContinue) {
                            NodeMessage retMsg = new NodeMessage(LobbyMessageDefine.TooManyOperations, guid);
                            NodeMessageDispatcher.SendNodeMessage(handle, retMsg);
                        }
                    }
                }
            }
            return isContinue;
        }
        //------------------------------------------------------------------------------------------------------
        private void ObserveEnterSceneResult(NodeMessage msg, int handle, uint seq)
        {
            if (handle != 0)
                return;
            GameFrameworkMessage.NodeMessageWithGuid headerMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != headerMsg) {
                GameFrameworkMessage.EnterSceneResult protoMsg = msg.m_ProtoData as GameFrameworkMessage.EnterSceneResult;
                if (null != protoMsg) {
                    UserProcessScheduler dataProcess = UserServer.Instance.UserProcessScheduler;
                    UserInfo user = dataProcess.GetUserInfo(headerMsg.m_Guid);
                    if (user != null && protoMsg.result == 0) {
                        user.CurrentState = UserState.Room;
                    }

                }
            }
        }
        //------------------------------------------------------------------------------------------------------
        private void HandleNodeRegister(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeRegister nodeRegMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeRegister;
            if (null != nodeRegMsg) {
                NodeMessage retMsg = new NodeMessage(LobbyMessageDefine.NodeRegisterResult);
                GameFrameworkMessage.NodeRegisterResult nodeRegResultMsg = new NodeRegisterResult();
                nodeRegResultMsg.m_IsOk = true;
                retMsg.m_NodeHeader = nodeRegResultMsg;
                NodeMessageDispatcher.SendNodeMessage(handle, retMsg);
            }
        }
        //------------------------------------------------------------------------------------------------------
        private void HandleAccountLogin(NodeMessage msg, int handle, uint seq)
        {
            StringBuilder stringBuilder = new StringBuilder(1024);
            int size = stringBuilder.Capacity;
            CenterHubApi.TargetName(UserServerConfig.WorldId, handle, stringBuilder, size);
            string node_name = stringBuilder.ToString();

            GameFrameworkMessage.NodeMessageWithAccount loginMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithAccount;
            if (null != loginMsg) {
                GameFrameworkMessage.AccountLogin protoData = msg.m_ProtoData as GameFrameworkMessage.AccountLogin;
                if (null != protoData) {
                    m_UserProcessScheduler.DefaultUserThread.QueueAction(m_UserProcessScheduler.DoAccountLogin, loginMsg.m_Account, protoData.m_Password, protoData.m_ClientInfo, node_name);
                }
            }
        }
        private void HandleRequestNickname(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithAccount nickMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithAccount;
            if (null != nickMsg) {
                GameFrameworkMessage.RequestNickname protoData = msg.m_ProtoData as GameFrameworkMessage.RequestNickname;
                if (null != protoData) {
                    m_UserProcessScheduler.DefaultUserThread.QueueAction(m_UserProcessScheduler.DoRequestNickname, nickMsg.m_Account);
                }
            }
        }
        private void HandleRoleEnter(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithAccount enterMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithAccount;
            if (null != enterMsg) {
                GameFrameworkMessage.RoleEnter protoData = msg.m_ProtoData as GameFrameworkMessage.RoleEnter;
                if (null != protoData) {
                    m_UserProcessScheduler.DefaultUserThread.QueueAction(m_UserProcessScheduler.DoRoleEnter, enterMsg.m_Account, protoData.m_Nickname);
                }
            }
        }
        //------------------------------------------------------------------------------------------------------
        private void HandleUserHeartbeat(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithGuid heartbeatMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != heartbeatMsg) {
                if (null == m_UserProcessScheduler.GetUserInfo(heartbeatMsg.m_Guid)) {
                    NodeMessage retMsg = new NodeMessage(LobbyMessageDefine.KickUser, heartbeatMsg.m_Guid);
                    NodeMessageDispatcher.SendNodeMessage(handle, retMsg);
                    LogSys.Log(LOG_TYPE.DEBUG, "HandleUserHeartbeat, guid:{0} can't found, kick.", heartbeatMsg.m_Guid);
                } else {
                    //echo
                    NodeMessageDispatcher.SendNodeMessage(handle, msg);
                    //�߼�����
                    m_UserProcessScheduler.GetUserThread(heartbeatMsg.m_Guid).QueueAction(m_UserProcessScheduler.DoUserHeartbeat, heartbeatMsg.m_Guid);
                }
            }
        }
        private void HandleChangeName(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithGuid nameMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != nameMsg) {
                GameFrameworkMessage.ChangeName protoData = msg.m_ProtoData as GameFrameworkMessage.ChangeName;
                if (null != protoData) {
                    m_UserProcessScheduler.DefaultUserThread.QueueAction(m_UserProcessScheduler.DoChangeName, nameMsg.m_Guid, protoData.m_Nickname);
                }
            }
        }
        private void HandleEnterScene(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithGuid headerMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != headerMsg) {
                GameFrameworkMessage.EnterScene protoMsg = msg.m_ProtoData as GameFrameworkMessage.EnterScene;
                if (null != protoMsg) {
                    ulong guid = headerMsg.m_Guid;
                    int sceneId = protoMsg.m_SceneId;
                    int wantRoomId = protoMsg.m_RoomId;

                    UserProcessScheduler dataProcess = UserServer.Instance.UserProcessScheduler;
                    UserInfo user = dataProcess.GetUserInfo(guid);
                    if (user != null) {
                        Msg_LB_RequestEnterScene builder = new Msg_LB_RequestEnterScene();
                        Msg_LB_BigworldUserBaseInfo baseInfoBuilder = new Msg_LB_BigworldUserBaseInfo();

                        baseInfoBuilder.AccountId = user.AccountId;
                        baseInfoBuilder.NodeName = user.NodeName;
                        baseInfoBuilder.WorldId = UserServerConfig.WorldId;
                        baseInfoBuilder.ClientInfo = user.ClientInfo;
                        baseInfoBuilder.StartServerTime = UserServerConfig.StartServerTime;
                        baseInfoBuilder.FightingCapacity = user.FightingCapacity;

                        builder.BaseInfo = baseInfoBuilder;
                        builder.User = UserThread.BuildRoomUserInfo(user, 0, 0);
                        builder.SceneId = sceneId;
                        builder.WantRoomId = wantRoomId;
                        builder.FromSceneId = user.SceneId;
                        UserServer.Instance.BigworldChannel.Send(builder);
                    }
                }
            }
        }
        private void HandleChangeSceneRoom(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithGuid headerMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != headerMsg) {
                GameFrameworkMessage.ChangeSceneRoom protoMsg = msg.m_ProtoData as GameFrameworkMessage.ChangeSceneRoom;
                if (null != protoMsg) {
                    ulong guid = headerMsg.m_Guid;
                    int sceneId = protoMsg.m_SceneId;
                    int wantRoomId = protoMsg.m_RoomId;
                    byte[] originalMsgData = msg.m_OriginalMsgData;
                    
                    UserInfo info = UserServer.Instance.UserProcessScheduler.GetUserInfo(guid);
                    if (null != info) {
                        UserServer.Instance.ForwardToBigworld(info, originalMsgData);
                    }
                }
            }
        }
        private void HandleRequestSceneRoomInfo(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithGuid headerMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != headerMsg) {
                ulong guid = headerMsg.m_Guid;
                byte[] originalMsgData = msg.m_OriginalMsgData;

                UserInfo info = UserServer.Instance.UserProcessScheduler.GetUserInfo(guid);
                if (null != info) {
                    UserServer.Instance.ForwardToBigworld(info, originalMsgData);
                }
            }
        }
        private void HandleRequestSceneRoomList(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithGuid headerMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != headerMsg) {
                ulong guid = headerMsg.m_Guid;
                byte[] originalMsgData = msg.m_OriginalMsgData;

                UserInfo info = UserServer.Instance.UserProcessScheduler.GetUserInfo(guid);
                if (null != info) {
                    UserServer.Instance.ForwardToBigworld(info, originalMsgData);
                }
            }
        }
        private void HandleQuitRoom(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithGuid headerMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != headerMsg) {
                GameFrameworkMessage.QuitRoom protoMsg = msg.m_ProtoData as GameFrameworkMessage.QuitRoom;
                if (null != protoMsg) {
                    ulong guid = headerMsg.m_Guid;
                    bool is_quit_room = protoMsg.m_IsQuitRoom;
                    byte[] originalMsgData = msg.m_OriginalMsgData;

                    UserInfo user = UserServer.Instance.UserProcessScheduler.GetUserInfo(guid);
                    if (user != null) {
                        if (user.CurrentState == UserState.Room) {
                            UserServer.Instance.ForwardToBigworld(user, originalMsgData);
                        } else {
                            user.CurrentState = UserState.Online;
                        }
                        LogSys.Log(LOG_TYPE.INFO, "QuitRoom Guid {0} state {1}", guid, user.CurrentState);
                    }
                }
            }
        }
        private void HandleStoryMessage(NodeMessage msg, int handle, uint seq)
        {
            GameFrameworkMessage.NodeMessageWithGuid storyMsg = msg.m_NodeHeader as GameFrameworkMessage.NodeMessageWithGuid;
            if (null != storyMsg) {
                GameFrameworkMessage.Msg_CLC_StoryMessage protoData = msg.m_ProtoData as GameFrameworkMessage.Msg_CLC_StoryMessage;
                if (null != protoData) {
                    ulong guid = storyMsg.m_Guid;
                    UserThread userThread = m_UserProcessScheduler.GetUserThread(guid);
                    if (null != userThread) {
                        //�ͻ��˷�������Ϣ������ǰ׺client����ֱֹ�ӵ��÷��������߼�����������Ϣ������clientǰ׺����
                        string msgId = string.Format("client:{0}", protoData.m_MsgId);
                        ArrayList args = new ArrayList();
                        args.Add(guid);
                        for (int i = 0; i < protoData.m_Args.Count; i++) {
                            switch (protoData.m_Args[i].val_type) {
                                case LobbyArgType.NULL://null
                                    args.Add(null);
                                    break;
                                case LobbyArgType.INT://int
                                    args.Add(int.Parse(protoData.m_Args[i].str_val));
                                    break;
                                case LobbyArgType.FLOAT://float
                                    args.Add(float.Parse(protoData.m_Args[i].str_val));
                                    break;
                                default://string
                                    args.Add(protoData.m_Args[i].str_val);
                                    break;
                            }
                        }
                        object[] objArgs = args.ToArray();
                        userThread.QueueAction(userThread.SendStoryMessage, msgId, objArgs);
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------------------------
    }
}