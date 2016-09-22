﻿using System;
using System.Collections.Generic;
using Common;

namespace Quake2DotNet
{
    ///////////////////////////////////////////////////////////////////////////
    // CONSOLE VARIABLE MANAGER
    ///////////////////////////////////////////////////////////////////////////
    public class ConsoleVarNode
    {
        public string Name;
        public string Value;
        public uint Flags;
    }

    public static class ConsoleVarManager
    {
        private static Dictionary<string, ConsoleVarNode> ConsoleVars = new Dictionary<string, ConsoleVarNode>();

        public static void Init()
        {
            Create("Quake2DotNetInit", "true", 0);
            Create("VersionLong", "Quake2DotNet v0.1 alpha", 0);
            Create("VersionShort", "v0.1 alpha", 0);
            Create("ScreenWidth", "1024", 0);
            Create("ScreenHeight", "768", 0);
            Create("ConsoleBackground", "pics/conback2.jpg", 0);
            Create("ConsoleCharacters", "pics/conchars.png", 0);

            Random RandomTrack = new Random();
            int TrackNumber = RandomTrack.Next(1, 10);
            string TrackString = "track" + TrackNumber.ToString().PadLeft(2, '0') + ".mp3";
            Create("MusicTrack", TrackString, 0);
        }

        public static ConsoleVarNode FindVar(string Name)
        {
            if (ConsoleVars.ContainsKey(Name))
            {
                return ConsoleVars[Name];
            }
            else
            {
                return null;
            }
        }

        public static string GetValueToString(string Name)
        {
            ConsoleVarNode Node = FindVar(Name);

            if (Node != null)
            {
                return Node.Value;
            }
            else
            {
                return "";
            }
        }

        public static uint GetValueToUInt(string Name)
        {
            ConsoleVarNode Node = FindVar(Name);

            if (Node != null)
            {
                return ConvertX.ToUInt(Node.Value, 0, "", "trim", 0);
            }
            else
            {
                return 0;
            }
        }

        public static ushort GetValueToUShort(string Name)
        {
            ConsoleVarNode Node = FindVar(Name);

            if (Node != null)
            {
                return ConvertX.ToUShort(Node.Value, 0, "", "trim", 0);
            }
            else
            {
                return 0;
            }
        }

        public static ConsoleVarNode Create(string var_name, string var_value, uint flags)
        {
            ConsoleVarNode Result = FindVar(var_name);

            // If the variable not found, then add a new one.
            if (Result == null)
            {
                ConsoleVarNode Node = new ConsoleVarNode();

                Node.Name = var_name;
                Node.Value = var_value;
                Node.Flags = flags;

                ConsoleVars.Add(var_name, Node);

                Result = Node;
            }

            return Result;
        }

        // With flags
        public static ConsoleVarNode Set(string var_name, string var_value, uint flags)
        {
            ConsoleVarNode Result = FindVar(var_name);

            if (Result != null)
            {
                Result.Name = var_name;
                Result.Value = var_value;
                Result.Flags = flags;
            }

            return Result;
        }

        // Without flags
        public static ConsoleVarNode Set(string var_name, string var_value)
        {
            ConsoleVarNode Result = FindVar(var_name);

            if (Result != null)
            {
                Result.Name = var_name;
                Result.Value = var_value;
            }

            return Result;
        }

        public static ConsoleVarNode SetOrCreate(string var_name, string var_value, uint flags)
        {
            ConsoleVarNode Result = FindVar(var_name);

            if (Result == null)
            {
                Result = Create(var_name, var_value, flags);
            }
            else
            {
                Result = Set(var_name, var_value, flags);
            }

            return Result;
        }
    }
}
