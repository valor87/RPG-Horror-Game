namespace FoxalFace.Common.Editor
{
    using System;


    public static class EditorHelper
    {
        #region API
        public static int GetPropertyGUID(UnityEngine.Object targetObject, string propertyPath)
        {
            return HashCode.Combine(targetObject.GetInstanceID(), propertyPath.GetHashCode());
        }
        #endregion
    }
}