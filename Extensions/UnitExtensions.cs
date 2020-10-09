using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace Extensions
{
    /// <summary>
    /// Extensions for <see cref="UnitsNet"/>.
    /// </summary>
    public static class UnitExtensions
    {
        /// <summary>
        /// Returns the abbreviation of this <paramref name="unit"/>.
        /// </summary>
	    public static string Abbrev(this LengthUnit unit) => Length.GetAbbreviation(unit);

        /// <summary>
        /// Returns the abbreviation of this <paramref name="unit"/>.
        /// </summary>
	    public static string Abbrev(this ForceUnit unit) => Force.GetAbbreviation(unit);

        /// <summary>
        /// Returns the abbreviation of this <paramref name="unit"/>.
        /// </summary>
	    public static string Abbrev(this PressureUnit unit) => Pressure.GetAbbreviation(unit);

        /// <summary>
        /// Returns the abbreviation of this <paramref name="unit"/>.
        /// </summary>
	    public static string Abbrev(this AreaUnit unit) => Area.GetAbbreviation(unit);
    }
}
