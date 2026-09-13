using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<char>("Is{Not}UpperCased", "char.IsUpper({value})",
	ExpectationText = "is {not} upper cased"
)]
public static partial class ThatChar;
