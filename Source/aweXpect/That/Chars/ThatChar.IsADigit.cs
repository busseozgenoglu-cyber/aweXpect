using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<char>("Is{Not}ADigit", "char.IsDigit({value})",
	ExpectationText = "is {not} a digit"
)]
public static partial class ThatChar;
