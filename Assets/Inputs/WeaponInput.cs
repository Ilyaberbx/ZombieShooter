// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/WeaponInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace FPS
{
    public class @WeaponInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @WeaponInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""WeaponInput"",
    ""maps"": [
        {
            ""name"": ""Weapon"",
            ""id"": ""488daba3-c4a7-443b-ab0d-e4d67788c7b8"",
            ""actions"": [
                {
                    ""name"": ""FirePressed"",
                    ""type"": ""Button"",
                    ""id"": ""2f9bd277-77c3-4b04-9829-a57ee30002ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireReleased"",
                    ""type"": ""Button"",
                    ""id"": ""fe8969d3-f397-451b-9098-0678f1a2ca59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e5f8b57a-7eb8-4af9-9345-19191b507b92"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""c00463ae-d8f5-4c27-b79b-c6e22e505c23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2d5773c7-510b-47a8-90eb-1250033ce03d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirePressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fcb709ac-e09a-436c-a60a-4bc45f3da0bb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireReleased"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9c37576-72c4-4c24-b5de-38d0bf628406"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cd12485-3930-42dd-a23c-976ec5e395d8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Weapon
            m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
            m_Weapon_FirePressed = m_Weapon.FindAction("FirePressed", throwIfNotFound: true);
            m_Weapon_FireReleased = m_Weapon.FindAction("FireReleased", throwIfNotFound: true);
            m_Weapon_MouseScroll = m_Weapon.FindAction("MouseScroll", throwIfNotFound: true);
            m_Weapon_Reload = m_Weapon.FindAction("Reload", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Weapon
        private readonly InputActionMap m_Weapon;
        private IWeaponActions m_WeaponActionsCallbackInterface;
        private readonly InputAction m_Weapon_FirePressed;
        private readonly InputAction m_Weapon_FireReleased;
        private readonly InputAction m_Weapon_MouseScroll;
        private readonly InputAction m_Weapon_Reload;
        public struct WeaponActions
        {
            private @WeaponInput m_Wrapper;
            public WeaponActions(@WeaponInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @FirePressed => m_Wrapper.m_Weapon_FirePressed;
            public InputAction @FireReleased => m_Wrapper.m_Weapon_FireReleased;
            public InputAction @MouseScroll => m_Wrapper.m_Weapon_MouseScroll;
            public InputAction @Reload => m_Wrapper.m_Weapon_Reload;
            public InputActionMap Get() { return m_Wrapper.m_Weapon; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
            public void SetCallbacks(IWeaponActions instance)
            {
                if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
                {
                    @FirePressed.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFirePressed;
                    @FirePressed.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFirePressed;
                    @FirePressed.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFirePressed;
                    @FireReleased.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFireReleased;
                    @FireReleased.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFireReleased;
                    @FireReleased.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFireReleased;
                    @MouseScroll.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnMouseScroll;
                    @MouseScroll.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnMouseScroll;
                    @MouseScroll.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnMouseScroll;
                    @Reload.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                    @Reload.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                    @Reload.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                }
                m_Wrapper.m_WeaponActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @FirePressed.started += instance.OnFirePressed;
                    @FirePressed.performed += instance.OnFirePressed;
                    @FirePressed.canceled += instance.OnFirePressed;
                    @FireReleased.started += instance.OnFireReleased;
                    @FireReleased.performed += instance.OnFireReleased;
                    @FireReleased.canceled += instance.OnFireReleased;
                    @MouseScroll.started += instance.OnMouseScroll;
                    @MouseScroll.performed += instance.OnMouseScroll;
                    @MouseScroll.canceled += instance.OnMouseScroll;
                    @Reload.started += instance.OnReload;
                    @Reload.performed += instance.OnReload;
                    @Reload.canceled += instance.OnReload;
                }
            }
        }
        public WeaponActions @Weapon => new WeaponActions(this);
        public interface IWeaponActions
        {
            void OnFirePressed(InputAction.CallbackContext context);
            void OnFireReleased(InputAction.CallbackContext context);
            void OnMouseScroll(InputAction.CallbackContext context);
            void OnReload(InputAction.CallbackContext context);
        }
    }
}
