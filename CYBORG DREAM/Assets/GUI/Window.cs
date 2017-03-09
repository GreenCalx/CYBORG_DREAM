using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GUI
{

    using UnityEngine;


    public class Window : MonoBehaviour
    {

        public int _width; // stored in the rect - disposable
        public int _height; // stored in the rect - disposable
        public float _xPos; // stored in the rect - disposable
        public float _yPos; // stored in the rect - disposable
        private Rect _windowRect;

        public bool _show;
        private int _windowID = 0;

        void Start()
        {
            _show = true;
        }

        public Window()
        {
            _width = (Screen.width - 200) / 2;
            _height = (Screen.height - 300) / 2;

            _xPos = 200F;
            _yPos = 200F;

            _windowRect = new Rect(_xPos, _yPos, _width, _height);
            
        }

        public Window(int width, int height, int xpos, int ypos)
        {
            _width = width;
            _height = height;

            _xPos = xpos;
            _yPos = ypos;

            _windowRect = new Rect(_xPos, _yPos, _width, _height);
        }

        void onGui()
        {
            if (_show)
                _windowRect = GUI.Window(_windowID, _windowRect, WindowFunction, "TEST");

        }

        public void WindowFunction(int wID)
        {
            GUI.Label(new Rect(5, 20, _windowRect.width, 20), "HELLO WORLD");
        }

    }
}