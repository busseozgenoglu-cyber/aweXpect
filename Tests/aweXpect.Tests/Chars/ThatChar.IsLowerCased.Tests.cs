namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsLowerCased
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData('a')]
			[InlineData('z')]
			[InlineData('ä')]
			[InlineData('ω')]
			public async Task WhenSubjectIsLowerCased_ShouldSucceed(char subject)
			{
				async Task Act() => await That(subject).IsLowerCased();
				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('A')]
			[InlineData('1')]
			[InlineData('@')]
			public async Task WhenSubjectIsNotLowerCased_ShouldFail(char subject)
			{
				async Task Act() => await That(subject).IsLowerCased();
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is lower-cased,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenSubjectIsLowerCased_ShouldFail()
			{
				char subject = 'a';
				async Task Act() => await That(subject).DoesNotComplyWith(it => it.IsLowerCased());
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not lower-cased,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotLowerCased_ShouldSucceed()
			{
				async Task Act() => await That('A').DoesNotComplyWith(it => it.IsLowerCased());
				await That(Act).DoesNotThrow();
			}
		}
	}
}
