namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsAnAsciiDigit
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData('0')]
			[InlineData('5')]
			[InlineData('9')]
			public async Task WhenSubjectIsAnAsciiDigit_ShouldSucceed(char subject)
			{
				async Task Act() => await That(subject).IsAnAsciiDigit();
				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('/')]
			[InlineData(':')]
			[InlineData('a')]
			[InlineData('\u0661')]
			public async Task WhenSubjectIsNoAsciiDigit_ShouldFail(char subject)
			{
				async Task Act() => await That(subject).IsAnAsciiDigit();
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is an ASCII digit,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenSubjectIsAnAsciiDigit_ShouldFail()
			{
				char subject = '5';
				async Task Act() => await That(subject).DoesNotComplyWith(it => it.IsAnAsciiDigit());
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not an ASCII digit,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNoAsciiDigit_ShouldSucceed()
			{
				async Task Act() => await That('\u0661').DoesNotComplyWith(it => it.IsAnAsciiDigit());
				await That(Act).DoesNotThrow();
			}
		}
	}
}
