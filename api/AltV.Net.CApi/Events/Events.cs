﻿using System.Numerics;
using System.Runtime.InteropServices;
using AltV.Net.Elements.Entities;

namespace AltV.Net.CApi.ClientEvents
{
    public delegate void TickModuleDelegate();
        
    public delegate void ClientEventModuleDelegate(string name, IntPtr args, ulong size);
    public delegate void ServerEventModuleDelegate(string name, IntPtr args, ulong size);
    public delegate void WebViewEventModuleDelegate(IntPtr webView, string name, IntPtr args, ulong size);
    public delegate void ConsoleCommandModuleDelegate(string name, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] string[] args, int argsSize);
    
    public delegate void CreatePlayerModuleDelegate(IntPtr pointer, ushort id);
    public delegate void RemovePlayerModuleDelegate(IntPtr pointer);
    
    public delegate void CreateVehicleModuleDelegate(IntPtr pointer, ushort id);
    public delegate void RemoveVehicleModuleDelegate(IntPtr pointer);

    public delegate void PlayerSpawnModuleDelegate();
    public delegate void PlayerDisconnectModuleDelegate();
    public delegate void PlayerEnterVehicleModuleDelegate(IntPtr pointer, byte seat);
    public delegate void PlayerLeaveVehicleModuleDelegate(IntPtr pointer, byte seat);
    public delegate void PlayerChangeVehicleSeatModuleDelegate(IntPtr pointer, byte oldSeat, byte newSeat);
    
    public delegate void GameEntityCreateModuleDelegate(IntPtr pointer, byte type);
    public delegate void GameEntityDestroyModuleDelegate(IntPtr pointer, byte type);
    public delegate void RemoveEntityModuleDelegate(IntPtr pointer, BaseObjectType type);

    public delegate void AnyResourceErrorModuleDelegate(string name);
    public delegate void AnyResourceStartModuleDelegate(string name);
    public delegate void AnyResourceStopModuleDelegate(string name);
    
    public delegate void KeyDownModuleDelegate(uint key);
    public delegate void KeyUpModuleDelegate(uint key);

    public delegate void ScreenshotResultModuleDelegate(IntPtr strPtr);
    public delegate void HttpResponseModuleDelegate(int statusCode, string body, IntPtr headerKeys, IntPtr headerValues, int headerSize);
    
    public delegate void ConnectionCompleteModuleDelegate();

    public delegate void GlobalMetaChangeModuleDelegate(string key, IntPtr value, IntPtr oldValue);
    public delegate void GlobalSyncedMetaChangeModuleDelegate(string key, IntPtr value, IntPtr oldValue);
    
    public delegate void LocalMetaChangeModuleDelegate(string key, IntPtr value, IntPtr oldValue);
    public delegate void StreamSyncedMetaChangeModuleDelegate(IntPtr target, BaseObjectType type, string key, IntPtr value, IntPtr oldValue);
    public delegate void SyncedMetaChangeModuleDelegate(IntPtr target, BaseObjectType type, string key, IntPtr value, IntPtr oldValue);

    public delegate void TaskChangeModuleDelegate(int oldTask, int newTask);

    public delegate void WindowFocusChangeModuleDelegate(byte state);
    public delegate void WindowResolutionChangeModuleDelegate(Vector2 oldResolution, Vector2 newResolution);

    public delegate void NetOwnerChangeModuleDelegate(IntPtr target, BaseObjectType type, IntPtr newOwner, IntPtr oldOwner);
}