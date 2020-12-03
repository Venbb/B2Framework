﻿using System.Collections.Generic;
using System.Reflection;

namespace B2Framework.Editor
{
    internal static partial class EditorUtility
    {
        internal static partial class Type
        {
            private static readonly string[] AssemblyNames =
            {
                "B2Framework",
                "Assembly-CSharp"
            };

            private static readonly string[] EditorAssemblyNames =
            {
                "B2Framework.Editor",
                "Assembly-CSharp-Editor"
            };

            /// <summary>
            /// 获取指定基类的所有子类的名称。
            /// </summary>
            /// <param name="typeBase">基类类型。</param>
            /// <returns>指定基类的所有子类的名称。</returns>
            internal static string[] GetTypeNames(System.Type typeBase)
            {
                return GetTypeNames(typeBase, AssemblyNames);
            }

            /// <summary>
            /// 获取指定基类的所有子类的名称。
            /// </summary>
            /// <param name="typeBase">基类类型。</param>
            /// <returns>指定基类的所有子类的名称。</returns>
            internal static string[] GetEditorTypeNames(System.Type typeBase)
            {
                return GetTypeNames(typeBase, EditorAssemblyNames);
            }

            private static string[] GetTypeNames(System.Type typeBase, string[] assemblyNames)
            {
                List<string> typeNames = new List<string>();
                foreach (string assemblyName in assemblyNames)
                {
                    Assembly assembly = null;
                    try
                    {
                        assembly = Assembly.Load(assemblyName);
                    }
                    catch
                    {
                        continue;
                    }

                    if (assembly == null)
                    {
                        continue;
                    }

                    System.Type[] types = assembly.GetTypes();
                    foreach (System.Type type in types)
                    {
                        if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                        {
                            typeNames.Add(type.FullName);
                        }
                    }
                }

                typeNames.Sort();
                return typeNames.ToArray();
            }
        }
    }
}