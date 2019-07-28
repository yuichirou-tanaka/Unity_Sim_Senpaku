// Copyright (C) 2017 ToyUro All Rights Reserved.
using UnityEngine;
namespace ToyUro
{
    // カメラを対象に視線を集めつつ周囲を回転する動き
    public class CameraLookAtFollowRot : MonoBehaviour 
    {
        public GameObject player = null;

        [SerializeField]
        Vector3 _CameraOffset = new Vector3(0.0f, 8f, -45f);

        public Vector3 CameraOffset {
            get {
                return _CameraOffset;
            }
            set
            {
                _CameraOffset = value;
            }
        }

        private GameObject _camera;

        public float CameraSpeed = 10f;

        float pitchamount = 20.0f;


        public bool cameraPlay = true;


        // Use this for initialization
        void Start()
        {
            _camera =  this.gameObject;// GetComponent<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!cameraPlay) return;
            {


                // カメラのオフセット
                Vector3 offset = CameraOffset;
                // キャラ位置
                Vector3 targetPos = player.transform.position;
                // カメラのAngle X
                float cameraAngle = _camera.transform.eulerAngles.y;
                // キャラのAngleY
                float targetAngle = player.transform.eulerAngles.y;

                SetCameraTransLookAt(offset, targetPos, cameraAngle, targetAngle);
            }


        }

        public void SetCameraTransLookAt(Vector3 offset, Vector3 targetPos, float cameraAngle, float targetAngle)
        {
            if (_camera == null) return;
            //目標Angle カメラAngleから目標Angleまでの、線形補完カメラ速度に経過時間を掛けた値）
            targetAngle = Mathf.LerpAngle(cameraAngle, targetAngle, CameraSpeed * 0.612f);
            // オイラー角をAngleから決めて、オフセット値をかけると回転済みオフセット値が取得
            offset = Quaternion.Euler(pitchamount, targetAngle, 0) * offset;
            // 目標とするカメラの位置を決定、カメラ位置と目標位置＋オフセット値、カメラの速度に経過時間を掛けた値
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPos + offset, CameraSpeed * 0.6f);
            //Debug.Log("targetAngle=" + targetAngle.ToString() + " offset=" + offset.ToString());

            // カメラの方角はターゲットの位置が中心
            _camera.transform.LookAt(targetPos);

        }


        static void dlog(string msg)
        {
            Debug.Log("CameraLookAtFollowRot:" + msg);
        }
    }

}