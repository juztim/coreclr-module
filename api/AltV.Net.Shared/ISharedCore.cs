﻿using System.Numerics;
using AltV.Net.CApi;
using AltV.Net.Data;
using AltV.Net.Elements.Args;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Net.Shared
{
    public interface ISharedCore : ICApiCore
    {
        IReadOnlyEntityPool<ISharedPlayer> PlayerPool { get; }
        IReadOnlyEntityPool<ISharedVehicle> VehiclePool { get; }
        IReadOnlyBaseBaseObjectPool BaseBaseObjectPool { get; }
        
        ISharedNativeResource Resource { get; }

        ISharedEntity GetEntityById(ushort id);

        IntPtr NativePointer { get; }

        void LogInfo(string message);
        
        uint Hash(string hash);

        /// <summary>
        /// Do NOT use unless you know what you are doing
        /// </summary>
        void LogDebug(string message);

        void LogWarning(string message);

        void LogError(string message);

        void LogColored(string message);

        void LogInfo(IntPtr message);

        /// <summary>
        /// Do NOT use unless you know what you are doing
        /// </summary>
        void LogDebug(IntPtr message);

        void LogWarning(IntPtr message);

        void LogError(IntPtr message);

        void LogColored(IntPtr message);

        string PtrToStringUtf8AndFree(nint str, int size);
        
        #region MValueAdapters
        void RegisterMValueAdapter<T>(IMValueAdapter<T> adapter);
        
        bool ToMValue(object obj, Type type, out MValueConst mValue);
        
        bool FromMValue(in MValueConst mValue, Type type, out object obj);
        
        bool MValueFromObject(object obj, Type type, out object result);
        
        bool IsMValueConvertible(Type type);
        #endregion
        
        #region MValues
        void CreateMValueNil(out MValueConst mValue);

        void CreateMValueBool(out MValueConst mValue, bool value);

        void CreateMValueInt(out MValueConst mValue, long value);

        void CreateMValueUInt(out MValueConst mValue, ulong value);

        void CreateMValueDouble(out MValueConst mValue, double value);

        void CreateMValueString(out MValueConst mValue, string value);

        void CreateMValueList(out MValueConst mValue, MValueConst[] val, ulong size);

        void CreateMValueDict(out MValueConst mValue, string[] keys, MValueConst[] val,
            ulong size);
        
        void CreateMValueBaseObject(out MValueConst mValue, ISharedBaseObject value);

        void CreateMValueFunction(out MValueConst mValue, IntPtr value);
        
        void CreateMValueVector3(out MValueConst mValue, Position value);
        
        void CreateMValueVector2(out MValueConst mValue, Vector2 value);
        
        void CreateMValueRgba(out MValueConst mValue, Rgba value);
        
        void CreateMValueByteArray(out MValueConst mValue, byte[] value);

        void CreateMValue(out MValueConst mValue, object obj);

        void CreateMValues(MValueConst[] mValues, object[] objects);
        #endregion
    }
}