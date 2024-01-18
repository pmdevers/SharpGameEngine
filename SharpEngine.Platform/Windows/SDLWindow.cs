using SharpEngine.Events;
using SDL2;
using SharpEngine.Core;
using SharpEngine.Platform.OpenGL;

namespace SharpEngine.Platform.Windows
{
    public class SDLWindow : IWindow
    {
        private bool _vsync;
        private Action<Event> _eventCallBack;
        nint _window, _glContext;

        public nint Handle { get { return _window; } }
        public bool Fullscreen { get; set; }

        private SDLWindow(string title, int width, int height)
        {
            EntryPoint.CoreLogger.Info("Creating Window {0} ({1}, {2})", title, width, height);

            SDL.Init(SDL.SDL_INIT_VIDEO);
            SDL.GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
            SDL.GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DEPTH_SIZE, 24);
            SDL.GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_ALPHA_SIZE, 8);
            SDL.GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_STENCIL_SIZE, 8);

            //SDL.GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 3);
            //SDL.GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
            //SDL.GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK, SDL.SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE);

            var flags = SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
            if (FullScreen) flags |= SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;


            _window = SDL.CreateWindow(title,
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                width,
                height,
                flags);

            EntryPoint.CoreLogger.Assert(_window == nint.Zero, "Could not create window!");

            _glContext = SDL.GL_CreateContext(_window);
            EntryPoint.CoreLogger.Assert(_glContext == nint.Zero, "Could not create context!");

            ///Renderer.Renderer.Instance = new OpenGLRendererApi();

            SDL.GL_SwapWindow(_window);

            _eventCallBack = (e) => { };
        }



        public bool IsVSync { get; set; }

        public string Title { get; set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public bool FullScreen { get; private set; }

        public int MainThreadID { get; private set; }

        public void OnUpdate()
        {
            var inputManager = (SDLInput)Input.Instance;
            SDL.SDL_Event e;

            var keys = new List<int>();
            var mouseButtons = new List<int>();

            while (SDL.SDL_PollEvent(out e) > 0)
            {

                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_FIRSTEVENT:
                        break;
                    case SDL.SDL_EventType.SDL_QUIT:
                        _eventCallBack.Invoke(new WindowCloseEvent());
                        break;
                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        switch (e.window.windowEvent)
                        {
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                                _eventCallBack.Invoke(new WindowResizeEvent(e.window.data1, e.window.data2));
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                                _eventCallBack.Invoke(new WindowMinimizedEvent());
                                break;
                        }
                        break;
                    case SDL.SDL_EventType.SDL_SYSWMEVENT:
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        keys.Add((int)e.key.keysym.sym);
                        _eventCallBack.Invoke(new KeyPressedEvent((int)e.key.keysym.sym, e.key.repeat));
                        break;
                    case SDL.SDL_EventType.SDL_KEYUP:
                        keys.Remove((int)e.key.keysym.sym);
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
                        inputManager.MouseClick(e.button.button);
                        _eventCallBack.Invoke(new MouseButtonPressedEvent(e.button.button));
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        inputManager.MouseRelease(e.button.button);
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

            inputManager.SetKeys(keys);
            SDL.GL_SwapWindow(_window);

        }

        public void SetEventCallBack(Action<Event> eventCallBack)
        {
            _eventCallBack = eventCallBack;
        }

        public static IWindow Create(WindowProperties properties)
        {
            Input.Instance = new SDLInput();
            return new SDLWindow(properties.Title, properties.Width, properties.Height);
        }
    }
}
