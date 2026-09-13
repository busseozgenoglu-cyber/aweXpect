namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsADigit
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData('0')]
			[InlineData('5')]
			[InlineData('9')]
			[InlineData('\u0661')]
			public async Task WhenSubjectIsADigit_ShouldSucceed(char subject)
			{
				async Task Act() => await That(subject).IsADigit();
				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('a')]
			[InlineData('½')]
			[InlineData('\t')]
			public async Task WhenSubjectIsNoDigit_ShouldFail(char subject)
			{
				async Task Act() => await That(subject).IsADigit();
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is a digit,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenSubjectIsADigit_ShouldFail()
			{
				char subject = '5';
				async Task Act() => await That(subject).DoesNotComplyWith(it => it.IsADigit());
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not a digit,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNoDigit_ShouldSucceed()
			{
				async Task Act() => await That('½').DoesNotComplyWith(it => it.IsADigit());
				await That(Act).DoesNotThrow();
			}
		}
	}
}
