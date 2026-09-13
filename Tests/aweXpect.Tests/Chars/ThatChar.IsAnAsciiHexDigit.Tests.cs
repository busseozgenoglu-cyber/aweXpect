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
			[InlineData('a')]
			[InlineData('f')]
			[InlineData('A')]
			[InlineData('F')]
			public async Task WhenSubjectIsAnAsciiHexDigit_ShouldSucceed(char subject)
			{
				async Task Act()
					=> await That(subject).IsAnAsciiHexDigit();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('/')]
			[InlineData(':')]
			[InlineData('g')]
			[InlineData('G')]
			[InlineData('\u0661')]
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
			[InlineData('a')]
			[InlineData('f')]
			[InlineData('A')]
			[InlineData('F')]
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
			[InlineData('/')]
			[InlineData(':')]
			[InlineData('g')]
			[InlineData('G')]
			[InlineData('\u0661')]
			public async Task WhenSubjectIsNoAsciiHexDigit_ShouldSucceed(char subject)
			{
				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsAnAsciiHexDigit());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
