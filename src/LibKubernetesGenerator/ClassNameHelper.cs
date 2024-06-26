using CaseExtensions;
using NJsonSchema;
using NSwag;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibKubernetesGenerator
{
    internal class ClassNameHelper : IScriptObjectHelper
    {
        private readonly Dictionary<string, string> classNameMap;
        private readonly Dictionary<JsonSchema, string> schemaToNameMapCooked;

        public ClassNameHelper(OpenApiDocument swagger)
        {
            classNameMap = InitClassNameMap(swagger);
            schemaToNameMapCooked = GenerateSchemaToNameMapCooked(swagger);
        }


        public void RegisterHelper(ScriptObject scriptObject)
        {
            scriptObject.Import(nameof(GetClassName), new Func<JsonSchema, string>(GetClassNameForSchemaDefinition));
        }

        private static Dictionary<JsonSchema, string> GenerateSchemaToNameMapCooked(OpenApiDocument swagger)
        {
            return swagger.Definitions.ToDictionary(x => x.Value, x => x.Key.Replace(".", "").ToPascalCase());
        }

        private Dictionary<string, string> InitClassNameMap(OpenApiDocument doc)
        {
            var map = new Dictionary<string, string>();
            foreach (var kv in doc.Definitions)
            {
                var k = kv.Key;
                var v = kv.Value;
                if (v.ExtensionData?.TryGetValue("x-kubernetes-group-version-kind", out _) == true)
                {
                    var groupVersionKindElements = (object[])v.ExtensionData["x-kubernetes-group-version-kind"];
                    var groupVersionKind = (Dictionary<string, object>)groupVersionKindElements[0];

                    var group = (string)groupVersionKind["group"];
                    var kind = (string)groupVersionKind["kind"];
                    var version = (string)groupVersionKind["version"];
                    map[$"{group}_{kind}_{version}"] = k.Replace(".", "").ToPascalCase();
                }
            }

            return map;
        }

        private string GetClassName(Dictionary<string, object> groupVersionKind)
        {
            var group = (string)groupVersionKind["group"];
            var kind = (string)groupVersionKind["kind"];
            var version = (string)groupVersionKind["version"];

            return classNameMap[$"{group}_{kind}_{version}"];
        }

        public string GetClassName(JsonSchema definition)
        {
            var groupVersionKindElements = (object[])definition.ExtensionData["x-kubernetes-group-version-kind"];
            var groupVersionKind = (Dictionary<string, object>)groupVersionKindElements[0];

            return GetClassName(groupVersionKind);
        }

        public string GetClassNameForSchemaDefinition(JsonSchema definition)
        {
            if (definition.ExtensionData != null &&
                definition.ExtensionData.ContainsKey("x-kubernetes-group-version-kind"))
            {
                return GetClassName(definition);
            }


            return schemaToNameMapCooked[definition];
        }
    }
}
