using aweXpect.SourceGenerators;

namespace aweXpect;

#if NET8_0_OR_GREATER
[CreateExpectationOn<char>("Is{Not}AnAsciiHexDigit", "char.IsAsciiHexDigit({value})",
	ExpectationText = "is {not} an ASCII hex digit",
	Remarks = """
	          This means, that the specified character is an ASCII hexadecimal digit.<br />
	          <seealso cref="char.IsAsciiHexDigit(char)" />
	          """
)]
#else
[CreateExpectationOn<char>("Is{Not}AnAsciiHexDigit", "{value} is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f'",
	ExpectationText = "is {not} an ASCII hex digit"
)]
#endif
public static partial class ThatChar;
