using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatChar
{
	/// <summary>
	///     Verifies that the subject is between the <paramref name="minimum" />…
	/// </summary>
	[GuaranteesNotNull]
	public static BetweenResult<AndOrResult<char, IThat<char>>, char> IsBetween(
		this IThat<char> source,
		char minimum)
		=> new(maximum => new AndOrResult<char, IThat<char>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsBetweenConstraint(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is not between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<char, IThat<char>>, char> IsNotBetween(
		this IThat<char> source,
		char minimum)
		=> new(maximum => new AndOrResult<char, IThat<char>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsBetweenConstraint(it, grammars, minimum, maximum).Invert()),
			source));

	private sealed class IsBetweenConstraint(
		string it,
		ExpectationGrammars grammars,
		char minimum,
		char maximum)
		: ConstraintResult.WithNotNullValue<char>(it, grammars),
			IValueConstraint<char>
	{
		public ConstraintResult IsMetBy(char actual)
		{
			Actual = actual;
			Outcome = actual >= minimum && actual <= maximum
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
