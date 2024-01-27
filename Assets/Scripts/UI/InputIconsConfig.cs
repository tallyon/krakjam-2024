using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu]
    public class InputIconsConfig : ScriptableObject
    {
        [SerializeField] private List<InputIconSet> iconSet = new();

        public InputIconSet GetIcons(string deviceName)
        {
            var controllerType = deviceName switch
            {
                "FastKeyboard" => ControllerType.KB, 
                _ => ControllerType.PS
            };
            return iconSet.FirstOrDefault(val => val.type == controllerType);
        }
    }

    public enum ControllerType
    {
        PS,
        XBOX,
        KB,
        DEFAULT
    }

    [Serializable]
    public class InputIconSet
    {
        public ControllerType type;
        public InputIcons icons;
    }
    
    [Serializable]
    public struct InputIcons
    {
        public Sprite skill1Icon;
        public Sprite skill2Icon;
        public Sprite interactableIcon;
        public Sprite goUp;
        public Sprite goDown;
        public Sprite goLeft;
        public Sprite goRight;
    }
}