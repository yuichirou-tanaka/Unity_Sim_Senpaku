#if UNITY_EDITOR
#elif UNITY_ANDROID
//#define USE_CINPUT
#endif

using UnityEngine;


using System.Collections.Generic;

#region ToyboxInput
/// <summary>
/// ToyboxInput
///  入力ライブラリ
/// </summary>
public class ToyboxInput
{

    public static bool GetKeyUp(KeyCode key)
    {
#if USE_CINPUT
		return Input.GetKeyUp(key);
#else
        return Input.GetKeyUp(key);
#endif
    }

    public static bool GetButtonDown(string name)
    {
#if USE_CINPUT
		return CrossPlatformInput.GetButtonDown(name);
#else
        return Input.GetButtonDown(name);
#endif

    }

    public static bool GetButtonPressed(string name)
    {
#if USE_CINPUT
		return CrossPlatformInput.GetButton(name);
#else
        return Input.GetButton(name);
#endif
    }

    public static bool GetButtonUp(string name)
    {
#if USE_CINPUT
		return CrossPlatformInput.GetButtonUp(name);
#else
        return Input.GetButtonUp(name);
#endif

    }


    public static float GetAxis(string name)
    {
#if USE_CINPUT
		return CrossPlatformInput.GetAxis(name);
#else
        return Input.GetAxis(name);
#endif
    }

    public static float GetAxisRaw(string name)
    {
#if USE_CINPUT
		return CrossPlatformInput.GetAxisRaw(name);
#else
        return Input.GetAxisRaw(name);
#endif
    }


    public static bool GetMouseOrTouchButtonDown(int number = 0)
    {
#if USE_CINPUT
		return Input.GetTouch(number).phase == TouchPhase.Began;
#else
        return Input.GetMouseButtonDown(0);
#endif
    }

    public static bool GetMouseOrTouchButtonUp(int number = 0)
    {
#if USE_CINPUT
		return Input.GetTouch(number).phase == TouchPhase.Ended;
#else
        return Input.GetMouseButtonUp(0);
#endif
    }

    public static Vector3 GetMouseOrTouchButtonPosition(int number = 0)
    {
#if USE_CINPUT
		return Input.GetTouch(number).position;
#else
        return Input.mousePosition;
#endif
    }
}
#endregion

#region GateInput
/// <summary>
///  GateInput
///  
/// </summary>
public class GateInput : ToyUro.SingletonMonoBehaviour<GateInput>
{
    public enum eButtonId
    {
        ButtonId_None

      , ButtonId_Left
      , ButtonId_Right
      , ButtonId_Up
      , ButtonId_Down
      , ButtonId_Jump
      , ButtonId_Dash
      , ButtonId_Item

      , ButtonId_Max

    }


    eButtonId PrevButtonId = eButtonId.ButtonId_None;
    eButtonId NowButtonId = eButtonId.ButtonId_None;

    List<bool> inputed;
    public eButtonId ButtonId
    {
        get
        {
            return NowButtonId;
        }
    }


    void Awake()
    {
        
    }

    void Start()
    {
        // ゲームパッドオブジェクトを作成します。
    }
    void Update()
    {
        PrevButtonId = NowButtonId;

        if(IsUpdatePadInputUp()){}
        else if(IsUpdatePadInputDown()){}
        else if(IsUpdatePadInputRight()){}
        else if(IsUpdatePadInputLeft()){}
        else{
            NowButtonId = eButtonId.ButtonId_None;
        }
    }
    static public Vector2 GetProControllerLeftStickX()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    static public Vector2 GetProControllerRightStickX()
    {
        return new Vector2(Input.GetAxis("RightH"), Input.GetAxis("RightV"));
    }

    public bool IsPressedPadInput()
    {
        return PrevButtonId == eButtonId.ButtonId_None;
    }


    #region up


    public bool IsPressedPadInputUp()
    {
        if (!IsPressedPadInput()) return false;
        return ButtonId == eButtonId.ButtonId_Up;
    }

 

    public bool IsUpdatePadInputUp()
    {
        bool ret = IsPadInputUp();
        if (ret)
        {
            NowButtonId = eButtonId.ButtonId_Up;
        }
        return ret;
    }
    static public bool IsPadInputUp()
    {
        return IsGamePadTriggered() || IsLeftStickUp();
    }

    static public bool IsPadInputPressedUp()
    {
        return IsGamePadPressedUp() || IsLeftStickUp();
    }


    static public bool IsLeftStickUp()
    {
        return Input.GetAxis("Vertical") > 0.02;
    }
    static public bool IsGamePadPressedUp()
    {
        return false;
    }
    static public bool IsGamePadTriggered()
    {
        return false;
    }

    #endregion

    #region down


    public bool IsUpdatePadInputDown()
    {
        bool ret = IsPadInputDown();
        if (ret)
        {
            NowButtonId = eButtonId.ButtonId_Down;
        }
        return ret;
    }
    static public bool IsPadInputDown()
    {
       return IsGamePadInputTriggeredDown() || IsLeftStickDown();
    }
    static public bool IsPadInputPressedDown()
    {
       return IsGamePadInputPressedDown() || IsLeftStickDown();
    }

    public bool IsPressedPadInputDown()
    {
        if (!IsPressedPadInput()) return false;
        return ButtonId == eButtonId.ButtonId_Down;
    }

    static public bool IsLeftStickDown()
    {
        return Input.GetAxis("Vertical") < -0.02;
    }

    static public bool IsGamePadInputTriggeredDown()
    {
        return false;
    }

    static public bool IsGamePadInputPressedDown()
    {
        return false;
    }


    #endregion

    #region left

    public bool IsUpdatePadInputLeft()
    {
        bool ret = IsPadInputLeft();
        if (ret)
        {
            NowButtonId = eButtonId.ButtonId_Left;
        }
        return ret;
    }
    static public bool IsPadInputLeft()           {        return IsPadInputTriggeredLeftGamePadState() || IsLeftStickLeft();   }
    static public bool IsPadInputPressedLeft()    {        return IsPadInputPressedLeftGamePadState()   || IsLeftStickLeft();   }

    /// <summary>
    /// Left stick
    /// </summary>
    /// <returns></returns>
    static public bool IsLeftStickLeft()
    {
        return Input.GetAxis("Horizontal") < -0.02;
    }

    static public bool IsPadInputTriggeredLeftGamePadState()
    {
        return false;
    }

    static public bool IsPadInputPressedLeftGamePadState()
    {
        return false;
    }

    #endregion

    // -----------------------------------------------------------------------------------------------
    #region right
    /// <summary>
    /// 押したとき
    /// </summary>
    /// <returns></returns>
    public bool IsUpdatePadInputRight()
    {
        bool ret = IsPadInputRight();
        if (ret)
        {
            NowButtonId = eButtonId.ButtonId_Right;
        }
        return ret;
    }
    /// <summary>
    /// 押し続けた
    /// </summary>
    /// <returns></returns>
    public bool IsUpdatePadInputPressedRight()
    {
        bool ret = IsPressedPadInputRight();
        if (ret)
        {
            NowButtonId = eButtonId.ButtonId_Right;
        }
        return ret;
    }

    public bool IsPressedPadInputRight()
    {
        if (!IsPressedPadInput()) return false;
        return ButtonId == eButtonId.ButtonId_Right;
    }

    static public bool IsLeftStickRight()
    {
        return Input.GetAxis("Horizontal") > 0.02;

    }
    static public bool IsPadInputRight()
    {
        return IsPadInputTriggeredRightGamePadState() || IsLeftStickRight();
    }
    static public bool IsPadInputPressedRight()
    {
        return IsPadInputPressedRightGamePadState() ||  IsLeftStickRight();
    }

    static public bool IsPadInputTriggeredRightGamePadState()
    {
        return false;
    }

    static public bool IsPadInputPressedRightGamePadState()
    {
        return false;
    }
#endregion

    // -----------------------------------------------------------------------------------------------

    // -----------------------------------------------------------------------------------------------

    static public void dlog(string msg)
    {

    }


}
#endregion