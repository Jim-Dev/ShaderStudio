using ShaderStudio.Objects.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XNA = Microsoft.Xna.Framework;

namespace ShaderStudio.Core
{
    public class Camera
    {

        public XNA.Vector3 Position = XNA.Vector3.Zero;
        public XNA.Quaternion Rotation = XNA.Quaternion.Identity;
        public XNA.Vector3 Scale = XNA.Vector3.One;

        private XNA.Vector3 cameraTarget = XNA.Vector3.Zero;
        public XNA.Vector3 CameraDirection = XNA.Vector3.Zero;
        public XNA.Vector3 CameraUp = XNA.Vector3.Zero;
        public XNA.Vector3 CameraRight = XNA.Vector3.Zero;
        public XNA.Vector3 CameraFront = new XNA.Vector3(0.0f, 0.0f, -1.0f);

        private float cameraFOV = 45;

        public float NearPlane = 0.1f;
        public float FarPlane = 100f;

        private Cross cameraTargetGizmo;

        public XNA.Vector3 CameraTarget
        {
            get { return this.cameraTarget; }
            set
            {
                this.cameraTarget = value;
                cameraTargetGizmo.Position = value;
            }
        }

        public float CameraFOV
        {
            get { return this.cameraFOV; }
            set
            {
                if (value < 0)
                    this.cameraFOV = 0 + 1;
                else if (value > 180)
                    this.cameraFOV = 180 - 1;
                else
                    this.cameraFOV = value;
            }
        }

        public Camera()
        {
            cameraTargetGizmo = new Cross();
            cameraTargetGizmo.Scale = new XNA.Vector3(0.25f);
            Scene.CurrentScene.AddSceneObject(cameraTargetGizmo);
        }

        public void SetDirection(float pitch, float yaw, float roll)
        {
            CameraDirection.X = (float)(Math.Cos(yaw) * Math.Cos(pitch));
            CameraDirection.Y = (float)(Math.Sin(pitch));
            CameraDirection.Z = (float)(Math.Sin(yaw) * Math.Cos(pitch));
        }

        public XNA.Matrix GetViewMatrix()
        {
            CameraDirection = XNA.Vector3.Normalize(Position - CameraTarget);
            CameraRight = XNA.Vector3.Normalize(XNA.Vector3.Cross(XNA.Vector3.Up, CameraDirection));
            CameraUp = XNA.Vector3.Normalize(XNA.Vector3.Cross(CameraDirection, CameraRight));

            XNA.Matrix output = XNA.Matrix.CreateLookAt(Position, CameraTarget, CameraUp);

            return output;
        }

        public XNA.Matrix GetProjectionMatrix(float width, float height)
        {
            return XNA.Matrix.CreatePerspectiveFieldOfView(XNA.MathHelper.ToRadians(CameraFOV), width / height, NearPlane, FarPlane);
        }

        public static Camera Default
        {
            get
            {
                Camera tmpCamera = new Camera();
                tmpCamera.CameraFOV = 60f;
                tmpCamera.Position = new XNA.Vector3(0f, 0f, 5f);
                return tmpCamera;
            }
        }

    }
}
