using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpEngine.Core;

public static class ShaderType 
{
    public const int NONE = -1;
    public const int Vertex = 0;
    public const int Fragment = 1;
}

public class ResourceManager(Assembly assembly)
{
    public ShaderProgramSource GetShaderSource(string source)
    {
        var names = assembly.GetManifestResourceNames();
        var file = names.FirstOrDefault(x => x.EndsWith($"Shaders.{source}.shader"));
        var stream = assembly.GetManifestResourceStream(file);
        using var reader = new StreamReader(stream);

        var dict = new string[2];
        var shaderType = ShaderType.NONE;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if(line.Contains("#shader"))
            {
                if (line.EndsWith("vertex"))
                {
                    shaderType = ShaderType.Vertex;
                } 
                else if(line.EndsWith("fragment"))
                {
                    shaderType = ShaderType.Fragment;
                }
            } else
            {
                dict[shaderType] += line + Environment.NewLine;
            }
        }

        return new(dict[ShaderType.Vertex], dict[ShaderType.Fragment]);
    }
}


public record struct ShaderProgramSource(string VertexShaderSource, string FragmentShaderSource);