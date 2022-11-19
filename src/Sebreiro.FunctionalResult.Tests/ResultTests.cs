using Shouldly;
using Sebreiro.FunctionalResult.Tests.ResultTestData;
using Xunit;

namespace Sebreiro.FunctionalResult.Tests
{
    public class ResultTests
    {
        [Fact]
        public void CreateSuccessResult()
        {
            // Arrange
            
            // Action
            var result = CreateResult(true);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            Should.Throw<ResultException>(() => result.ErrorMessage);
            Should.Throw<ResultException>(() => result.ErrorCode.ToString());
        }

        [Fact]
        public void CreateFailureResult()
        {
            // Arrange

            // Action
            var result = CreateResult(false);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.ErrorMessage.ShouldBe("Something very bad happened");
            result.ErrorCode.ShouldBe(ResultTestErrorCodes.ErrorCode1);
        }

        [Fact]
        public void CreateGenericSuccessResult()
        {
            // Arrange

            // Action
            Result<ResultTestModel> result = CreateGenericResult(true);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
            Should.Throw<ResultException>(() => result.ErrorMessage);
            Should.Throw<ResultException>(() => result.ErrorCode.ToString());
        }

        [Fact]
        public void CreateGenericFailureResult()
        {
            // Arrange

            // Action
            Result<ResultTestModel> result = CreateGenericResult(false);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.ErrorMessage.ShouldBe("Something very bad happened");
            Should.Throw<ResultException>(() => result.Value.ToString());
            result.ErrorCode.ShouldBe(ResultTestErrorCodes.ErrorCode1);
        }

        [Fact]
        public void CreateGenericFailureResultWithCustomMessage()
        {
            // Arrange

            // Action
            var customErrorMessage = "custom error message";
            Result<ResultTestModel> result = Result.Failure<ResultTestModel>(ResultTestErrorCodes.ErrorCode1, customErrorMessage);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.ErrorMessage.ShouldBe("Something very bad happened. " + customErrorMessage);
            Should.Throw<ResultException>(() => result.Value.ToString());
            result.ErrorCode.ShouldBe(ResultTestErrorCodes.ErrorCode1);
        }

        [Fact]
        public void Convert_NonGeneric_ToGeneric()
        {
            // Arrange

            Result.Success(55);

            // Act
            Result result = Result.Failure(ResultTestErrorCodes.ErrorCode1);

            // Assert
            result.ErrorCode.ShouldBe(ResultTestErrorCodes.ErrorCode1);
            result.ErrorMessage.ShouldNotBeNull();
        }

        [Fact]
        public void CreateGenericSuccessResultWithError()
        {
            // Arrange
            Result<ResultTestModel, ErrorResultTestModel> data = CreateGenericResultWithError(true);
            var value = data.Value;

            // Action
            Result<ResultTestModel, ErrorResultTestModel> result = Result.Success<ResultTestModel, ErrorResultTestModel>(value);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
            Should.Throw<ResultException>(() => result.ErrorMessage);
            Should.Throw<ResultException>(() => result.ErrorCode.ToString());
            Should.Throw<ResultException>(() => result.Error.ToString());
        }

        [Fact]
        public void CreateGenericFailureResultWithError()
        {
            // Arrange
            Result<ResultTestModel, ErrorResultTestModel> data = CreateGenericResultWithError(false);
            var error = data.Error;

            // Action
            Result<ResultTestModel, ErrorResultTestModel> result 
                = Result.Failure<ResultTestModel, ErrorResultTestModel>(ResultTestErrorCodes.ErrorCode1, error);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.ErrorMessage.ShouldBe("Something very bad happened");
            Should.Throw<ResultException>(() => result.Value.ToString());
            result.ErrorCode.ShouldBe(ResultTestErrorCodes.ErrorCode1);
            result.Error.Description.ShouldBe(error.Description);
        }

        [Fact]
        public void CreateGenericFailureResultWithErrorAndCustomMessage()
        {
            // Arrange
            var customErrorMessage = "custom error message";
            Result<ResultTestModel, ErrorResultTestModel> data = CreateGenericResultWithError(false);
            var error = data.Error;

            // Action
            Result<ResultTestModel, ErrorResultTestModel> result
                = Result.Failure<ResultTestModel, ErrorResultTestModel>(ResultTestErrorCodes.ErrorCode1, customErrorMessage, error);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.ErrorMessage.ShouldBe("Something very bad happened. " + customErrorMessage);
            Should.Throw<ResultException>(() => result.Value.ToString());
            result.ErrorCode.ShouldBe(ResultTestErrorCodes.ErrorCode1);
            result.Error.Description.ShouldBe(error.Description);
        }

        private Result CreateResult(bool success)
        {
            if (success)
            {
                return Result.Success();
            }

            return Result.Failure(ResultTestErrorCodes.ErrorCode1);
        }

        private Result<ResultTestModel> CreateGenericResult(bool success)
        {
            if (success)
            {
                return Result.Success(new ResultTestModel
                {
                    Name = "Фома",
                    Surname = "Киняев"
                });
            }

            return Result.Failure<ResultTestModel>(ResultTestErrorCodes.ErrorCode1);
        }

        private Result<ResultTestModel, ErrorResultTestModel> CreateGenericResultWithError(bool success)
        {
            if (success)
            {
                return Result.Success<ResultTestModel, ErrorResultTestModel>(new ResultTestModel
                {
                    Name = "Фома",
                    Surname = "Киняев"
                });
            }

            return Result.Failure<ResultTestModel, ErrorResultTestModel>(
                ResultTestErrorCodes.ErrorCode1,
                "message",
                new ErrorResultTestModel
                {
                    Description = "Ошибка"
                });
        }
    }
}
