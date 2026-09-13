namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsAnAsciiHexDigit
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData('0')]
			[InlineData('9')]
			[InlineData('A')]
			[InlineData('F')]
			[InlineData('a')]
			[InlineData('f')]
			public async Task WhenSubjectIsAnAsciiHexDigit_ShouldSucceed(char subject)
			{
				async Task Act()
					=> await That(subject).IsAnAsciiHexDigit();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('G')]
			[InlineData('g')]
			[InlineData('x')]
			[InlineData('½')]
			[InlineData('\u0660')]
			public async Task WhenSubjectIsNoAsciiHexDigit_ShouldFail(char subject)
			{
				async Task Act()
					=> await That(subject).IsAnAsciiHexDigit();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is an ASCII hex digit,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}

		public sealed class NegatedTests
		{
			[Theory]
			[InlineData('0')]
			[InlineData('9')]
			[InlineData('A')]
			[InlineData('F')]
			[InlineData('a')]
			[InlineData('f')]
			public async Task WhenSubjectIsAnAsciiHexDigit_ShouldFail(char subject)
			{
				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsAnAsciiHexDigit());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not an ASCII hex digit,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData('G')]
			[InlineData('g')]
			[InlineData('x')]
			[InlineData('½')]
			[InlineData('\u0660')]
			public async Task WhenSubjectIsNoAsciiHexDigit_ShouldSucceed(char subject)
			{
				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsAnAsciiHexDigit());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
