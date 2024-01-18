using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SharpEngine.Core;
using SharpEngine.Renderer;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;

namespace SharpEngine.Platform.OpenGL
{
    internal class OpenGLShader : IShader
    {
        public OpenGLShader(string name, string vertexSrc, string fragmentSrc)
        {
            _id = GL.CreateProgram();
            int vs = CompileShader(ShaderType.VertexShader, vertexSrc);
            int fs = CompileShader(ShaderType.FragmentShader, fragmentSrc);

            GL.AttachShader(_id, vs);
            GL.AttachShader(_id, fs);

            GL.LinkProgram(_id);
            GL.ValidateProgram(_id);

            GL.DeleteShader(vs);
            GL.DeleteShader(fs);
            
            Name = name;

        }
        ~OpenGLShader()
        {
            Dispose();
        }

        private int CompileShader(ShaderType shaderType, string source)
        {
            var id = GL.CreateShader(shaderType);
            
            GL.ShaderSource(id, source);

            GL.CompileShader(id);

            var log = GL.GetShaderInfoLog(id);

            if (!string.IsNullOrEmpty(log))
            {
                EntryPoint.CoreLogger.Error(log);
            }

            return id;
        }

        private int _id;
        public string Name { get; private set; }

        public void Bind()
        {
            GL.UseProgram(_id);
        }

        public void SetFloat(string name, float value)
        {
            int location = GL.GetUniformLocation(_id, name);
            // Gl.Uniform1(location, value);
        }

        public void SetFloat2(string name, Vector2 value)
        {
            int location = GL.GetUniformLocation(_id, name);
            //Gl.Uniform2(location, value);
        }

        public void SetFloat3(string name, Vector3 value)
        {
            int location = GL.GetUniformLocation(_id, name);
            //Gl.Uniform3(location, value);
        }

        public void SetFloat4(string name, Vector4 value)
        {
            int location = GL.GetUniformLocation(_id, name);
            //Gl.Uniform4(location, value);
        }

        public void SetInt(string name, int value)
        {
            int location = GL.GetUniformLocation(_id, name);
            //Gl.Uniform1(location, value);
        }

        public void SetIntArray(string name, int[] values)
        {
            int location = GL.GetUniformLocation(_id, name);
            //Gl.Uniform1(location, values);
        }

        public unsafe void SetMatrix4(string name, Matrix4 value)
        {
            int location = GL.GetUniformLocation(_id, name);
            // Gl.UniformMat4(location, 1, false, (float*)&value);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if(_id > 0)
            {
                GL.DeleteProgram(_id);
                _id = 0;
            }
        }
    }
}
