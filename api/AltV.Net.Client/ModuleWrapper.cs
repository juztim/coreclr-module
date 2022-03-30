using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using AltV.Net.Client.CApi;
using AltV.Net.Client.Elements.Entities;
using AltV.Net.Client.Elements.Factories;
using AltV.Net.Client.Elements.Pools;
using AltV.Net.Client.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AltV.Net.Client
{
    public class ModuleWrapper
    {
        private static Module _module;
        private static Core _core;
        private static IResource _resource;
        private static IntPtr _resourcePointer;
        private static IntPtr _corePointer;
        private static Runtime.CSharpResourceImpl _runtime;

        public static void MainWithAssembly(Assembly resourceAssembly, IntPtr resourcePointer, IntPtr corePointer)
        {
            var library = new Library();
            _resourcePointer = resourcePointer;
            _corePointer = corePointer;
            Console.SetOut(new AltTextWriter());
            Console.SetError(new AltErrorTextWriter());

            var type = typeof(IResource);
            var resource = resourceAssembly.GetTypes().FirstOrDefault(t => t.IsClass && !t.IsAbstract && type.IsAssignableFrom(t));
            if (resource is null)
            {
                throw new Exception("Cannot find resource");
                return;
            }

            unsafe
            {
                var pointer = library.Resource_GetCSharpImpl(_resourcePointer);
                _runtime = new Runtime.CSharpResourceImpl(library, pointer); // todo pool, move somewhere else
                _runtime.SetDelegates();
            }

            _resource = (IResource) Activator.CreateInstance(resource)!;
            Alt.Log("Instance created");
            
            var playerPool = new PlayerPool(_resource.GetPlayerFactory());
            Alt.Log("Player pool created");
            
            var vehiclePool = new VehiclePool(_resource.GetVehicleFactory());
            Alt.Log("Vehicle pool created");
            
            var client = new Core(library, corePointer, playerPool, vehiclePool);
            _core = client;
            Alt.CoreImpl = client;
            
            var module = new Module(client);
            _module = module;
            
            _module.InitPools(playerPool, vehiclePool);
            Alt.Log("Pools initialized");

            _resource.OnStart();
            Alt.Log("Finished");
        }
        
        public static void OnStop()
        {
            _resource.OnStop();
            
            Alt.Log("Stopping timers");
            foreach (var safeTimer in _module.RunningTimers.ToArray())
            {
                safeTimer.Stop();
                safeTimer.Dispose();
            }
            _core.PlayerPool.Dispose();
            _core.VehiclePool.Dispose();

            _runtime.Dispose();
        }
        
        public static void OnCreatePlayer(IntPtr pointer, ushort id)
        {
            _core.OnCreatePlayer(pointer, id);
        }

        public static void OnRemovePlayer(IntPtr pointer)
        {
            _core.OnRemovePlayer(pointer);
        }

        public static void OnCreateVehicle(IntPtr pointer, ushort id)
        {
            _core.OnCreateVehicle(pointer, id);
        }

        public static void OnRemoveVehicle(IntPtr pointer)
        {
            _core.OnRemoveVehicle(pointer);
        }

        public static void OnTick()
        {
            _core.OnTick();
        }

        public static void OnPlayerSpawn()
        {
            _core.OnPlayerSpawn();
        }
        
        public static void OnPlayerDisconnect()
        {
            _core.OnPlayerDisconnect();
        }

        public static void OnPlayerEnterVehicle(IntPtr pointer, byte seat)
        {
            _core.OnPlayerEnterVehicle(pointer, seat);
        }

        public static void OnResourceError(string name)
        {
            _core.OnResourceError(name);
        }
        
        public static void OnResourceStart(string name)
        {
            _core.OnResourceStart(name);
        }
        
        public static void OnResourceStop(string name)
        {
            _core.OnResourceStop(name);
        }
        
        public static void OnKeyDown(uint key)
        {
            var consoleKey = (ConsoleKey) key;
            _core.OnKeyDown(consoleKey);
        }
        
        public static void OnKeyUp(uint key)
        {
            var consoleKey = (ConsoleKey) key;
            _core.OnKeyUp(consoleKey);
        }
        
        public static void OnServerEvent(string name, IntPtr pointer, ulong size)
        {
            var args = new IntPtr[size];
            if (pointer != IntPtr.Zero)
            {
                Marshal.Copy(pointer, args, 0, (int) size);
            }
            
            Alt.Log($"Server event \"{name}\" called. Parsed {args.Length} arguments");
            
            _core.OnServerEvent(name, args);
        }

        public static void OnClientEvent(string name, IntPtr pointer, ulong size)
        {
            var args = new IntPtr[size];
            if (pointer != IntPtr.Zero)
            {
                Marshal.Copy(pointer, args, 0, (int) size);
            }
            
            Alt.Log($"Client event \"{name}\" called. Parsed {args.Length} arguments");
            
            _core.OnClientEvent(name, args);
        }
        
        public static void OnConsoleCommand(string name,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            string[] args, int _)
        {
            args ??= new string[0];
            _core.OnConsoleCommand(name, args);
        }
    }
}