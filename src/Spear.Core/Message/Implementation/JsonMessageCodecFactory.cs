﻿namespace Spear.Core.Message.Implementation
{
    public class JsonMessageCodecFactory : IMessageCodecFactory
    {
        private readonly IMessageEncoder _encoder = new JsonMessageEncoder();
        private readonly IMessageDecoder _decoder = new JsonMessageDecoder();
        public IMessageEncoder GetEncoder()
        {
            return _encoder;
        }

        public IMessageDecoder GetDecoder()
        {
            return _decoder;
        }
    }
}
