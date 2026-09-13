using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<char>("Is{Not}Control", "char.IsControl({value})",
	ExpectationText = "is {not} a control character"
)]
public static partial class ThatChar;
