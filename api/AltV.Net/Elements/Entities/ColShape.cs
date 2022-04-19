using System;
using System.Numerics;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Net.Elements.Entities
{
    public class ColShape : WorldObject, IColShape
    {
        public IntPtr ColShapeNativePointer { get; }
        public override IntPtr NativePointer => ColShapeNativePointer;
        
        private static IntPtr GetWorldObjectPointer(ICore core, IntPtr nativePointer)
        {
            unsafe
            {
                return core.Library.Shared.ColShape_GetWorldObject(nativePointer);
            }
        }
        
        public bool IsPlayersOnly
        {
            get
            {
                unsafe
                {
                    CheckIfEntityExists();
                    return Core.Library.Server.ColShape_IsPlayersOnly(ColShapeNativePointer) == 1;
                }
            }
            set
            {
                unsafe
                {
                    CheckIfEntityExists();
                    Core.Library.Server.ColShape_SetPlayersOnly(ColShapeNativePointer, value ? (byte) 1 : (byte) 0);
                }
            }
        }

        public ColShapeType ColShapeType
        {
            get
            {
                unsafe
                {
                    CheckIfEntityExists();
                    return (ColShapeType) Core.Library.Server.ColShape_GetColShapeType(ColShapeNativePointer);
                }
            }
        }

        public ColShape(ICore core, IntPtr nativePointer) : base(core, GetWorldObjectPointer(core, nativePointer), BaseObjectType.ColShape)
        {
            ColShapeNativePointer = nativePointer;
        }

        public ColShape(ICore core, IntPtr nativePointer, BaseObjectType baseObjectType) : base(core, GetWorldObjectPointer(core, nativePointer), baseObjectType)
        {
            ColShapeNativePointer = nativePointer;
        }

        public bool IsPointIn(Vector3 point)
        {
            CheckIfEntityExists();
            
            unsafe
            {
                return Core.Library.Shared.ColShape_IsPointIn(ColShapeNativePointer, point) == 1;
            }
        }

        public bool IsEntityIn(ISharedEntity entity)
        {
            CheckIfEntityExists();
            entity.CheckIfEntityExists();
            
            unsafe
            {
                return Core.Library.Shared.ColShape_IsEntityIn(ColShapeNativePointer, entity.EntityNativePointer) == 1;
            }
        }

        public bool IsEntityIn(IEntity entity)
        {
            return IsEntityIn((ISharedEntity) entity);
        }
        
        [Obsolete("Use IsEntityIn instead")]
        public bool IsPlayerIn(IPlayer player)
        {
            Alt.LogWarning("colShape.IsPlayerIn is deprecated, use colShape.IsEntityIn instead");
            return IsEntityIn(player);
        }

        [Obsolete("Use IsEntityIn instead")]
        public bool IsVehicleIn(IVehicle vehicle)
        {
            Alt.LogWarning("colShape.IsVehicleIn is deprecated, use colShape.IsEntityIn instead");
            return IsEntityIn(vehicle);
        }

        public virtual void Remove()
        {
            Alt.RemoveColShape(this);
        }
    }
}