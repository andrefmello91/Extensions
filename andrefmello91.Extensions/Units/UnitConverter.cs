using System;
using UnitsNet;
using UnitsNet.Units;

namespace andrefmello91.Extensions;

public static partial class UnitExtensions
{

	#region Methods

	/// <summary>
	///     Convert this <paramref name="quantity" /> to another unit.
	/// </summary>
	/// <param name="quantity">The value to convert.</param>
	/// <param name="fromUnit">The base unit.</param>
	/// <param name="toUnit">The target unit.</param>
	public static double Convert<TUnit>(this double quantity, TUnit fromUnit, TUnit toUnit)
		where TUnit : Enum =>
		fromUnit.Equals(toUnit)
			? quantity
			: UnitConverter.Convert(quantity, fromUnit, toUnit);

	/// <inheritdoc cref="Convert{TUnit}(double,TUnit,TUnit)" />
	public static double Convert<TUnit>(this int quantity, TUnit fromUnit, TUnit toUnit)
		where TUnit : Enum =>
		((double) quantity).Convert(fromUnit, toUnit);

	/// <summary>
	///     Convert this <paramref name="length" /> in <see cref="LengthUnit.Millimeter" /> to another
	///     <see cref="LengthUnit" />.
	/// </summary>
	/// <inheritdoc cref="Convert{TUnit}(double,TUnit,TUnit)" />
	public static double ConvertFromMillimeter(this double length, LengthUnit toUnit) => length.Convert(LengthUnit.Millimeter, toUnit);

	/// <inheritdoc cref="Convert{TUnit}(double,TUnit,TUnit)" />
	public static double ConvertFromMillimeter(this int length, LengthUnit toUnit) => length.Convert(LengthUnit.Millimeter, toUnit);

	/// <summary>
	///     Convert this <paramref name="pressure" /> in <see cref="PressureUnit.Megapascal" /> to another
	///     <see cref="PressureUnit" />.
	/// </summary>
	/// <inheritdoc cref="Convert{TUnit}(double,TUnit,TUnit)" />
	public static double ConvertFromMPa(this double pressure, PressureUnit toUnit) => pressure.Convert(PressureUnit.Megapascal, toUnit);

	/// <inheritdoc cref="ConvertFromMPa(double,PressureUnit)" />
	public static double ConvertFromMPa(this int pressure, PressureUnit toUnit) => pressure.Convert(PressureUnit.Megapascal, toUnit);

	/// <summary>
	///     Convert this <paramref name="force" /> in <see cref="ForceUnit.Newton" /> to another <see cref="ForceUnit" />.
	/// </summary>
	/// <inheritdoc cref="Convert{TUnit}(double,TUnit,TUnit)" />
	public static double ConvertFromNewton(this double force, ForceUnit toUnit) => force.Convert(ForceUnit.Newton, toUnit);

	/// <inheritdoc cref="ConvertFromNewton(double,ForceUnit)" />
	public static double ConvertFromNewton(this int force, ForceUnit toUnit) => force.Convert(ForceUnit.Newton, toUnit);

	/// <summary>
	///     Convert this <paramref name="area" /> in <see cref="AreaUnit.SquareMillimeter" /> to another
	///     <see cref="AreaUnit" />.
	/// </summary>
	/// <inheritdoc cref="Convert{TUnit}(double,TUnit,TUnit)" />
	public static double ConvertFromSquareMillimeter(this double area, AreaUnit toUnit) => area.Convert(AreaUnit.SquareMillimeter, toUnit);

	/// <inheritdoc cref="ConvertFromSquareMillimeter(double,AreaUnit)" />
	public static double ConvertFromSquareMillimeter(this int area, AreaUnit toUnit) => area.Convert(AreaUnit.SquareMillimeter, toUnit);

	#endregion

}