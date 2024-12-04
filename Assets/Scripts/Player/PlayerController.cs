using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


public class PlayerController : MonoBehaviour
{
    PlayerAction action;
    WeaponAction weaponAction;

    Vector2 moveVec;

    WeaponManager weaponManager;

    private void Awake()
    {
        action = new PlayerAction();
        weaponAction = new WeaponAction();

        weaponAction.Weapon.Add0.performed += AddButtonClick0;
        weaponAction.Weapon.Add1.performed += AddButtonClick1;
        weaponAction.Weapon.Add2.performed += AddButtonClick2;
        weaponAction.Weapon.Add3.performed += AddButtonClick3;

        weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        moveVec = action.Player.Move.ReadValue<Vector2>();
    }

    public void AddButtonClick0(CallbackContext callbackContext)
    {
        weaponManager.ActiveWeapon(WeaponType.Gun);
    }

    public void AddButtonClick1(CallbackContext callbackContext)
    {
        weaponManager.ActiveWeapon(WeaponType.Rocket);
    }

    public void AddButtonClick2(CallbackContext callbackContext)
    {
        weaponManager.ActiveWeapon(WeaponType.Shield);
    }

    public void AddButtonClick3(CallbackContext callbackContext)
    {
        weaponManager.ActiveWeapon(WeaponType.Stick);
    }

    public bool CheckMove()
    {
        return moveVec != Vector2.zero;
    }

    public float CheckFlip()
    {
        return moveVec.x;
    }

    public Vector2 GetValue()
    {
        return moveVec;
    }

    private void OnEnable()
    {
        action.Enable();
        weaponAction.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
        weaponAction.Disable();
    }
}
