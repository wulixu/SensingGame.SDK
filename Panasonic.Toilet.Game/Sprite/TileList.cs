using System.Collections.Generic;
using System.ComponentModel;
using TronCell.Game.Sprite.Converters;

namespace TronCell.Game.Sprite
{
    [TypeConverter(typeof(TileListConverter))]
    public class TileList : List<int>
    {
        #region Constructors

        public TileList(params int[] tiles)
            : base(tiles)
        {
        }

        #endregion
    }
}