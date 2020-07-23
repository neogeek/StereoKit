namespace StereoKit
{
    public class Camera
    {
        /// <summary>Calculates the screen position of a Vec3 in world space relative to the camera.</summary>
        /// <param name="position">The position in world space you want to convert to screen space.</param>
        public static Vec3 WorldToScreenPoint(Vec3 position)
        {
            var mat = Input.Head.ToMatrix().Inverse() * Renderer.Projection * Renderer.CameraRoot;

            var point = mat * new Vec4(position.x, position.y, position.z, 1);

            var x = (point.x / point.w + 1f) * 0.5f * StereoKitApp.System.displayWidth;
            var y = (point.y / point.w + 1f) * 0.5f * StereoKitApp.System.displayHeight;

            if (point.w == 0)
            {
                return Vec3.Zero;
            }

            return new Vec3(x, y, position.z);
        }

        /// <summary>Calculates the viewport position of a Vec3 in world space relative to the camera.</summary>
        /// <param name="position">The position in world space you want to convert to viewport space.</param>
        public static Vec3 WorldToViewportPoint(Vec3 position)
        {
            var point = WorldToScreenPoint(position);

            var x = point.x / (StereoKitApp.System.displayWidth - point.x);
            var y = point.y / (StereoKitApp.System.displayHeight - point.y);

            return new Vec3(x, y, point.z);
        }
    }
}
