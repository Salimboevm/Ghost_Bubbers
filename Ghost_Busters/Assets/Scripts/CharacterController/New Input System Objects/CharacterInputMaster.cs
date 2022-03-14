// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/CharacterController/New Input System Objects/CharacterInputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CharacterInputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterInputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterInputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""54f0a7d3-ac5e-4132-b342-f451d95c53af"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d7846479-2c29-4abf-81b3-317724c60c95"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f8993f6d-f5cf-47e1-b307-795922103cf9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f9b8e195-ce08-4756-add4-3df5c7c65a0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""WeaponChange"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a4220138-e8b4-4952-8ca1-045e2c2f9618"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""c528654c-11c8-4f1d-ba83-944c0e6cae72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChannelIncrease"",
                    ""type"": ""Button"",
                    ""id"": ""96550b89-53a1-42d0-b231-86ffa2d3a75b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""DecreaseChannel"",
                    ""type"": ""Button"",
                    ""id"": ""5eb5bceb-29fb-4903-9be8-54f3817b3479"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Movement"",
                    ""id"": ""3863cbd8-5b27-4966-8f8d-35b84bd48e03"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dbf6f5ac-2f61-4a0a-9d7b-b275c339320c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aa6165e6-172d-4038-95ee-59e5da2b7e1d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fa990fd6-84d8-4745-b964-4d5bde61d329"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""62033461-3f2a-4d50-8839-be04946ea0af"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8847e242-0faa-47c8-9b3c-298082b66214"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55af5589-8e21-4e1a-bb75-e3eaea91613a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38bcabc4-d7c1-412f-81fc-e84aa81e2846"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b67dd54d-a1c7-4827-af15-6d9a7af4052b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d851a5f3-291e-450f-8eb0-d2d3a62e9813"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChannelIncrease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""861a8f2b-5d20-453f-badb-951b788576a7"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DecreaseChannel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_WeaponChange = m_Player.FindAction("WeaponChange", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_ChannelIncrease = m_Player.FindAction("ChannelIncrease", throwIfNotFound: true);
        m_Player_DecreaseChannel = m_Player.FindAction("DecreaseChannel", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_WeaponChange;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_ChannelIncrease;
    private readonly InputAction m_Player_DecreaseChannel;
    public struct PlayerActions
    {
        private @CharacterInputMaster m_Wrapper;
        public PlayerActions(@CharacterInputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @WeaponChange => m_Wrapper.m_Player_WeaponChange;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @ChannelIncrease => m_Wrapper.m_Player_ChannelIncrease;
        public InputAction @DecreaseChannel => m_Wrapper.m_Player_DecreaseChannel;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @WeaponChange.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChange;
                @WeaponChange.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChange;
                @WeaponChange.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChange;
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @ChannelIncrease.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChannelIncrease;
                @ChannelIncrease.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChannelIncrease;
                @ChannelIncrease.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChannelIncrease;
                @DecreaseChannel.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDecreaseChannel;
                @DecreaseChannel.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDecreaseChannel;
                @DecreaseChannel.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDecreaseChannel;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @WeaponChange.started += instance.OnWeaponChange;
                @WeaponChange.performed += instance.OnWeaponChange;
                @WeaponChange.canceled += instance.OnWeaponChange;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ChannelIncrease.started += instance.OnChannelIncrease;
                @ChannelIncrease.performed += instance.OnChannelIncrease;
                @ChannelIncrease.canceled += instance.OnChannelIncrease;
                @DecreaseChannel.started += instance.OnDecreaseChannel;
                @DecreaseChannel.performed += instance.OnDecreaseChannel;
                @DecreaseChannel.canceled += instance.OnDecreaseChannel;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnWeaponChange(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnChannelIncrease(InputAction.CallbackContext context);
        void OnDecreaseChannel(InputAction.CallbackContext context);
    }
}
