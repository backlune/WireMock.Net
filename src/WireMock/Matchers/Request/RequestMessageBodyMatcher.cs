using System;
using JetBrains.Annotations;
using WireMock.Validation;

namespace WireMock.Matchers.Request
{
    /// <summary>
    /// The request body matcher.
    /// </summary>
    public class RequestMessageBodyMatcher : IRequestMatcher
    {
        /// <summary>
        /// The body.
        /// </summary>
        private readonly string _body;

        /// <summary>
        /// The body as byte[].
        /// </summary>
        private readonly byte[] _bodyData;

        /// <summary>
        /// The body function
        /// </summary>
        private readonly Func<string, bool> _bodyFunc;

        /// <summary>
        /// The body data function
        /// </summary>
        private readonly Func<byte[], bool> _bodyDataFunc;

        /// <summary>
        /// The matcher.
        /// </summary>
        public readonly IMatcher Matcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMessageBodyMatcher"/> class.
        /// </summary>
        /// <param name="body">
        /// The body Regex pattern.
        /// </param>
        public RequestMessageBodyMatcher([NotNull] string body)
        {
            Check.NotNull(body, nameof(body));
            _body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMessageBodyMatcher"/> class.
        /// </summary>
        /// <param name="body">
        /// The body Regex pattern.
        /// </param>
        public RequestMessageBodyMatcher([NotNull] byte[] body)
        {
            Check.NotNull(body, nameof(body));
            _bodyData = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMessageBodyMatcher"/> class.
        /// </summary>
        /// <param name="func">
        /// The body func.
        /// </param>
        public RequestMessageBodyMatcher([NotNull] Func<string, bool> func)
        {
            Check.NotNull(func, nameof(func));
            _bodyFunc = func;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMessageBodyMatcher"/> class.
        /// </summary>
        /// <param name="func">
        /// The body func.
        /// </param>
        public RequestMessageBodyMatcher([NotNull] Func<byte[], bool> func)
        {
            Check.NotNull(func, nameof(func));
            _bodyDataFunc = func;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMessageBodyMatcher"/> class.
        /// </summary>
        /// <param name="matcher">
        /// The body matcher.
        /// </param>
        public RequestMessageBodyMatcher([NotNull] IMatcher matcher)
        {
            Check.NotNull(matcher, nameof(matcher));
            Matcher = matcher;
        }

        /// <summary>
        /// Determines whether the specified RequestMessage is match.
        /// </summary>
        /// <param name="requestMessage">The RequestMessage.</param>
        /// <returns>
        ///   <c>true</c> if the specified RequestMessage is match; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMatch(RequestMessage requestMessage)
        {
            if (Matcher != null)
                return Matcher.IsMatch(requestMessage.Body);

            if (_body != null)
                return requestMessage.Body == _body;

            if (_bodyData != null)
                return requestMessage.BodyAsBytes == _bodyData;

            if (_bodyFunc != null)
                return _bodyFunc(requestMessage.Body);

            if (_bodyDataFunc != null)
                return _bodyDataFunc(requestMessage.BodyAsBytes);

            return false;
        }
    }
}