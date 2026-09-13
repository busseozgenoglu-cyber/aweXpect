using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<char>("Is{Not}LowerCased", "char.IsLower({value})",
	ExpectationText = "is {not} lower cased"
)]
public static partial class ThatChar;
