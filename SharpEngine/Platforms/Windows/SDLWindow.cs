using System;
using System.Collections.Generic;
using System.Text;
using SharpEngine.Events;
using SDL2;
using OpenGL;

namespace SharpEngine.Platforms.Windows
{
    public class SDLWIndow : Window
    {
        private bool _vsync;
        private Action<Events.Event> _eventCallBack;
        IntPtr _window, _glContext;

        public override IntPtr Handle { get { return _window; } }

        private SDLWIndow(string title, int width, int height) : base(title, width, height)
        {

            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DEPTH_SIZE, 24);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_ALPHA_SIZE, 8);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_STENCIL_SIZE, 8);

            var flags = SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
            if (FullScreen) flags |= SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;


            _window = SDL.SDL_CreateWindow("title",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                width,
                height,
                flags);
            
            EntryPoint.CoreLogger.Assert(_window == IntPtr.Zero, "Could not create window!");

            _glContext = SDL.SDL_GL_CreateContext(_window);
            EntryPoint.CoreLogger.Assert(_glContext == IntPtr.Zero, "Could not create context!");

            Gl.ClearColor(0f, 0f, 0f, 1f);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            SDL.SDL_GL_SwapWindow(_window);
        }


        public static Window Create(WindowProperties properties)
        {
            return new SDLWIndow(properties.Title, properties.Width, properties.Height);
        }

        public override bool IsVSync { get
            {
                return _vsync;
            }
            set
            {
                _vsync = value;
            }
        }

        public override void OnUpdate()
        {
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) > 0){

                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_FIRSTEVENT:
                        break;
                    case SDL.SDL_EventType.SDL_QUIT:
                        _eventCallBack.Invoke(new WindowCloseEvent());
                        break;
                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        break;
                    case SDL.SDL_EventType.SDL_SYSWMEVENT:
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        _eventCallBack.Invoke(new KeyPressedEvent((int)e.key.keysym.sym, e.key.repeat));
                        break;
                    case SDL.SDL_EventType.SDL_KEYUP:
                        _eventCallBack.Invoke(new KeyReleaseEvent((int)e.key.keysym.sym));
                        break;
                    case SDL.SDL_EventType.SDL_TEXTEDITING:
                        break;
                    case SDL.SDL_EventType.SDL_TEXTINPUT:
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        _eventCallBack.Invoke(new MouseMovedEvent(e.motion.x, e.motion.y));
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        _eventCallBack.Invoke(new MouseButtonPressedEvent(e.button.button));
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        _eventCallBack.Invoke(new MouseButtonReleasedEvent(e.button.button));
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                        _eventCallBack.Invoke(new MouseScrollEvent(e.wheel.x, e.wheel.y));
                        break;
                    case SDL.SDL_EventType.SDL_JOYAXISMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_JOYBALLMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_JOYHATMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_JOYBUTTONDOWN:
                        break;
                    case SDL.SDL_EventType.SDL_JOYBUTTONUP:
                        break;
                    case SDL.SDL_EventType.SDL_JOYDEVICEADDED:
                        break;
                    case SDL.SDL_EventType.SDL_JOYDEVICEREMOVED:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERBUTTONUP:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED:
                        break;
                    case SDL.SDL_EventType.SDL_FINGERDOWN:
                        break;
                    case SDL.SDL_EventType.SDL_FINGERUP:
                        break;
                    case SDL.SDL_EventType.SDL_FINGERMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_DOLLARGESTURE:
                        break;
                    case SDL.SDL_EventType.SDL_DOLLARRECORD:
                        break;
                    case SDL.SDL_EventType.SDL_MULTIGESTURE:
                        break;
                    case SDL.SDL_EventType.SDL_CLIPBOARDUPDATE:
                        break;
                    case SDL.SDL_EventType.SDL_DROPFILE:
                        break;
                    case SDL.SDL_EventType.SDL_LASTEVENT:
                        break;
                }
            }

            SDL.SDL_GL_SwapWindow(_window);

        }

        public override void SetEventCallBack(Action<Events.Event> eventCallBack)
        {
            _eventCallBack = eventCallBack;
        }

    }
}
