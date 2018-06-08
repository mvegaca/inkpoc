﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Input;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;

namespace InkPoc.Helpers.Ink
{
    public class InkPointerDeviceManager
    {
        private readonly InkCanvas inkCanvas;

        private bool enableMouse;
        private bool enablePen;
        private bool enableTouch;

        public InkPointerDeviceManager(InkCanvas _inkCanvas)
        {
            inkCanvas = _inkCanvas;
            inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse |
                                                      CoreInputDeviceTypes.Pen |
                                                      CoreInputDeviceTypes.Touch;

            inkCanvas.InkPresenter.UnprocessedInput.PointerEntered += UnprocessedInput_PointerEntered;
        }

        public bool EnableMouse
        {
            get => enableMouse;
            set
            {
                enableMouse = value;
                UpdateInputDevice(CoreInputDeviceTypes.Mouse, value);
            }
        }

        public bool EnablePen
        {
            get => enablePen;
            set
            {
                enablePen = value;
                UpdateInputDevice(CoreInputDeviceTypes.Pen, value);
            }
        }

        public bool EnableTouch
        {
            get => enableTouch;
            set
            {
                enableTouch = value;
                UpdateInputDevice(CoreInputDeviceTypes.Touch, value);
            }
        }

        private void UnprocessedInput_PointerEntered(InkUnprocessedInput sender, PointerEventArgs e)
        {
            if (e.CurrentPoint.PointerDevice.PointerDeviceType == PointerDeviceType.Pen)
            {
                EnableTouch = false;
            }
        }
        
        private void UpdateInputDevice(CoreInputDeviceTypes inputDevice, bool isEnabled)
        {
            inkCanvas.InkPresenter.InputDeviceTypes = isEnabled
                ? inkCanvas.InkPresenter.InputDeviceTypes | inputDevice
                : inkCanvas.InkPresenter.InputDeviceTypes & ~inputDevice;
        }
    }
}
