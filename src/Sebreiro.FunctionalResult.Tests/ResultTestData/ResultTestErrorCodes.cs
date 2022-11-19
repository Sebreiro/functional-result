using System.ComponentModel;

namespace Sebreiro.FunctionalResult.Tests.ResultTestData
{
    public enum ResultTestErrorCodes
    {
        Unknown = 0,
        [Description("Something very bad happened")]
        ErrorCode1 = 1,
        
        [Description("")]
        AnotherError = 2
    }
}