using System;
using System.ComponentModel;
using System.Globalization;

namespace TronCell.Game.Sprite.Converters
{
    public class TileListConverter : TypeConverter
    {
        #region Methods

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string[] sValues = value.ToString().Split(',');
            int[] iValues = new int[sValues.Length];
            for (int index = 0; index < sValues.Length; index++)
            {
                iValues[index] = Convert.ToInt32(sValues[index]);
            }

            return new TileList(iValues);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return null;
            }

            TileList list = value as TileList;
            string[] sValues = new string[list.Count];
            for (int index = 0; index < list.Count; index++)
            {
                sValues[index] = list[index].ToString();
            }

            return string.Join(",", sValues);
        }

        #endregion
    }
}