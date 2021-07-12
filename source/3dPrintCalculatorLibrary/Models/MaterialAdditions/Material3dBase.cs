using AndreasReitberger.Enums;
using System;

namespace AndreasReitberger.Models.MaterialAdditions
{
    public class Material3dBase
    {
        #region Properties 
        public Guid Id
        { get; set; }
        public Material3dTypes Type
        { get; set; }
        public string Material
        { get; set; }
        public string Polymer
        { get; set; }
        #endregion

        #region Constructors
        public Material3dBase() { }
        public Material3dBase(Material3dTypes Type, string Material)
        {
            this.Type = Type;
            this.Material = Material;
        }
        public Material3dBase(Material3dTypes Type, string Material, string Polymer)
        {
            this.Type = Type;
            this.Material = Material;
            this.Polymer = Polymer;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return string.IsNullOrEmpty(Type.ToString()) ? Material : string.Format("{0} ({1})", Material, Type.ToString());
        }
        public override bool Equals(object obj)
        {
            var item = obj as Material3dBase;
            if (item == null)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
