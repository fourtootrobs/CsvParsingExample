using System;
using System.IO;
using System.Net.Http;
using CsvHelper;

namespace CsvParsingExample
{
    public class HttpStreamCsvReaderContext
        : IDisposable
    {
        public CsvReader CsvReader { get; private set; }

        private readonly HttpResponseMessage _responseMessage;
        private bool _disposedValue;        

        public HttpStreamCsvReaderContext(
            HttpResponseMessage responseMessage,
            Stream stream)
        {
            _responseMessage = responseMessage;

            CsvReader = new CsvReader(
                new StreamReader(stream),
                CsvHelperConstants.DefaultCsvConfig);
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    CsvReader?.Dispose();
                    _responseMessage?.Dispose();
                }

                _disposedValue = true;
            }
        }
    }
}
